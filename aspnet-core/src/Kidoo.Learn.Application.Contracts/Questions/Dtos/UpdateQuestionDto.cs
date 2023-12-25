using Kidoo.Learn.Enums;
using Kidoo.Learn.Questions.QuestionOptionDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kidoo.Learn.Questions.Dtos;

public class UpdateQuestionDto
{
    public string Title { get; set; }
    public Subject Subject { get; set; }
    public StoryGroup? StoryGroup { get; set; }
    public string Description { get; set; }
    public string QuestionImageFile { get; set; }
    public SchoolClass Class { get; set; }
    public QuestionType QuestionType { get; set; }
    public DifficultyLevel DifficultyLevel { get; set; }
    public string CorrectAnswer { get; set; }
    public Options? MultipleChoiceCurrectAnswer { get; set; }
    public Guid? CorrectOptionId { get; set; }
}
