using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApplication.Models
{
    public class AlterarFilmeInputModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Duracao { get; set; }
        [Required]
        public string Sinopse { get; set; }

    }
}
