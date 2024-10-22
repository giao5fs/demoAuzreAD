using System.Text;
using api3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://cognito-idp.us-east-1.amazonaws.com/us-east-1_YIW9yXf7G";
        options.MetadataAddress = "https://cognito-idp.us-east-1.amazonaws.com/us-east-1_YIW9yXf7G/.well-known/openid-configuration";
        options.IncludeErrorDetails = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,

        };
    });

builder.Services.AddCors(options =>
    options.AddPolicy("AllowVueApp", builder =>
    {
        builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
    })
);

builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowVueApp");

// app.UseHttpsRedirection();

app.UseMiddleware<JwtMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();