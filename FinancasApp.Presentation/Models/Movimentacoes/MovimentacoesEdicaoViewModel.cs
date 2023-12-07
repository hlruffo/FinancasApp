using FinancasApp.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FinancasApp.Presentation.Models.Movimentacoes
{
    public class MovimentacoesEdicaoViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Por favor, informar o nome da movimentação")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Por favor, descrever a movimentação")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Por favor, informar a data da movimentação")]
        public DateTime? Data { get; set; }

        [Required(ErrorMessage = "Por favor, informar o valor da movimentação")]
        public decimal? Valor { get; set; }

        [Required(ErrorMessage = "Por favor, selecionar o tipo da movimentação")]
        public int? Tipo { get; set; }

        /// <summary>
        /// Propriedade que popula o campo de seleção de categorias
        /// </summary>
        public List<SelectListItem>? ListagemCategorias { get; set; }

        /// <summary>
        /// Capturar o id da categoria selecionada
        /// </summary>
        [Required(ErrorMessage = "Por favor, selecionar o código da categoria")]
        public Guid? CategoriaId { get; set; }
    }

}
