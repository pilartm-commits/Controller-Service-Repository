using System.ComponentModel.DataAnnotations;

namespace Controller_Service_Repository.Models.DTOs.Requests
{
    public class ProductForCreateDto
    {
        [Required (ErrorMessage = "El nombre es requerido")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
        [Required (ErrorMessage = "El precio es requerido")]
        [Range (0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
       public decimal Price { get; set; }
    }
}
