using System;
using System.ComponentModel.DataAnnotations;

namespace Livraria.Models
{
    public class Emprestimo
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Livro")]
        public int LivroId { get; set; }
        public Livro? Livro { get; set; }

        [Required]
        [Display(Name = "Funcionário")]
        public int FuncionarioId { get; set; }
        public Funcionario? Funcionario { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Empréstimo")]
        public DateTime DataEmprestimo { get; set; }

        // Mantemos esta como required no model (se o banco não permite nulls),
        // mas o controller garante um valor antes de salvar.
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data Prevista de Devolução")]
        public DateTime DataPrevistaDevolucao { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Devolução")]
        public DateTime? DataDevolucao { get; set; }

        [Display(Name = "Status")]
        public string? Status { get; set; }
    }
}
