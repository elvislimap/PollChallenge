using Microsoft.Extensions.DependencyInjection;
using PollChallenge.Application.Interfaces;
using PollChallenge.Application.Services;
using PollChallenge.Domain.Interfaces.CrossCutting.Json;
using PollChallenge.Domain.Interfaces.Repositories.EFCore;
using PollChallenge.Domain.Interfaces.Services;
using PollChallenge.Domain.Services;
using PollChallenge.Infra.CrossCutting.Json;
using PollChallenge.Infra.Data.Repositories.EFCore;

namespace PollChallenge.Infra.CrossCutting.Ioc
{
    public static class ConfigStrapper
    {
        public static void RegisterServicesIoc(this IServiceCollection services)
        {
            #region EFCore repositories

            services.AddScoped<IPollEFRepository, PollEFRepository>();
            services.AddScoped<IPollOptionEFRepository, PollOptionEFRepository>();
            services.AddScoped<IVoteEFRepository, VoteEFRepository>();

            #endregion

            #region AppServices

            services.AddScoped<IPollAppService, PollAppService>();
            services.AddScoped<IVoteAppService, VoteAppService>();

            #endregion

            #region Services

            services.AddScoped<INotificationService, NotificationService>();

            #endregion

            #region CrossCutting

            services.AddScoped<ICustomContractResolver, CustomContractResolver>();

            #endregion
        }
    }
}