using Kidoo.Learn.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kidoo.Learn.Questions.QuestionOptionDtos;

public class CreateUpdateQuestionOptionDto
{
    public string OptionText { get; set; }
    public Options options { get; set; }
}
