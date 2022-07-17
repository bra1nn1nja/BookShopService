using BookShopService.Domain.Models;
using BookShopService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BookShopService.API.Facades;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Swashbuckle.AspNetCore.Filters;
using BookShopService.API.Swagger.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IBookShopFacade, BookShopFacade>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

if (!builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseInMemoryDatabase("InMem"));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("BooksConnection")));
}

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
    
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.EnableAnnotations();
        options.SwaggerDoc(
            "v1", 
            new OpenApiInfo 
            { 
                Title = "Books API", 
                Version = "1.0.0", 
                Description = "API that manages a collection of books in a fictional store" 
            });
        options.IncludeXmlComments(xmlFilename);
        options.ExampleFilters();
        options.ParameterFilter<SortByParameterFilter>();
    });

builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Books API");
        options.DocumentTitle = "Books API";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

PrepareDatabase.Populate(app, app.Environment.IsProduction());

app.Run();
