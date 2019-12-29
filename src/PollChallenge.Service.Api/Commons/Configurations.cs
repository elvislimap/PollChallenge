using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using PollChallenge.Infra.Data.Contexts;

namespace PollChallenge.Service.Api.Commons
{
    public static class Configurations
    {
        public static void RegisterServicesApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors();

            services.AddDbContext<ContextEf>(
                opt => opt.UseSqlServer(configuration.GetConnectionString("PollChallengeContext"),
                builder => builder.MigrationsAssembly("PollChallenge.Service.Api")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                    var resolver = opt.SerializerSettings.ContractResolver as CamelCasePropertyNamesContractResolver;
                    resolver.NamingStrategy = new SnakeCaseNamingStrategy();
                });
        }
    }
}