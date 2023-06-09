using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace Pokedex.WebApi.Extension
{
    public static class ServiceExtension
    {
        public static void AddSwaggerExtensions(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFiles => opt.IncludeXmlComments(xmlFiles));

                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MySmartEdu API",
                    Description = "Esta es la primera version del API de MySmartEdu, en lo adelante estara sujeto a cambio hasta que finalicemos con esta primera version del aplicativo.",
                    Contact = new OpenApiContact
                    {
                        Name = "Franklyn Brea",
                        Email = "franklynbrea100@gmail.com"
                        //Url = new Uri("https://www.itla.edu.do")
                    }
                });
               

            });
        }
        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });

        }

    }
}
       


