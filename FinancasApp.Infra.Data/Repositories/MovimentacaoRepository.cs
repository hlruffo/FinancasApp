using FinancasApp.Domain.Entities;
using FinancasApp.Domain.Interfaces.Repositories;
using FinancasApp.Infra.Data.Contexts;

namespace FinancasApp.Infra.Data.Repositories
{
    /// <summary>
    /// classe de repositorio para movimentacao 
    /// </summary>
    public class MovimentacaoRepository : BaseRepository<Movimentacao>, IMovimentacaoRepository
    {
        public List<Movimentacao> Get(DateTime dataMin, DateTime dataMax, Guid usuarioId)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<Movimentacao>()
                    .Where(m => m.Data >= dataMin && m.Data <= dataMax && m.UsuarioId == usuarioId)
                    .OrderBy(m=> m.Data)
                    .ToList();
            }
        }
    }
}
