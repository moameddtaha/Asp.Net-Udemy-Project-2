using System.Reflection;
using CitiesManager.WebAPI.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json")); // Sets default response format to JSON
    options.Filters.Add(new ConsumesAttribute("application/json")); // Sets default request format to JSON
})
    .AddXmlSerializerFormatters(); // Adds XML serialization support

builder.Services.AddApiVersioning(config =>
{
    config.ApiVersionReader = new UrlSegmentApiVersionReader(); // Reads API version from the URL segment (e.g., /api/v1/cities)

    //config.ApiVersionReader = new QueryStringApiVersionReader("api-version"); // Reads API version from the query string (e.g., /api/cities?api-version=1.0)

    //config.ApiVersionReader = new HeaderApiVersionReader("api-version"); // Reads API version from the request header (e.g., api-version: 1.0)

    //config.DefaultApiVersion = new ApiVersion(1, 0); // Sets the default API version to 1.0
    //config.AssumeDefaultVersionWhenUnspecified = true; // Assumes the default version when no version is specified

}); // Enables API versioning, allowing multiple versions of the API to coexist

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); // Generates description of all endpoints in the application
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments("api/xml");

    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "Cities Web API",
        Version = "1.0",
    });
    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "Cities Web API",
        Version = "2.0",
    });
}); // Generates Swagger documentation for the OpenAPI

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // Formats the version number in the group name
    options.SubstituteApiVersionInUrl = true; // Substitutes the API version in the URL
}); // Adds support for versioned API documentation

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// CORS: localhost:4200
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200");
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // creates endpoint for swagger.json
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Cities Web API v1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "Cities Web API v2");
        //options.RoutePrefix = string.Empty; // Sets Swagger UI at the root URL
    }); // creates swagger UI for testing all endpoints/ action methods
}

app.UseHsts();

app.UseHttpsRedirection();

app.UseCors(); // Enables CORS policy defined above

app.UseAuthorization();

app.MapControllers();

app.Run();
