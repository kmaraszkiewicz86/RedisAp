using System;
using System.Threading;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace Common
{
    public static class RedisManager
    {
        public static void OnWork(Action<StackExchangeRedisCacheClient, IDatabase> action)
        {
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost"))
            {
                var db = redis.GetDatabase();

                var serializer = new NewtonsoftSerializer();
                using (var cacheClient = new StackExchangeRedisCacheClient(redis, serializer))
                {
                    action(cacheClient, db);
                }
            }
        }
    }
}
