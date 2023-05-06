using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Models;
using System.Reflection;
using uniform_player.Domain.Interfaces.Services;
using uniform_player.Infrastructure.Exceptions;
using uniform_player.Infrastructure.General;
using uniform_player.Infrastructure.Middleware;
using uniform_player.Infrastructure.Services;

namespace uniform_player
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSwaggerGen(options =>
            {


                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Form player API",
                    Description = "ѕлеер форм дл€ проектов любой направленности"
                });

                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));


            });

            builder.Services.AddMvc(options => options.Filters.Add(typeof(ApiExceptionFilter)));
            builder.Services.AddControllers();
            builder.Services.AddTransient<IScenarioService, ScenarioService>();
            builder.Services.AddSingleton<ScenarioManager>();
            builder.Services.AddSingleton<TransitionManager>();
            var app = builder.Build();

            app.MapControllers();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseMiddleware<RequestIdentityMiddleware>();
            app.Run();
        }
    }
}