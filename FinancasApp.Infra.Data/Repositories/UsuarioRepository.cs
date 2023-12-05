using FinancasApp.Domain.Entities;
using FinancasApp.Domain.Interfaces.Repositories;
using FinancasApp.Infra.Data.Contexts;

namespace FinancasApp.Infra.Data.Repositories
{
    /// <summary>
    /// Repositorio de dados para entidade Usuario
    /// </summary>
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public Usuario? Get(string email)
        {
            using(var dataContext = new DataContext())
            {
                return dataContext.Set<Usuario>()
                    .Where(u => u.Email.Equals(email))
                    .FirstOrDefault();  
            }
        }

        public Usuario? Get(string email, string senha)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<Usuario>()
                    .Where(u => u.Email.Equals(email) && u.Senha.Equals(senha))
                    .FirstOrDefault();
            }
        }
    }
}
