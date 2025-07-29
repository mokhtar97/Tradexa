using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Tradexa.Application.Interfaces;
using Tradexa.Infrastructure.Persistence;
using Tradexa.Domain.Entities;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Tradexa.Infrastructure.Services;
using static Microsoft.IO.RecyclableMemoryStreamManager;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);

// ------------ Serilog + Seq Logging ------------
builder.Host.UseSerilog((context, config) =>
{
    config
        .Enrich.FromLogContext()
        //.Enrich.WithCorrelationId()
        .WriteTo.Console()
        .WriteTo.Seq("http://localhost:5341"); // Update if hosted elsewhere
});

// ------------ Database + Identity ------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAutoMapper(typeof(Program));

// ------------ Localization ------------
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Uncomment to enable localization middleware
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en"),
        new CultureInfo("ar")
    };

    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

// ------------ CORS Policy ------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ------------ Controllers + JSON + Annotations ------------
builder.Services.AddControllers()
    .AddDataAnnotationsLocalization()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

// ------------ Swagger ------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ------------ Authentication / Authorization ------------
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// ------------ Dependency Injection (Services) ------------
 builder.Services.AddScoped<IProductService, ProductService>();
 builder.Services.AddScoped<ICategoryService, CategoryService>();
 builder.Services.AddScoped<IUserService, UserService>();
 builder.Services.AddScoped<IInvoiceService, InvoiceService>();
 builder.Services.AddScoped<IReportService, ReportService>();
 //builder.Services.AddScoped<ILayoutPreferenceService, LayoutPreferenceService>();

// ------------ Logging + HttpContext ------------
builder.Services.AddHttpContextAccessor();
// builder.Services.AddScoped<ILoggingService, LoggingService>();

var app = builder.Build();

// ------------ Middleware Pipeline ------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tradexa API V1");
        c.RoutePrefix = string.Empty; // This makes Swagger UI the root page
    });
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

// Uncomment if localization is enabled
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseSerilogRequestLogging();
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
