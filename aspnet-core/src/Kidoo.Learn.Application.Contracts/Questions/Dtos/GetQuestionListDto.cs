using Kidoo.Learn.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Kidoo.Learn.Questions.Dtos;

public class GetQuestionListDto: PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
    public Subject? Subject { get; set; }
    public StoryGroup? QuestionGroup { get; set; }
    public DifficultyLevel? DifficultyLevel { get; set; }

}
