using Infraestructure.Core.Data;
using Infraestructure.Core.Repository;
using Infraestructure.Core.Repository.Interface;
using Infraestructure.Core.UnitOfWork;
using Infraestructure.Core.UnitOfWork.Interface;
using Logistic.Domain.Services.Logistic;
using Logistic.Domain.Services.Logistic.Interfaces;

namespace Logistic.Handlers
{
    public static class DependencyInyectionHandler
    {
        public static void DependencyInyectionConfig(IServiceCollection services)
        {
            // Infrastructure
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<SeedDb>();

            //Domain
            services.AddTransient<IMaritimeLotServices, MaritimeLotServices>();
            services.AddTransient<ILandLotServices, LandLotServices>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<ITypeProductServices, TypeProductServices>();
            services.AddTransient<IClientServices, ClientServices>();
            services.AddTransient<ISeaportServices, SeaportServices>();
            services.AddTransient<IWarehouseServices, WarehouseServices>();
        }
    }
}
