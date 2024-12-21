using VibetexApp.Domain.Interfaces.Repositories;
using VibetexApp.Domain.Interfaces.Services;
using VibetexApp.Domain.Services;
using VibetexApp.Infra.Data.Repositories;

namespace VibetexApp.API.Configuration
{
    public class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjection(IServiceCollection services)
        {
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IPontoService,PontoService>();
            services.AddTransient<IPontoRepository, PontoRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
        }
    }
}
