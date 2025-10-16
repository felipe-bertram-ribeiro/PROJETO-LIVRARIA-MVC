using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Livraria.Models
{
    public class Emprestimo
    {
        public int Id { get; set; }

        [Required]
        public int LivroId { get; set; }

        [ForeignKey("LivroId")]
        public Livro Livro { get; set; }

        [Required]
        public int FuncionarioId { get; set; }

        [ForeignKey("FuncionarioId")]
        public Funcionario Funcionario { get; set; }

        public DateTime DataEmprestimo { get; set; } = DateTime.Now;

        public DateTime DataDevolucaoPrevista { get; set; }

        public DateTime? DataDevolucaoReal { get; set; }
    }
}
