using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kidoo.Learn.PuzzleCategories
{
    public class CreateUpdatePuzzleCategoryDto
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
