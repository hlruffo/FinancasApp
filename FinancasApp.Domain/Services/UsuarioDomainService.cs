using FinancasApp.Domain.Entities;
using FinancasApp.Domain.Helpers;
using FinancasApp.Domain.Interfaces.Repositories;
using FinancasApp.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Services
{

        
    /// <summary>
    /// Classe para implementar os serviços de dominio de usuario
    /// </summary>
    public class UsuarioDomainService : IUsuarioDomainService
    {
        //atributo
        private readonly IUsuarioRepository? _usuarioRepository;

        //metodo para gerar a instancia do atributo
        public UsuarioDomainService(IUsuarioRepository? usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public void CriarUsuario(Usuario usuario)
        {
            //verificar se já existe usuario cadastrado com email informado
            if(_usuarioRepository?.Get(usuario.Email) != null)
            {
                throw new ApplicationException("O email informado já está cadastrado. Por favor, verifique.");
            }
            //gerando um id para o usuario 
            usuario.Id = Guid.NewGuid();

            //gearando data e hora de criação 
            usuario.DataHoraCriacao = DateTime.Now;

            //criptografando senha do usuario
            usuario.Senha = SHA1CryptoHelper.Encrypt(usuario.Senha);

            //gravando o usuário no banco de dados
            _usuarioRepository?.Add(usuario);

        }

        public Usuario Obter(string email, string senha)
        {
            var usuario = _usuarioRepository?.Get(email, SHA1CryptoHelper.Encrypt(senha));
            if(usuario == null)             
                throw new ApplicationException("Usuário não encontrado.");

            return usuario;
        }

       
    }
}
