using CinemaApplication.Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApplication.Models
{
    public class NovoFilmeInputModel
    {
        [Required]
        [MinLength(2)]
        public string Titulo { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Por favor, informe um tempo de duração ")]
        public string Duracao { get; set; }
        [Required]
        [MinLength(10)]
        public string Sinopse { get; set; }
    }
}
