using Microsoft.Extensions.DependencyInjection;

namespace UKParliament.CodeTest.Services;

public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterCoreServices(this IServiceCollection services)
    {
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<IDepartmentService, DepartmentService>();

        return services;
    }
}
