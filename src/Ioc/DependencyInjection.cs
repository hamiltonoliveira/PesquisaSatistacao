using Application.Interfaces;
using Application.Services;
using Infra.Interfaces;
using Infra.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraStructure(this IServiceCollection service, IConfiguration Configuration)
        {
            service.AddScoped<IAutenticarRepositorio, AutenticarRepositorio>();
            service.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            service.AddScoped<IEnqueteRepositorio, EnqueteRepositorio>();
            return service;
        }

        public static IServiceCollection AddServices(this IServiceCollection service, IConfiguration Configuration)
        {
            service.AddScoped<IAutenticarService, AutenticarService>();
            service.AddScoped<IUsuarioService, UsuarioService>();
            service.AddScoped<IEnqueteService, EnqueteService>();
            return service;
        }
    }
}
