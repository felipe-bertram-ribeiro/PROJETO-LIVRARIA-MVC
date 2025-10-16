using System.ComponentModel.DataAnnotations;

namespace Livraria.Models
{
    public class Funcionario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(255)]
        public string SenhaHash { get; set; }

        [Required]
        [StringLength(50)]
        public string Cargo { get; set; } = "Atendente";
    }
}
