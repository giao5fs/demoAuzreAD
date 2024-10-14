using api1.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration.GetValue<string>("AzureAd:Authority");
        options.Audience = builder.Configuration.GetValue<string>("AzureAd:Audience");
    });

builder.Services.AddCors(options =>
    options.AddPolicy("AllowVueApp", builder =>
    {
        builder.WithOrigins("http://localhost:8080",
        "https://icy-rock-048660400.5.azurestaticapps.net")
                .AllowAnyMethod()
                .AllowAnyHeader();
    })
);

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient();

builder.Services.AddTransient<ApiUtility>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowVueApp");

app.UseHttpsRedirection();

app.UseMiddleware<JwtMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
