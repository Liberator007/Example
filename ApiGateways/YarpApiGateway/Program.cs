using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddRateLimiter(rateLimiterOptions =>
//{
//    rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
//    {
//        options.Window = TimeSpan.FromSeconds(10);
//        options.PermitLimit = 5;
//    });
//});

//var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "AuthorService";
//var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "ApiGateway";
//var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? "super-secret-key";

var jwtSection = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSection["Key"]!);

// JWT-аутентификация
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Bearer", options =>
    {
        //options.Authority = "http://authorservice:8080";
        //options.Audience = "ApiGateway";
        //options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            //ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization();
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("require-jwt", policy => policy.RequireAuthenticatedUser());
//});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "API Gateway",
//        Version = "v1",
//        Description = "Gateway для AuthorService и PostService"
//    });
//});

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwaggerUI(c =>
    //{
    //    // Ты можешь добавить сюда ссылки на Swagger других сервисов
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gateway v1");
    //    c.SwaggerEndpoint("https://authorservice.api/swagger/v1/swagger.json", "AuthorService API via Gateway");
    //});
}

app.Use(async (context, next) =>
{
    if (context.User.Identity?.IsAuthenticated == true)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //var email = context.User.FindFirst(ClaimTypes.Email)?.Value;

        if (!string.IsNullOrEmpty(userId))
            context.Request.Headers["X-User-Id"] = userId;
        //if (!string.IsNullOrEmpty(email))
        //    context.Request.Headers["X-User-Email"] = email;
    }

    await next();
});

app.Use(async (context, next) =>
{
    Console.WriteLine($"➡️ {context.Request.Method} {context.Request.Path}");
    await next();
    Console.WriteLine($"⬅️ {context.Response.StatusCode}");
});

app.UseAuthentication();
app.UseAuthorization();
app.MapReverseProxy();

app.Run();
