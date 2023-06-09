using Pokedex.Core.Application;
using Pokedex.Infrastructure.Persistence;
using Pokedex.WebApi.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddApplicationLayer();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new GroupingByNamespaceConvention());
});

builder.Services.AddHealthChecks();
builder.Services.AddSwaggerExtensions();
builder.Services.AddApiVersioningExtension();

builder.Services.AddCors(options => options.AddPolicy("ApiCorsPolicy",
             builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwggaerExtensions();
}
else
{
    app.UseSwggaerExtensions();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
