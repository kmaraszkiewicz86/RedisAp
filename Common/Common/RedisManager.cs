using System;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace Common
{
    public static class RedisManager
    {
        public static void OnWork(Action<IRedisDatabase, IDatabase> action, Action onNoConnection = null)
        {
            try
            {
                using (var connect = new RedisSinglePool("localhost"))
                {
                    var db = connect.GetConnection().GetDatabase();

                    var serializer = new NewtonsoftSerializer();
                    var cacheClient =
                        new RedisCacheClient(connect, serializer, new RedisConfiguration());

                        action(cacheClient.Db0, db);
                }
            }
            catch (StackExchange.Redis.RedisConnectionException)
            {
                onNoConnection?.Invoke();
            }

        }
    }
}
