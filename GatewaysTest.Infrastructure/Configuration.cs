using System.Reflection;
using GatewaysTest.Domain.Common.Model;
using GatewaysTest.Domain.Common.Repositories;
using GatewaysTest.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GatewaysTest.Infrastructure;

public static class Configuration
{
    public static IServiceCollection ConfigureRepositories(this IServiceCollection serviceCollection)
    {
        var infrastructureAssembly = typeof(Configuration).Assembly;
        var domainAssembly = typeof(IDomainRepository).Assembly;

        foreach (var i in domainAssembly
                     .GetTypes()
                     .Where(p
                         => p.IsPublic && !p.IsGenericType && p.GetInterface(nameof(IDomainRepository)) != null))
        foreach (var j in infrastructureAssembly
                     .GetTypes()
                     .Where(p => p.IsPublic && p.IsClass && p.GetInterface(i.Name) != null))
            AddCommonImplementationService(serviceCollection, i, j);

        return serviceCollection.ConfigureQueryFilterTranslators();
    }
    private static IServiceCollection ConfigureQueryFilterTranslators(this IServiceCollection serviceCollection)
    {
        var domainAssembly = typeof(IDomainRepository).Assembly;

        foreach (var i in domainAssembly
                     .GetTypes()
                     .Where(p
                         => p.IsPublic 
                            && p.IsClass 
                            && !p.IsGenericType 
                            && p.GetInterface(nameof(IQueryFilterTranslator)) != null))
            foreach (var j in i.GetInterfaces())
                if(j != typeof(IQueryFilterTranslator))
                    serviceCollection.AddCommonImplementationService(j, i);
           

        return serviceCollection;
    }
    public static IServiceCollection ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("DomainConnectionString");
        return services
            .AddDbContext<DomainContext>(options => { options.UseSqlServer(connection, 
                sqlOptions 
                    =>
                {
                    sqlOptions.EnableRetryOnFailure();
                    sqlOptions.CommandTimeout(120);
                }); 
            })
            .AddScoped<IUnitOfWork, UnitOfWork>();
    }
    private static void AddCommonImplementationService(this IServiceCollection services, Type service, 
        Type implementation)
    {
        ServiceDescriptor? descriptor;
        if ((descriptor = services.FirstOrDefault(p => p.ImplementationType == implementation)) != null)
        {
            if (descriptor.ServiceType == service)
                Console.WriteLine($"The service has already been registered: {service.FullName} - {implementation.FullName}");
            else
                services.AddScoped(service, p => p.GetRequiredService(descriptor.ServiceType));
        }
        else
            services.AddScoped(service, implementation);
    }
}