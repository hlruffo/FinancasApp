namespace FinancasApp.Presentation.Models.Home
{
    /// <summary>
    /// modelo de dados para a pagina principal do nosso projeto
    /// </summary>
    public class PainelPrincipalViewModel
    {
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public decimal? TotalReceitas { get; set; }
        public decimal? TotalDespesas { get; set; }
        public decimal? Saldo { get; set; }
        public decimal? Situacao { get; set; }
        public List<ChartViewModel>? GraficoDonut { get; set; }
        public List<ChartViewModel>? GraficoColunas { get; set; }

    }
    public class ChartViewModel
    {
        public string? Nome { get; set; }
        public decimal? Valor { get;set; }
    }

}
