using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Common
{
    internal class RedisSinglePool : IRedisCacheConnectionPoolManager
    {
        private readonly IConnectionMultiplexer _connection;

        public RedisSinglePool(string connectionString)
        {
            _connection = ConnectionMultiplexer.Connect(connectionString);
        }

        public IConnectionMultiplexer GetConnection()
        {
            return _connection;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
