using Kidoo.Learn.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kidoo.Learn.Puzzles.Dtos
{
    public class CreateUpdatePuzzleDto
    {
        public string PuzzleUrl { get; set; }
        public string PuzzleKey { get; set; }
        public ComplexityLevel ComplexityLevel { get; set; }
        public int DisplayOrderId { get; set; }
        public Guid PuzzleCategoryId { get; set; }
        public string Description { get; set; }
        public string PuzzleText { get; set; }
        public string PuzzleFile { get; set; }
        public string Password { get; set; }
    }
}
