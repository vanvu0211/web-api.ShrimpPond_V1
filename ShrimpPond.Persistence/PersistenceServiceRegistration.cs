using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShrimpPond.Application.Contract.Persistence;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Persistence.DatabaseContext;
using ShrimpPond.Persistence.Repository;
using ShrimpPond.Persistence.Repository.Generic;


namespace ShrimpPond.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ShrimpPondDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("ShrimpPond"));
            });
           

            services.AddScoped(typeof(IRepositoryBaseAsync<,>), typeof(RepositoryBase<,>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPondRepository, PondRepository>();
            services.AddScoped<ICertificateRepository, CertificateRepository>();
            services.AddScoped<ISizeShrimpRepository, SizeShrimpRepository>();
            services.AddScoped<IFoodFeedingRepository, FoodFeedingRepository>();
            services.AddScoped<IFoodForFeedingRepository, FoodForFeedingRepository>();
            services.AddScoped<IFoodRepository, FoodRepository>();
            services.AddScoped<IMedicineRepository, MedicineRepository>();
            services.AddScoped<ILossShrimpRepository, LossShrimpRepository>();
            services.AddScoped<IEnvironmentStatusRepository, EnvironmentStatusRepository>();
            services.AddScoped<IHarvestRepository, HarvestRepository>();
            services.AddScoped<IFarmRepository, FarmRepository>();
            services.AddScoped<ITimeSettingObjectRepository, TimeSettingObjectRepository>();
            services.AddScoped<ITimeSettingRepository, TimeSettingRepository>();

            return services;
        }
    }
}
