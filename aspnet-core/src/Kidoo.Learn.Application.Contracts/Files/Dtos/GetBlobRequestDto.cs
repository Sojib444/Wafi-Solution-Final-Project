using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kidoo.Learn.Files.Dtos
{
    public class GetBlobRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
