using JetBrains.Annotations;
using Kidoo.Learn.Courses;
using Kidoo.Learn.CourseSections;
using Kidoo.Learn.Enums;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using static System.Collections.Specialized.BitVector32;

namespace Kidoo.Learn.Questions;

public class Question : FullAuditedAggregateRoot<Guid>
{
    public string Title { get; set; }
    public string QuestionUniqueId { get; set; }
    public Subject Subject { get; set; }
    public StoryGroup? StoryGroup { get; set; }
    public string Description { get; set; }
    public string QuestionImageFile { get; set; }
    public SchoolClass Class { get; set; }
    public QuestionType QuestionType { get; set; }
    public DifficultyLevel DifficultyLevel { get; set; }
    public string CorrectAnswer { get; set; }
    public Guid? CorrectOptionId { get; set; }

    // Navigation properties
    public List<QuestionOption> Options { get; set; }


    // Constructors
    private Question() { }

    internal Question(
        Guid id,
        [NotNull] string title,
        string questionUniqueId,
        Subject subject,
        string description,
        string questionImageFile,
        SchoolClass @class,
        QuestionType questionType,
        DifficultyLevel difficultyLevel,
        string correctAnswer,
         Guid? correctOptionId,
        StoryGroup? questionGroup,
        List<QuestionOption> options) : base(id)
    {
        Title = title;
        QuestionUniqueId = questionUniqueId;
        Subject = subject;
        Description = description;
        QuestionImageFile = questionImageFile;
        Class = @class;
        QuestionType = questionType;
        DifficultyLevel = difficultyLevel;
        CorrectAnswer = correctAnswer;
        Options = options;
        CorrectOptionId = correctOptionId;
        StoryGroup = questionGroup;

    }

    internal bool IsEqual(
        string title,
        Subject subject,
        string description,
        string questionImageFile,
        SchoolClass @class,
        QuestionType questionType,
        DifficultyLevel difficultyLevel,
        string correctAnswer)
    {
        if (
            Title == title &&
            Subject == subject &&
            Description == description &&
            QuestionImageFile == questionImageFile &&
            Class == @class &&
            QuestionType == questionType &&
            DifficultyLevel == difficultyLevel &&
            CorrectAnswer == correctAnswer)
        {
            return true;
        }
        return false;
    }

    internal void update(
        string title,
        Subject subject,
        string description,
        string questionImageFile,
        SchoolClass @class,
        QuestionType questionType,
        DifficultyLevel difficultyLevel,
        string correctAnswer)
    {
        Title = title;
        Subject = subject;
        Description = description;
        QuestionImageFile = questionImageFile;
        Class = @class;
        QuestionType = questionType;
        DifficultyLevel = difficultyLevel;
        CorrectAnswer = correctAnswer;
    }
    internal void AddOption(Guid id, Options options, string optionText, Guid questionId)
    {
        Options.Add(new QuestionOption(id, questionId, optionText, options));
    }
}