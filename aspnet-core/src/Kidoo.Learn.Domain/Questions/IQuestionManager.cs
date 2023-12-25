using JetBrains.Annotations;
using Kidoo.Learn.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Kidoo.Learn.Questions;

public interface IQuestionManager: IDomainService
{
    Task AddOptionAsync(Question question, QuestionOption optionDomain, Guid questionId);
    Task<Question> CreateQuestionAsync(
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
        ICollection<QuestionOption> options);
    QuestionOption CreateQuestionOptionAsync(Guid questionId, Options options, string optionText);
    Task<Question> GetAsync(Guid id);
    Task UpdateAsync(
        Question question, 
        string title, 
        Subject subject, 
        string description, 
        string questionImageFile,
        SchoolClass @class, 
        QuestionType questionType,
        DifficultyLevel difficultyLevel, 
        string correctAnswer);
}
