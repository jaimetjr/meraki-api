using Application.DTOs;
using Application.Interfaces;
using Application.Mapping;
using Application.Services;
using Application.Validators;
using Domain.Interfaces;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, string connectionString)
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
            services.AddScoped<ITestimonialRepository, TestimonialRepository>();

            // Automapper configuration
            services.AddAutoMapper(c => c.AddProfile<AutoMapperProfile>());

            // Services layer registrations 
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<ITherapistService, TherapistService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBenefitService, BenefitService>();
            services.AddScoped<ISpecialtyService, SpecialtyService>();
            services.AddScoped<ITestimonialService, TestimonialService>();

            // Validators
            services.AddScoped<IValidator<ServiceDto>, ServiceValidator>();
            services.AddScoped<IValidator<TherapistDto>, TherapistValidator>();
            services.AddScoped<IValidator<CourseDto>, CourseValidator>();
            services.AddScoped<IValidator<CategoryDto>, CategoryValidator>();
            services.AddScoped<IValidator<BenefitDto>, BenefitValidator>();
            services.AddScoped<IValidator<SpecialtyDto>, SpecialtyValidator>();
            services.AddScoped<IValidator<TestimonialDto>, TestimonialValidator>();

            return services;
        }
    }

}
