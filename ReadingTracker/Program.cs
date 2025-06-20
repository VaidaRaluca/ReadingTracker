using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ReadingTracker.Core.Interfaces;
using ReadingTracker.Database;
using ReadingTracker.Database.Repos;
using ReadingTracker.Core.Services;
using ReadingTracker.Core;
using ReadingTracker.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Add Controllers
builder.Services.AddControllers();

// EF Core DbContext
builder.Services.AddDbContext<ReadingTrackerDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.MigrationsAssembly("ReadingTracker.Database")
    ));


// DI: Repositories & Services
builder.Services.AddScoped<IReaderRepo, ReaderRepo>();
builder.Services.AddScoped<IBookRepo, BookRepo>();
builder.Services.AddScoped<IReaderBookRepo, ReaderBookRepo>();
builder.Services.AddScoped<IReaderService, ReaderService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IReaderBookService, ReaderBookService>();

AppConfig.Init(builder.Configuration);


builder.Services.AddSingleton<AuthService>();


// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Reading Tracker API",
        Version = "v1",
        Description = "Reading API"
    });
});

// Mapping
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();



// Middleware
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseErrorHandlingMiddleware();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reading Tracker API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.MapControllers();
app.Run();