﻿using FinancasApp.Domain.Entities;
using FinancasApp.Domain.Enums;
using FinancasApp.Domain.Interfaces.Services;
using FinancasApp.Presentation.Models.Auth;
using FinancasApp.Presentation.Models.Movimentacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;

namespace FinancasApp.Presentation.Controllers
{
    [Authorize]
    public class MovimentacoesController : Controller
    {
        private ICategoriaDomainService? _categoriaDomainService;
        private IMovimentacaoDomainService? _movimentacaoDomainService;

        public MovimentacoesController(ICategoriaDomainService? categoriaDomainService, IMovimentacaoDomainService? movimentacaoDomainService)
        {
            _categoriaDomainService = categoriaDomainService;
            _movimentacaoDomainService = movimentacaoDomainService;
        }

        /// <summary>
        /// Método para abrir a página /Movimentacoes/Cadastro
        /// </summary>
        public IActionResult Cadastro()
        {
            var model = new MovimentacoesCadastroViewModel();
            model.ListagemCategorias = ObterCategorias();
            return View(model);
        }

        /// <summary>
        /// Método para receber o submit da página /Movimentacoes/Cadastro
        /// </summary>
        [HttpPost]
        public IActionResult Cadastro(MovimentacoesCadastroViewModel model)
        {
            //verificar se não há erros de validação
            if (ModelState.IsValid)
            {
                try
                {
                    //capturar os dados da movimentação
                    var movimentacao = new Movimentacao
                    {
                        Id = Guid.NewGuid(), //chave primária
                        Nome = model.Nome,
                        Data = model.Data,
                        Valor = model.Valor,
                        Descricao = model.Descricao,
                        Tipo = (TipoMovimentacao)model.Tipo,
                        CategoriaId = model.CategoriaId, //chave estrangeira
                        UsuarioId = ObterUsuarioAutenticado().Id //chave estrangeira
                    };

                    //gravar a movimentação
                    _movimentacaoDomainService?.Cadastrar(movimentacao);

                    TempData["MensagemSucesso"] = "Movimentação cadastrada com sucesso.";

                    //limpar os campos do formulário
                    model = new MovimentacoesCadastroViewModel();
                    ModelState.Clear();
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            model.ListagemCategorias = ObterCategorias();
            return View(model);
        }

        /// <summary>
        /// Método para abrir a página /Movimentacoes/Consulta
        /// </summary>
        public IActionResult Consulta()
        {
            var model = new MovimentacoesConsultaViewModel();

            try
            {
                //verificando se há datas gravadas em sessão
                if (HttpContext.Session.GetString("DataMin") != null
                    && HttpContext.Session.GetString("DataMax") != null)
                {
                    //capturar as datas armazenadas em sessão
                    model.DataMin = DateTime.Parse(HttpContext.Session.GetString("DataMin"));
                    model.DataMax = DateTime.Parse(HttpContext.Session.GetString("DataMax"));

                    //realizando a consulta de movimentações
                    model.ListagemMovimentacoes = _movimentacaoDomainService
                        .Consultar(model.DataMin.Value, model.DataMax.Value, ObterUsuarioAutenticado().Id.Value);
                }
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return View(model);
        }

        /// <summary>
        /// Método para receber o submit da página /Movimentacoes/Consulta
        /// </summary>
        [HttpPost]
        public IActionResult Consulta(MovimentacoesConsultaViewModel model)
        {
            //verificar se não há erros de validação
            if (ModelState.IsValid)
            {
                try
                {
                    //consultar as movimentações e armazenando os dados na classe Model
                    model.ListagemMovimentacoes = _movimentacaoDomainService?
                        .Consultar(model.DataMin.Value, model.DataMax.Value, ObterUsuarioAutenticado().Id.Value);

                    //gravar as datas em sessão
                    HttpContext.Session.SetString("DataMin", model.DataMin.ToString());
                    HttpContext.Session.SetString("DataMax", model.DataMax.ToString());
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            //retornando a model
            return View(model);
        }

        /// <summary>
        /// Método para abrir a página /Movimentacoes/Edicao
        /// </summary>
        public IActionResult Edicao(Guid id)
        {
            var model = new MovimentacoesEdicaoViewModel();
            try
            {
                var movimentacao = _movimentacaoDomainService?.ObterPorId(id);
                
                model.Nome = movimentacao.Nome;
                model.Data = movimentacao.Data;
                model.Valor = movimentacao.Valor;
                model.Descricao = movimentacao.Descricao;
                model.Tipo = (int)movimentacao.Tipo;
                model.CategoriaId = movimentacao.CategoriaId;            
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }
            model.ListagemCategorias = ObterCategorias();
            return View(model);
        }


        /// <summary>
        /// metodo para receber submit post do formulario de edição
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edicao(MovimentacoesEdicaoViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var movimentacao = new Movimentacao
                    {

                        Id = model.Id,
                        Nome = model.Nome,
                        Data = model.Data,
                        Valor = model.Valor,
                        Descricao = model.Descricao,
                        Tipo = (TipoMovimentacao?)model.Tipo,
                        CategoriaId = model.CategoriaId,
                        UsuarioId = ObterUsuarioAutenticado().Id
                    };
                    _movimentacaoDomainService?.Atualizar(movimentacao);
                    TempData["MensagemSucesso"] = "Movimentação Atualizada com sucesso.";
                    return RedirectToAction("Consulta");
                }
                catch (Exception e) 
                {
                    TempData["MensagemErro"] = e.Message;
                }

            }
            model.ListagemCategorias = ObterCategorias();
            return View(model);
        }

        /// <summary>
        /// Método para executar a exclusão  /Movimentacoes/Exclusao/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Exclusao(Guid id)
        {   
            try
            {
                //realiza a exclusão do registro
                _movimentacaoDomainService?.Excluir(id);
                TempData["MensagemSucesso"] = "Movimentação excluída com sucesso ";
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            //retornar para pagina de consulta
            return RedirectToAction("Consulta");
        }

        /// <summary>
        /// Método para gerar a lista de opções para preenchimento
        /// do campo de seleção de categorias
        /// </summary>
        private List<SelectListItem> ObterCategorias()
        {
            //consultar as categorias cadastradas no sistema
            var categorias = _categoriaDomainService?.Consultar();

            //criando uma lista de opções de seleção
            var lista = new List<SelectListItem>();

            //percorrer todas as categorias obtidas
            foreach (var item in categorias)
            {
                //adicionando um item na lista de opções do campo
                lista.Add(new SelectListItem
                {
                    Value = item.Id.ToString(), //valor do campo
                    Text = item.Nome //texto exibido no campo
                });
            }

            return lista;
        }

        /// <summary>
        /// Método para retornar o usuário autenticado no AspNet (Cookie)
        /// </summary>
        private UserAuthViewModel ObterUsuarioAutenticado()
        {
            //ler os dados do usuário gravado no arquivo de cookie (json)
            var data = User.Identity.Name;

            //deserializar os dados
            return JsonConvert.DeserializeObject<UserAuthViewModel>(data);
        }
    }
}



