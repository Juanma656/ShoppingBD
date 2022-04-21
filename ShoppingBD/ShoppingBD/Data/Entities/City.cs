﻿using System.ComponentModel.DataAnnotations;

namespace ShoppingBD.Data.Entities
{
    public class City
    {
        public int Id { get; set; }

        [Display(Name = "Departamento/Estado")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]


        public string Name { get; set; }

        public State State { get; set; }
    }
}

