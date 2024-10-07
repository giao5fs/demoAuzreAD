using api1.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//somrhing

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration.GetValue<string>("AzureAd:Authority");
        options.Audience = builder.Configuration.GetValue<string>("AzureAd:Api1ClientId");
    });

builder.Services.AddCors(options =>
    options.AddPolicy("AllowVueApp", builder =>
    {
        builder.WithOrigins("http://localhost:8080", "https://ambitious-river-015f84110.5.azurestaticapps.net/")
                .AllowAnyHeader()
                .AllowAnyMethod();
    })
);

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient();

builder.Services.AddTransient<ApiUtility>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowVueApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
