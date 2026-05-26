using System.Text.Json.Serialization;
using api.Data;
using api.Services;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://securetoken.google.com/financial-tracker-api-3a630";

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://securetoken.google.com/financial-tracker-api-3a630",

            ValidateAudience = true,
            ValidAudience = "financial-tracker-api-3a630",

            ValidateLifetime = true,
        };
    });

builder.Services.AddAuthorization();

builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
    );

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "Frontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
        }
    );
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<AccountsService>();
builder.Services.AddScoped<CategoriesService>();
builder.Services.AddScoped<TransactionsService>();

var firebasePath = Path.Combine(
    builder.Environment.ContentRootPath,
    "firebase",
    "firebase-admin.json"
);

FirebaseApp.Create(
    new AppOptions()
    {
        Credential = CredentialFactory
            .FromFile<ServiceAccountCredential>(firebasePath)
            .ToGoogleCredential(),
    }
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("Frontend");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
