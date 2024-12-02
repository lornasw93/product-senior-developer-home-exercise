using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace UKParliament.CodeTest.Data;

public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterDataServices(this IServiceCollection services)
    {
        services.AddDbContext<PersonManagerContext>(op => op.UseInMemoryDatabase("PersonManager"));

        // var app = builder.Build();

        //using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        //{
        //    using var context = serviceScope.ServiceProvider.GetRequiredService<PersonManagerContext>();
        //    context.Database.EnsureCreated();
        //}

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        //services.AddScoped<IPersoRepository, Repository>();
        //services.AddScoped<IDepartmentRepository, DepartmentRepository>();

        return services;
    }
}
