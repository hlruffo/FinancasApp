using FinancasApp.Domain.Entities;
using FinancasApp.Domain.Interfaces.Repositories;

namespace FinancasApp.Infra.Data.Repositories
{
    /// <summary>
    /// classe de repositório para categoria
    /// </summary>
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
    }
}
