using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

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

//// JWT-аутентификация
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer("Bearer", options =>
//    {
//        //options.Authority = "http://authorservice.api:8080";
//        //options.Audience = "ApiGateway";
//        //options.RequireHttpsMetadata = false;

//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidIssuer = "AuthorService", ValidateIssuer = true,
//            ValidAudience = "ApiGateway", ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(
//                Encoding.UTF8.GetBytes(jwtSecret)) // позже вынесем в env
//        };
//    });

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("require-jwt", policy => policy.RequireAuthenticatedUser());
//});

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapReverseProxy();

app.Run();
