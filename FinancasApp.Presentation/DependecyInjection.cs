using FinancasApp.Domain.Interfaces.Repositories;
using FinancasApp.Domain.Interfaces.Services;
using FinancasApp.Domain.Services;
using FinancasApp.Infra.Data.Repositories;

namespace FinancasApp.Presentation
{
    public static class DependecyInjection
    {
        /// <summary>
        /// Método para configurar as injeções de dependecia do sistema
        /// </summary>
        
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IUsuarioDomainService, UsuarioDomainService>();
            services.AddTransient<ICategoriaDomainService, CategoriaDomainService>();
            services.AddTransient<IMovimentacaoDomainService, MovimentacaoDomainService>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<IMovimentacaoRepository, MovimentacaoRepository>();




        }
    }
}
