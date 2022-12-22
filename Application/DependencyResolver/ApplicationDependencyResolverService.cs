using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Application.DependencyResolver
{
    public class ApplicationDependencyResolverService
    {
        public static void Register(IServiceCollection services, IConfiguration Configuration)
        {
            Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Debug()
           .WriteTo.File("logs/debug/debug_.txt", rollingInterval: RollingInterval.Day)
           .CreateLogger();
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
            services.AddOptions();
            services.AddMediatR(typeof(LibraryEntrypoint).Assembly);
        }
    }
}