using Domain.Interfaces;
using Hangfire;
using Hangfire.MemoryStorage;
using Hangfire.MySql;
using Hangfire.Redis;
using IdentityServer4.AccessTokenValidation;
using Infrastructure.Hangfire.Jobs;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.DependencyResolver
{
    public static class InfraDependencyResolverService
    {
        public static void Register(IServiceCollection services, IConfiguration Configuration)
        {
            var connectionString = Configuration.GetConnectionString("MyConnectionString");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            services.AddDbContext<BoilerPlateDbContext>(opt => opt.UseMySql(connectionString, serverVersion));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddAuthentication("Bearer").AddJwtBearer("Bearer",options =>
            {
                options.Authority = Configuration["JWT:Authority"];
                options.RequireHttpsMetadata = false;
                options.Audience = "custom";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ////////////////////////////////////////////////////////
                    // The following made the difference.  
                    ////////////////////////////////////////////////////////
                    ValidateAudience = false,
                };
            });
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddIdentityServerAuthentication(opt =>
            //{
            //    opt.RequireHttpsMetadata = false;
            //    opt.Authority = Configuration["JWT:Authority"]; // IdP
            //    opt.ApiName = "custom"; //  api resource name
            //    opt.ApiSecret = Configuration["JWT:Secret"];
            //});
            services.AddHangfire(x =>
            {
                x.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                //.UseStorage(
                //    new MySqlStorage(
                //        Configuration.GetConnectionString("MyConnectionString"),
                //        new MySqlStorageOptions
                //        {
                //            TablesPrefix = "Hangfire"
                //        }
                //    )
                //);
                //.UseMemoryStorage();
                .UseRedisStorage(Configuration["REDIS:ConnectionString"]);
            });
            services.AddHangfireServer();
            //RecurringJob.AddOrUpdate<Jobs>("RecurringSendGetRequest", x => x.SendGetRequest(), Cron.Minutely());
        }
        public static void Configure() 
        {
            RecurringJob.AddOrUpdate<Jobs>("g1", job => job.SendGetRequest(), Cron.Minutely());
        }
      
        public class MysqlEntityFrameworkDesignTimeServices : IDesignTimeServices
        {
            public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
            {
                serviceCollection.AddEntityFrameworkMySql();
                new EntityFrameworkRelationalDesignServicesBuilder(serviceCollection)
                    .TryAddCoreServices();
            }
        }

    }
}