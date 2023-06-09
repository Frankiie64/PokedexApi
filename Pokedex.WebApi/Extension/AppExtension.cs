using Swashbuckle.AspNetCore.SwaggerUI;

namespace Pokedex.WebApi.Extension
{
    public static class AppExtension
    {
        public static void UseSwggaerExtensions(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokedex API v1");
                opt.DefaultModelRendering(ModelRendering.Model);
            });
        }
    }
}
