using System.ComponentModel.DataAnnotations;

namespace ShoppingBD.Models
{
    public class CityViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Departamento/Estado")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]


        public string Name { get; set; }

        public int StateId { get; set; }
    }
}
