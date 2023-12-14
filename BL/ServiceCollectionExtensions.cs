using BL.Implementation;
using BL.Interfaces;
using Dal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.profiles;

namespace BL
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IStationService, StationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICarService, CarService>();
            services.AddAutoMapper(typeof(UserAndUserDTO));
            services.AddAutoMapper(typeof(StationAndStationDTO));
            services.AddAutoMapper(typeof(CarAndCarDTO));
            services.AddRepositories();
        }
    }
}
