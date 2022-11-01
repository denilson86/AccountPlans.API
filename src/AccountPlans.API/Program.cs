using Domain.Repository;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);
var desenvEnvironment = "Development";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositoriesIoC(builder.Configuration);

var app = builder.Build();
var provider = app.Services.GetService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment(desenvEnvironment))
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        if (provider is not null)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(
                    $"../swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
