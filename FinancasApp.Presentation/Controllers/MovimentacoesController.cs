using FinancasApp.Domain.Entities;
using FinancasApp.Domain.Enums;
using FinancasApp.Domain.Interfaces.Services;
using FinancasApp.Presentation.Models.Auth;
using FinancasApp.Presentation.Models.Movimentacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace FinancasApp.Presentation.Controllers
{
    [Authorize]
    public class MovimentacoesController : Controller
    {

        private ICategoriaDomainService? _categoriaDomainService;
        private IMovimentacaoDomainService _movimentacaoDomainService;

        public MovimentacoesController(ICategoriaDomainService? categoriaDomainService, IMovimentacaoDomainService movimentacaoDomainService)
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
            model.ListagemCategorias = obterCategorias();
            return View(model);
        }

        /// <summary>
        /// Método para receber o submit da página /Movimentacoes/Cadastro
        /// </summary>
        [HttpPost]
        public IActionResult Cadastro(MovimentacoesCadastroViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var movimentacao = new Movimentacao
                    {
                        Id = Guid.NewGuid(),
                        Nome = model.Nome,
                        Data = model.Data,
                        Valor = model.Valor,
                        Descricao = model.Descricao,
                        Tipo = (TipoMovimentacao)model.Tipo,
                        CategoriaId = model.CategoriaId,
                        UsuarioId = ObterUsuarioAutenticado().Id
                    };
                    //gravar movimentacao
                    _movimentacaoDomainService?.Cadastrar(movimentacao);
                    TempData["MensagemSucesso"] = "Movimentação cadastrada com sucesso.";


                    //limpar campos do cadastro.
                    model = new MovimentacoesCadastroViewModel();
                    ModelState.Clear();
                }
                catch (Exception e) 
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            model.ListagemCategorias = obterCategorias();
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
                //verifica se há data em sessão 
                if ( HttpContext.Session.GetString("DataMin")!=null &&
                    HttpContext.Session.GetString("DataMax") != null)
                {
                    
                    model.DataMin = DateTime.Parse(HttpContext.Session.GetString("DataMin"));
                    model.DataMax = DateTime.Parse(HttpContext.Session.GetString("DataMax"));

                    //realiza consulta
                    model.ListagemMovimentacoes = _movimentacaoDomainService.Consultar(model.DataMin.Value, model.DataMax.Value, ObterUsuarioAutenticado().Id.Value);
                }

            }
            catch(Exception e)
            {
                TempData["MensagemErro"]=e.Message;
            }
            return View();
        }

        /// <summary>
        /// Método para receber o submit da página /Movimentacoes/Consulta
        /// </summary>
        [HttpPost]
        public IActionResult Consulta(MovimentacoesConsultaViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    //consultar as movimentações
                    model.ListagemMovimentacoes = _movimentacaoDomainService?
                        .Consultar(model.DataMin.Value, model.DataMax.Value, ObterUsuarioAutenticado().Id.Value);


                    //gravar datas em sessão
                    HttpContext.Session.SetString("DataMin", model.DataMin.ToString());
                    HttpContext.Session.SetString("DataMax", model.DataMax.ToString());

                }
                catch (Exception e) 
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }
            return View(model);
        }

        /// <summary>
        /// Método para abrir a página /Movimentacoes/Edicao
        /// </summary>
        public IActionResult Edicao()
        {
            return View();
        }

        /// <summary>
        /// metodo para gerar a lista de opções para preenchimento do campo de seleção de categorias
        /// </summary>
        /// <returns> lista de categorias cadastrada no banco de dados</returns>
        private List<SelectListItem> obterCategorias()
        {
            //consultar categorias cadastradas no sistemas
            var categorias = _categoriaDomainService?.Consultar();

            //criando lista de opções de seleção 
            var lista = new List<SelectListItem>();

            //percorrer categorias 
            foreach(var categoria in categorias)
            {
                lista.Add(new SelectListItem
                {
                    Value = categoria.Id.ToString(), //valor do campo
                    Text = categoria.Nome //texto exibido no campo 
                });
            }
            return lista;
        }

        /// <summary>
        /// metodo para retornar o usuario autenticado no aspnet (cookie)
        /// </summary>
        /// <returns></returns>
        private UserAuthViewModel ObterUsuarioAutenticado()
        {
            //ler os dados do arquivo cookie
            var data = User.Identity.Name;

            //deserealizar e retornar
            return JsonConvert.DeserializeObject<UserAuthViewModel>(data);
        }

    }
}



