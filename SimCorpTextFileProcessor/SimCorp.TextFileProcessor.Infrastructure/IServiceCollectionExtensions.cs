using Microsoft.Extensions.DependencyInjection;
using SimCorp.TextFileProcessor.Application.Interfaces.Infrastructure;

namespace SimCorp.TextFileProcessor.Infrastructure
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IFileReaderService, FileReaderService>();

            return services;
        }

    }
}
