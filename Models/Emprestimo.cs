using System;
using System.ComponentModel.DataAnnotations;

namespace Livraria.Models
{
    public class Emprestimo
    {
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um livro")]
        public int LivroId { get; set; }
        public Livro? Livro { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um funcionário")]
        public int FuncionarioId { get; set; }
        public Funcionario? Funcionario { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um cliente")]
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataEmprestimo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataPrevistaDevolucao { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DataDevolucao { get; set; }

        public string? Status { get; set; }
    }
}
