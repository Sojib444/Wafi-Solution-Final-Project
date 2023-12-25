using Kidoo.Learn.Enums;
using System; 
using Volo.Abp.Domain.Entities;

namespace Kidoo.Learn.Questions;

public class QuestionOption: Entity<Guid>
{
    public string OptionText { get; set; }


    // Navigation properties
    public Guid QuestionId { get; set; }
    public Question Question { get; set; }
    public Options Options { get; set; }


    // Constructors
    private QuestionOption() { }

    internal QuestionOption(Guid id, Guid questionId, string optionText, Options options): base(id)
    {
        QuestionId = questionId;
        OptionText = optionText;
        Options = options;
    }
     
}
