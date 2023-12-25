using System.ComponentModel.DataAnnotations;

namespace Kidoo.Learn.Files.Dtos
{
    public class SaveBlobInputDto
    {
        [Required]
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public decimal Size { get; set; }
    }
}
