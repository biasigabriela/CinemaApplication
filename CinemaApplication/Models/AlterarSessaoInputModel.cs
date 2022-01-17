using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApplication.Models
{
    public class AlterarSessaoInputModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Dia { get; set; }
        [Required]
        public string Horario { get; set; }
        [Required]
        public int QuantidadeLugares { get; set; }
        public int TotalOcupado { get; set; }
        public double Preco { get; set; }
      


    }
}
