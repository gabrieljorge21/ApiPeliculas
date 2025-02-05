using System.ComponentModel.DataAnnotations;

namespace ApiPelículas.Models.Dtos
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "'Name' field is required.")]
        [MaxLength(60, ErrorMessage = "'Name' field must have 60 chars as max lenght.")]
        [MinLength(2, ErrorMessage = "'Name' field must have 2 chars as min lenght.")]
        public string Name { get; set; }
    }
}
