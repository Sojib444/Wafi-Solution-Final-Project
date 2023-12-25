using Kidoo.Learn.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kidoo.Learn.Questions.QuestionOptionDtos;

public class UpdateQuestionOptionDto
{
    public Guid Id { get; set; }
    public Options Options { get; set; }
    public string OptionText { get; set; }
}
