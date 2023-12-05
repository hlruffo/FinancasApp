using FinancasApp.Domain.Entities;

namespace FinancasApp.Domain.Interfaces.Services
{
    /// <summary>
    /// Interface de serviços de dominio para a entidade de categoria
    /// </summary>
    public interface ICategoriaDomainService
    {
        List<Categoria> Consultar();
    }
}
