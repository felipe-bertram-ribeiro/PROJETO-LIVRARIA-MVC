using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Livraria.Models
{
    public class Emprestimo
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Livro")]
        public int LivroId { get; set; }

        [ForeignKey("LivroId")]
        public Livro? Livro { get; set; }

        [Required]
        [Display(Name = "Funcionário")]
        public int FuncionarioId { get; set; }

        [ForeignKey("FuncionarioId")]
        public Funcionario? Funcionario { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data do Empréstimo")]
        public DateTime DataEmprestimo { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [Display(Name = "Data de Devolução")]
        public DateTime? DataDevolucao { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; } = "Não devolvido";
    }
}
