using Kidoo.Learn.Enums;
using System;
using Volo.Abp.Application.Dtos;

namespace Kidoo.Learn.Questions.QuestionOptionDtos;

public class QuestionOptionDto : EntityDto<Guid>
{
    public string OptionText { get; set; }
    public Options Options { get; set; }
    public bool IsAnswered { get; set; }
}
