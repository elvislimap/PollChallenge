using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PollChallenge.Infra.CrossCutting.Json;
using PollChallenge.Infra.Data.Contexts;

namespace PollChallenge.Service.Api.Configurations
{
    public static class ServicesApi
    {
        public static void RegisterServicesApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors();

            services.AddDbContext<ContextEf>(
                opt => opt.UseSqlServer(configuration.GetConnectionString("PollChallengeContext"),
                builder => builder.MigrationsAssembly("PollChallenge.Service.Api")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(opt => opt.SerializerSettings.ContractResolver = new CustomContractResolver());
        }
    }
}