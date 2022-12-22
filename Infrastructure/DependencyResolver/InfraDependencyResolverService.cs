using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Hangfire;
using Infrastructure.Hangfire.Jobs;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Infrastructure.Services.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
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
            services.AddScoped(typeof(ICachingService<>), typeof(CachingService<>)); 
            services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<BoilerPlateDbContext>();
            services.AddScoped<IIdentityService, IdentityService>();
            //services.AddAuthentication("Bearer").AddJwtBearer("Bearer",options =>
            //{
            //    options.Authority = Configuration["JWT:Authority"];
            //    options.RequireHttpsMetadata = false;
            //    options.Audience = "custom";
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateAudience = false,
            //    };
            //});
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(Configuration["JWT:key"]))
                };
            });
            //Hangfire
            services.AddHangfire(x =>
            {
                x.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseRedisStorage(Configuration["REDIS:ConnectionString"]);
            });
            services.AddHangfireServer();
            //Config Redis for caching
            services.AddStackExchangeRedisCache(options => { options.Configuration = Configuration["REDIS:ConnectionString"]; });
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