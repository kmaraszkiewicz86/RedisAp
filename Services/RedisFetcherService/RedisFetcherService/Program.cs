using System;
using System.Linq;
using System.Threading;
using Common;
using Common.Models;
using Newtonsoft.Json;
using NHibernate.Cfg;

namespace RedisFetcherService
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var session = new Configuration().BuildDefaultSession().OpenSession())
            {
                while (true)
                {
                    RedisManager.OnWork((cacheClient, db) =>
                    {
                        while (true)
                        {
                            try
                            {
                                var keys = cacheClient.SearchKeysAsync("*").GetAwaiter().GetResult().ToList();

                                if (keys.Any())
                                {
                                    foreach (var key in keys)
                                    {
                                        Console.WriteLine($"Trying to parse {key} data");

                                        try
                                        {
                                            var items = cacheClient.GetAllAsync<Movie>(new[] { key }).GetAwaiter().GetResult();

                                            foreach (var item in items)
                                            {
                                                var movie = new Movie
                                                {
                                                    Name = item.Value.Name,
                                                    Describe = item.Value.Describe
                                                };

                                                movie.Category = session.Query<Category>()
                                                    .FirstOrDefault(c => c.Id == item.Value.Category.Id);


                                                session.Save(movie);
                                            }
                                        }
                                        catch (JsonReaderException)
                                        {
                                            Console.WriteLine("Data is not compatible, skipping...");
                                        }

                                        db.KeyDelete(key);

                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"{DateTime.Now:HH:mm:ss tt zz} No new keys found");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }

                            Console.WriteLine($"{DateTime.Now:HH:mm:ss tt zz} Waiting 3 seconds");
                            Thread.Sleep(3000);
                        }

                    }, () =>
                    {
                        Console.WriteLine($"{DateTime.Now:HH:mm:ss tt zz} Connection for redis database failed. I am trying to connect again for 3 seconds");
                        Thread.Sleep(3000);
                    });
                }
            }
        }
    }
}
