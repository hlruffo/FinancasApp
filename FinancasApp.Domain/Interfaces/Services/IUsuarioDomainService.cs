using FinancasApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Interfaces.Services
{
    /// <summary>
    /// Interface de serviços de dominio para a entidade usuario
    /// </summary>
    public interface IUsuarioDomainService
    {
        /// <summary>
        /// Criar usuario no sistema
        /// </summary>
        /// <param name="usuario"></param>
        void CriarUsuario(Usuario usuario);

        /// <summary>
        /// Obter um usuario valido no sistema
        /// </summary>
        /// <param name="email"></param>
        /// <param name="senha"></param>
        Usuario Obter(string email, string senha);

    }

}
