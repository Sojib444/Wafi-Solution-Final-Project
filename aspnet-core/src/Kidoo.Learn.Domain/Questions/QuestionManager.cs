using JetBrains.Annotations;
using Kidoo.Learn.Courses;
using Kidoo.Learn.Enums;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Kidoo.Learn.Questions;

public class QuestionManager : DomainService, IQuestionManager
{
    IRepository<Question, Guid> _questionRepository;
    public QuestionManager(IRepository<Question, Guid> repository)
    {
        _questionRepository = repository;
    }
    public async Task<Question> CreateQuestionAsync(
        string title,
        Subject subject,
        string description,
        string questionImageFile,
        SchoolClass @class,
        QuestionType questionType,
        DifficultyLevel difficultyLevel,
        string correctAnswer,
         Guid? correctOptionId,
        StoryGroup? questionGroup,
        ICollection<QuestionOption> options)
    {
        var isTitleExist = await _questionRepository.AnyAsync(x => x.Title == title);
        if (isTitleExist) throw new UserFriendlyException($"Question already exist with '{title}' title");

        var questionId = GuidGenerator.Create();
        string questionUniqueId = GenerateUniqueId(subject, questionType);

        var questionOptionList = new List<QuestionOption>();
        if (options != null)
        {
            foreach (var item in options)
            {
                if (item.OptionText.IsNullOrWhiteSpace()) item.OptionText = "_" ;
                var newOption = new QuestionOption(GuidGenerator.Create(), questionId, item.OptionText, item.Options);
                questionOptionList.Add(newOption);
            }
        }


         
        return new Question(
                    questionId, 
                    title, 
                    questionUniqueId, 
                    subject, 
                    description, 
                    questionImageFile, 
                    @class, 
                    questionType, 
                    difficultyLevel, 
                    correctAnswer,
                    correctOptionId,
                    questionGroup,
                    questionOptionList);
    }
    private string GenerateUniqueId(Subject subject, QuestionType? questionType )
    {
        if ( questionType == null || questionType == 0)
        {
            questionType = QuestionType.MultipleChoice;
        }

        const string UniqueIdFormat = "{0}_{1}_{2:yyyyMMddHHmmss}";

        var date = Clock.Now;
        string subjectPrefix = subject.ToString().Substring(0, 3).ToUpper();
        string questionTypePrefix = questionType.ToString().Substring(0, 3).ToUpper();

        string generatedUniqueId = string.Format(UniqueIdFormat, subjectPrefix, questionTypePrefix, date);

        return generatedUniqueId;
    }


    public async Task<Question> GetAsync(Guid id)
    {
        var entity = await _questionRepository.FindAsync(id);
        if (entity == null) throw new UserFriendlyException("Invalid Request, requested data not found.");
        
        return entity;
    }

    public async Task UpdateAsync(
                    Question question, 
                    string title, 
                    Subject subject, 
                    string description, 
                    string questionImageFile,
                    SchoolClass @class, 
                    QuestionType questionType,
                    DifficultyLevel difficultyLevel, 
                    string correctAnswer)
    {
        var questionEntityUnchanged = question.IsEqual(title, subject, description, questionImageFile, @class, questionType, difficultyLevel, correctAnswer);

        // update if required
        if (!questionEntityUnchanged)
        {
            question.update(title, subject, description, questionImageFile, @class, questionType, difficultyLevel, correctAnswer);
        }
        
    }

    public QuestionOption CreateQuestionOptionAsync(Guid questionId, Options options, string optionText)
    {
        var newQuestion = new QuestionOption(GuidGenerator.Create(), questionId, optionText, options);
        return newQuestion;
    }

    public async Task AddOptionAsync(Question question, QuestionOption optionDomain, Guid questionId)
    {
        question.AddOption(
                GuidGenerator.Create(),
                optionDomain.Options,
                optionDomain.OptionText,
                questionId
            );
    }
}
