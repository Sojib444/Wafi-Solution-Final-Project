using Kidoo.Learn.Enums;
using Kidoo.Learn.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Kidoo.Learn.Puzzles.Dtos;

public class PuzzleDto : EntityDto<Guid>
{
    public string PuzzleUrl { get; set; }
    public string PuzzleKey { get; set; }
    public ComplexityLevel ComplexityLevel { get; set; }
    public string ComplexityLevelString => ComplexityLevel.GetDisplayName();
    public int DisplayOrderId { get; set; }
    public Guid PuzzleCategoryId { get; set; }
    public string PuzzleCategoryName { get; set; }
    public string Description { get; set; }
    public string PuzzleText { get; set; }
    public string PuzzleFile { get; set; }
    public string Password { get; set; }
}
