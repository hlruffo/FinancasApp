using FinancasApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Interface para repositório de movimentacao
    /// </summary>
    public interface IMovimentacaoRepository :IBaseRepository<Movimentacao>
    {
        /// <summary>
        /// Método para consultar uma lista de movimentações de acordo com um período de tempo para
        /// o usuário atualmente logado no sistema.
        /// </summary>
        /// <param name="dataMin"></param>
        /// <param name="dataMax"></param>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        List<Movimentacao> Get(DateTime dataMin, DateTime dataMax, Guid usuarioId);
    }
}
