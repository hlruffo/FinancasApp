using System.ComponentModel.DataAnnotations;

namespace FinancasApp.Presentation.Models.Account
{
    public class RegisterViewModel
    {
        [MinLength(8, ErrorMessage = "Por favor informe mínimo {1} caractéres.")]
        [MaxLength(100, ErrorMessage = "Por favor, informe no máximo {1} caractéres")]
        [Required(ErrorMessage = "Por favor, informe seu nome.")]
        public string? Nome { get; set; }

        [EmailAddress(ErrorMessage = "Por favor, informe um email válido.")]
        [Required(ErrorMessage = "Por favor, informe um email de acesso.")]
        public string? Email { get; set; }


        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$", ErrorMessage ="Utilize uma senha forte com maiusculas, minusculas, simbolos e números, tendo 8 caractéres no mínimo.")]
        [Required(ErrorMessage = "Por favor, informe sua senha.")]
        public string? Senha { get; set; }

        [Compare("Senha",ErrorMessage ="Senhas não conferem.")]
        [Required(ErrorMessage = "Por favor, confirme sua senha.")]
        public string? SenhaConfirmacao { get; set; }
    }
}
