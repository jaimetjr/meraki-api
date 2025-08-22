using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            // Register DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString)); // Or UseNpgsql for PostgreSQL

            // Register generic repository (optional if used directly)
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Register domain-specific repositories
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<ITherapistRepository, TherapistRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBenefitRepository, BenefitRepository>();
            services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();

            return services;
        }
    }

}
