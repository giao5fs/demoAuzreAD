using adminApp.Data;
using adminApp.Models;
using Amazon.Extensions.NETCore.Setup;
using Amazon.S3;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AdminDbContext>(options =>
{
    options.UseInMemoryDatabase("AdminDatabase");
});

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true; // Enable for HTTPS requests
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "image/svg+xml" });
});

builder.Services.Configure<AWSOptions>(builder.Configuration.GetSection("AWS"));
builder.Services.AddAWSService<IAmazonS3>();

//-------------------------------------------
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AdminDbContext>();
    SeedDatabase(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseResponseCompression();
// app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=86400"); // 1 day
    }
});

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


void SeedDatabase(AdminDbContext context)
{
    context.Timesheets.AddRange(new List<Timesheet>
        {
            new Timesheet { EmployeeName = "John Doe", Date = DateTime.Now, HoursWorked = 8, TaskDescription = "Development" },
            new Timesheet { EmployeeName = "Jane Smith", Date = DateTime.Now.AddDays(-1), HoursWorked = 6, TaskDescription = "Testing" }
        });

    context.SaveChanges();
}