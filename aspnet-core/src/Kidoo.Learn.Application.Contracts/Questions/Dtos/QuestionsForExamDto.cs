using Kidoo.Learn.Enums;
using Kidoo.Learn.Questions.QuestionOptionDtos;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Kidoo.Learn.Questions.Dtos;

public class QuestionsForExamDto : EntityDto<Guid>
{
    public string Title { get; set; }
    public string QuestionUniqueId { get; set; }
    public Subject Subject { get; set; }
    public string Description { get; set; }
    public string QuestionImageFile { get; set; }
    public SchoolClass Class { get; set; }
    public QuestionType QuestionType { get; set; }
    public DifficultyLevel DifficultyLevel { get; set; }
    public StoryGroup? StoryGroup { get; set; }
    public string CorrectAnswer { get; set; }
    public Guid? CorrectOptionId { get; set; }
    public string CorrectOptionString { get; set; }

    // Navigation properties
    public List<QuestionOptionDto> Options { get; set; } = new();
}
