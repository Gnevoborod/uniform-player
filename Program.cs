using Microsoft.AspNetCore.Mvc.Filters;
using uniform_player.Domain.Interfaces.Services;
using uniform_player.Infrastructure.Exceptions;
using uniform_player.Infrastructure.Middleware;
using uniform_player.Infrastructure.Services;

namespace uniform_player
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSwaggerGen();

            builder.Services.AddMvc(options => options.Filters.Add(typeof(ApiExceptionFilter)));
            builder.Services.AddControllers();
            builder.Services.AddTransient<IScenarioService, ScenarioService>();
            var app = builder.Build();

            app.MapControllers();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseMiddleware<RequestIdentityMiddleware>();
            app.Run();
        }
    }
}