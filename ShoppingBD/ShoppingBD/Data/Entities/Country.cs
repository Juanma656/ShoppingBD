using System.ComponentModel.DataAnnotations;

namespace ShoppingBD.Data.Entities
{
    public class Country
    {
        public int Id { get; set; }

        [Display(Name = "Pais")]
        [MaxLength(50,ErrorMessage ="El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El capo {0} es obligatorio.")]
        
        
        public string Name { get; set; }
    }
}
