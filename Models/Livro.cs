﻿using System.ComponentModel.DataAnnotations;

namespace Livraria.Models
{
    public class Livro
    {
        public int Id { get; set; }

        [Required, StringLength(150)]
        public string Titulo { get; set; }

        [Required, StringLength(100)]
        public string Autor { get; set; }

        [StringLength(50)]
        public string Genero { get; set; }

        [StringLength(20)]
        public string ISBN { get; set; }

        public int QuantidadeEstoque { get; set; }

        public string? CapaUrl { get; set; }
    }
}
