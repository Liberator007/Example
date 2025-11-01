using System.Security.Claims;

namespace PostService.API.Middleware
{
    public class UserHeaderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HashSet<string> _publicPaths = new()
        {
            "/posts",           // пример: GET /posts
            "/posts/{id}"       // пример: GET /posts/{id}
        };

        public UserHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLowerInvariant();

            // Если маршрут публичный — просто пропускаем дальше
            if (IsPublicEndpoint(context.Request.Method, path))
            {
                await _next(context);
                return;
            }

            // Проверяем авторизацию
            var userIdHeader = context.Request.Headers["X-User-Id"].FirstOrDefault();

            if (string.IsNullOrEmpty(userIdHeader))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("User ID not found in headers");
                return;
            }

            // Сохраняем userId для контроллеров
            context.Items["UserId"] = Guid.Parse(userIdHeader);

            var claims = new List<Claim> { new(ClaimTypes.NameIdentifier, userIdHeader) };
            context.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "GatewayHeader"));

            await _next(context);
        }

        private bool IsPublicEndpoint(string method, string? path)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            // простая логика: GET-запросы на /posts разрешены без авторизации
            if (method.Equals("GET", StringComparison.OrdinalIgnoreCase) && path.StartsWith("/posts"))
                return true;

            return false;
        }
    }

}
