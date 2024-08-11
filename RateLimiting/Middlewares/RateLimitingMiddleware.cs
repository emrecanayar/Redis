using StackExchange.Redis;

namespace RateLimiting.Middlewares
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConnectionMultiplexer _redis;

        public RateLimitingMiddleware(RequestDelegate next, IConnectionMultiplexer redis)
        {
            _next = next;
            _redis = redis;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string? ipAddress = context.Connection.RemoteIpAddress?.ToString();
            string redisKey = $"RateLimit:{ipAddress}";

            var db = _redis.GetDatabase();

            // Kullanıcının mevcut istek sayısını al

            var requestCount = await db.StringIncrementAsync(redisKey);

            // Eğer bu kullanıcının ilk isteği ise expiration süresi belirle
            if (requestCount == 1)
            {
                await db.KeyExpireAsync(redisKey, TimeSpan.FromMinutes(1));
            }

            // Eğer istek sayısı belirlenen limiti aşarsa, 429 hatası döndür
            if (requestCount > 100)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Too many requests. Please try again later.");
                return;
            }

            // İstek sınırı aşılmadıysa, bir sonraki middleware'e geç
            await _next(context);

        }
    }
}
