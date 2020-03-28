using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Common;
using Common.Models;
using NHibernate.Cfg;

namespace RedisFetcherService
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var session = new Configuration().BuildDefaultSession().OpenSession())
            {
                RedisManager.OnWork((cacheClient, db) =>
                {
                    while (true)
                    {
                        try
                        {
                            foreach (var key in cacheClient.SearchKeys("*"))
                            {
                                Console.WriteLine(key);
                                var items = cacheClient.GetAll<Movie>(new[] { key });

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
                                
                                db.KeyDelete(key);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }


                        Console.WriteLine("Waiting 3 seconds");
                        Thread.Sleep(3000);
                    }
                });
            }
        }
    }
}
