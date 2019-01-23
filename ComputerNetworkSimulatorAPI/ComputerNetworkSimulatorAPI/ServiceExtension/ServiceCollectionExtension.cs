using AutoMapper;
using ComputerNetworkSimulatorAPI.Interfaces;
using ComputerNetworkSimulatorAPI.Models.Database;
using ComputerNetworkSimulatorAPI.Models.SimulationDTO;
using ComputerNetworkSimulatorAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DependencyResolver
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCustomModules(this IServiceCollection services)
        {
            services.AddScoped<ITestFeature, TestFeature>();
            services.AddScoped<ISimulationService, SimulationService>();

            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            var connection = "Server=BARTEKP;Database=ComputerNetworkSimulator;Trusted_Connection=True;";
            services.AddDbContext<ComputerNetworkSimulatorContext>(options => options.UseSqlServer(connection));

            return services;
        }
    }
}
