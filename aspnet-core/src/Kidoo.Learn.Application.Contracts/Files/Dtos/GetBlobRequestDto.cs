using System.ComponentModel.DataAnnotations;

namespace Kidoo.Learn.Files.Dtos
{
    public class GetBlobRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
