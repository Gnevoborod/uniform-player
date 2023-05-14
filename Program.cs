using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Models;
using System.Reflection;
using uniform_player.Domain.Interfaces.General;
using uniform_player.Domain.Interfaces.Services;
using uniform_player.Infrastructure.Exceptions;
using uniform_player.Infrastructure.General;
using uniform_player.Infrastructure.Middleware;
using uniform_player.Infrastructure.Services;
using NLog;
using NLog.Web;
using Microsoft.EntityFrameworkCore.Query;
using uniform_player.Database;
using uniform_player.Domain.Interfaces.Repositories;
using uniform_player.Infrastructure.Repositories;

namespace uniform_player
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
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
            //builder.Logging.ClearProviders();
            builder.Host.UseNLog();

            builder.Services.AddDbContext<UniformPlayerDbContext>(ServiceLifetime.Scoped);
            builder.Services.AddMvc(options => options.Filters.Add(typeof(ApiExceptionFilter)));
            builder.Services.AddControllers();
            builder.Services.AddTransient<IScenarioService, ScenarioService>();
            builder.Services.AddSingleton<IScenarioManager,ScenarioManager>();
            builder.Services.AddSingleton<ITransitionManager,TransitionManager>();
            builder.Services.AddScoped<IScenarioRepository, ScenarioRepository>();
            builder.Services.AddScoped<IMover, Mover>();
            var app = builder.Build();

            app.MapControllers();
            app.UseSwagger();
            app.UseSwaggerUI( c =>
{
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAPI");
                c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
            });
            app.UseStaticFiles();
            app.UseMiddleware<RequestIdentityMiddleware>();
            app.Run();

           
        }
    }
}