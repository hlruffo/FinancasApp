using System.ComponentModel.DataAnnotations;

namespace FinancasApp.Presentation.Models.Account
{
    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage ="Por favor, informe um email válido.")]
        [Required(ErrorMessage ="Por favor, informe o email de acesso.")]
        public string? Email { get; set; }


        [MinLength(8,ErrorMessage ="Sua senha deve conter no mínimo {1} caractéres.")]
        [MaxLength(20, ErrorMessage ="Por favor, informe no máximo {1} caractéres")]
        [Required(ErrorMessage = "Por favor, informe sua senha.")]
        public string? Senha { get; set; }
    }
}
