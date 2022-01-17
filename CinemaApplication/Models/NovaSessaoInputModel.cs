using CinemaApplication.Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApplication.Models
{
    public class NovaSessaoInputModel
    {
        [Required]
        [Range(1, 31, ErrorMessage = "Por favor, informe uma data")]
        public string Dia { get; set; }
        [Required]
        [Range(0, 23, ErrorMessage = "Por favor, informe um horario válido")]
        public string Horario { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Por favor, informe uma quantidade válida de lugares")]
        public int QuantidadeLugares { get; set; }
        [Required]
        public int TotalOcupado { get; set; }
        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Por favor, informe um valor de ingresso")]
        public double Preco { get; set; }
        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Por favor, informe um filme")]
        public string FilmeExibicao { get; set; }
        [Required]
        public string AtualizarHash { get; set; }
    }
}
