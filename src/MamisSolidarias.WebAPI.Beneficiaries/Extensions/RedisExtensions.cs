using StackExchange.Redis;

namespace MamisSolidarias.WebAPI.Beneficiaries.Extensions;

internal static class RedisExtensions
{
    public static void AddRedis(this IServiceCollection services, IConfiguration configuration, ILoggerFactory loggerFactory)
    {
        var logger = loggerFactory.CreateLogger("Redis");
        
        var connectionString = configuration.GetConnectionString("Redis");
        if (connectionString is null)
        {
            logger.LogError("Redis connection string is null");
            throw new ArgumentNullException(nameof(connectionString),"Redis connection string is null");
        }
        services.AddSingleton(ConnectionMultiplexer.Connect(connectionString));
    }
}