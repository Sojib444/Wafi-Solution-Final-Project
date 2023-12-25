using Kidoo.Learn.Enums;
using Kidoo.Learn.Permissions;
using Kidoo.Learn.Questions.Dtos;
using Kidoo.Learn.Questions.QuestionOptionDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Kidoo.Learn.Questions;

public class QuestionAppService : ApplicationService, IQuestionAppService
{
    private readonly IQuestionManager _questionManager;
    private readonly IRepository<Question, Guid> _questionRepository;
    private readonly IRepository<QuestionOption, Guid> _questionOptionRepository;
    public QuestionAppService(IRepository<Question, Guid> repository, IQuestionManager questionManager, IRepository<QuestionOption, Guid> questionOptionRepository)
    {
        _questionRepository = repository;
        _questionManager = questionManager;
        _questionOptionRepository = questionOptionRepository;
    }

    [Authorize(LearnPermissions.Question.Create)]
    public async Task<QuestionDto> CreateAsync(CreateUpdateQuestionDto input)
    {
        var questionOptions = ObjectMapper.Map<ICollection<CreateUpdateQuestionOptionDto>, ICollection<QuestionOption>>(input.Options);

        var newQuestion = await _questionManager.CreateQuestionAsync(
                                    input.Title,
                                    input.Subject,
                                    input.Description,
                                    input.QuestionImageFile,
                                    input.Class,
                                    input.QuestionType,
                                    input.DifficultyLevel,
                                    input.CorrectAnswer,
                                    input.CorrectOptionId,
                                    input.StoryGroup,
                                    questionOptions);

       if (newQuestion.QuestionType == QuestionType.MultipleChoice)
        {
            var correctQuestionId = newQuestion.Options.Where(x => x.Options == input.MultipleChoiceCurrectAnswer)
                                                   .Select(x => x.Id)
                                                   .FirstOrDefault();
            newQuestion.CorrectOptionId = correctQuestionId;
        }
        else
        {
            newQuestion.CorrectOptionId = null;
        }

        await _questionRepository.InsertAsync(newQuestion);

        return ObjectMapper.Map<Question, QuestionDto>(newQuestion);
    }

    [Authorize(LearnPermissions.Question.Default)]
    public async Task<QuestionDto> GetAsync(Guid id)
    {
        var questionQuery = await _questionRepository.GetQueryableAsync(); 

        var question = await questionQuery.Where(x => x.Id == id)
                                        .Include(x => x.Options.OrderBy(x => x.Options))
                                        .FirstOrDefaultAsync();

        if (question == null) return null;

        return ObjectMapper.Map<Question, QuestionDto>(question);
    }

    [Authorize(LearnPermissions.Question.Default)]
    public async Task<PagedResultDto<QuestionDto>> GetListAsync(GetQuestionListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Question.CreationTime) + "asc";
        }
        var questionQuery = await _questionRepository.GetQueryableAsync();
        var optionQuery = await _questionOptionRepository.GetQueryableAsync();

        #region filter
        if (!input.Filter.IsNullOrEmpty())
            questionQuery = questionQuery.Where(x => x.Title.Contains(input.Filter) ||
                    x.Description.Contains(input.Filter) ||
                    x.CorrectAnswer.Contains(input.Filter) ||
                    x.QuestionUniqueId.Contains(input.Filter));

        if (input.Subject != null)
        {
            questionQuery = questionQuery.Where(x => x.Subject == input.Subject);
        }
        if (input.QuestionGroup != null)
        {
            questionQuery = questionQuery.Where(x => x.StoryGroup == input.QuestionGroup);
        }
        if (input.DifficultyLevel != null)
        {
            questionQuery = questionQuery.Where(x => x.DifficultyLevel == input.DifficultyLevel);
        }

        if (!input.Filter.IsNullOrEmpty())
            questionQuery = questionQuery.Where(x => x.Title.Contains(input.Filter) ||
                    x.Description.Contains(input.Filter) ||
                    x.CorrectAnswer.Contains(input.Filter) ||
                    x.QuestionUniqueId.Contains(input.Filter));

        #endregion

        var totalCount = await questionQuery.CountAsync();

        var query = from question in questionQuery
                    join option in optionQuery on question.CorrectOptionId equals option.Id into qo
                    from p in qo.DefaultIfEmpty()
                    select new QuestionDto
                    {
                        Id = question.Id,
                        QuestionUniqueId = question.QuestionUniqueId,
                        Subject = question.Subject,
                        Description = question.Description,
                        Title = question.Title,
                        QuestionImageFile = question.QuestionImageFile,
                        Class = question.Class,
                        QuestionType = question.QuestionType,
                        DifficultyLevel = question.DifficultyLevel,
                        CorrectAnswer = question.CorrectAnswer,
                        CorrectOption = p.OptionText,
                        StoryGroup = question.StoryGroup,
                        CreationTime = question.CreationTime,
                        Options = question.Options.Select(y=> new QuestionOptionDto { 
                            Id = y.Id, 
                            Options = y.Options, 
                            OptionText = y.OptionText
                        }).OrderBy(x=>x.Options).ToList()
                    };

        var result = await query
            .OrderByDescending(x => x.CreationTime)
            .PageBy(input)
            .ToListAsync();

        return new PagedResultDto<QuestionDto>(
            totalCount,
            result
        );
    }

    [Authorize(LearnPermissions.Question.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        var entity = await _questionRepository.FindAsync(id);
        if (entity == null) throw new UserFriendlyException("Invalid Request, requested data not found.");

        await _questionRepository.DeleteAsync(id);
    }

    [Authorize(LearnPermissions.Question.Edit)]
    public async Task<QuestionDto> UpdateAsync(Guid id, UpdateQuestionDto input)
    {
        var question = await (await _questionRepository.GetQueryableAsync()).Where(x => x.Id == id).FirstOrDefaultAsync();

        if (question == null) throw new UserFriendlyException("Update failed, Couldn't find the requested data.");

        question.CorrectOptionId = input.CorrectOptionId;
        question.StoryGroup = input.StoryGroup;

        await _questionManager.UpdateAsync(
                                    question,
                                    input.Title,
                                    input.Subject,
                                    input.Description,
                                    input.QuestionImageFile,
                                    input.Class,
                                    input.QuestionType,
                                    input.DifficultyLevel,
                                    input.CorrectAnswer);

        await _questionRepository.UpdateAsync(question);

        return ObjectMapper.Map<Question, QuestionDto>(question);
    }

    [Authorize(LearnPermissions.Question.Edit)]
    public async Task<List<QuestionOptionDto>> AddQuestionOptionAsync(List<CreateUpdateQuestionOptionDto> input, Guid questionId)
    {
        var question = await (await _questionRepository.GetQueryableAsync()).Where(x => x.Id == questionId).Include(x => x.Options).FirstOrDefaultAsync();

        foreach (var item in input)
        {
            var optionDomain = ObjectMapper.Map<CreateUpdateQuestionOptionDto, QuestionOption>(item);

            await _questionManager.AddOptionAsync(question, optionDomain, questionId);
        }

        await _questionRepository.UpdateAsync(question);

        return ObjectMapper.Map<List<QuestionOption>, List<QuestionOptionDto>>(question.Options);
    }

    [Authorize(LearnPermissions.Question.Default)]
    public async Task<List<SelectListDto>> GetQuestionDropdownAsync()
    {
        var result = await (await _questionRepository.GetQueryableAsync())
                                    .AsNoTracking()
                                    .Select(x => new SelectListDto()
                                    {
                                        Id = x.Id,
                                        Text = x.Title,
                                    })
                                    .ToListAsync();
        return result;
    }
    [Authorize(LearnPermissions.Question.Edit)]
    public async Task UpdateOptionsAsync(Guid questionId, List<UpdateQuestionOptionDto> questionOptionDto)
    {
        if (questionOptionDto[0].Id == Guid.Empty)
        {
            // create this questionOptionDto as new list
            foreach (var option in questionOptionDto)
            {
                var optionEntity = _questionManager.CreateQuestionOptionAsync(questionId, option.Options, option.OptionText);

                await _questionOptionRepository.UpdateAsync(optionEntity);
            }

        }
        else
        {
            foreach (var option in questionOptionDto)
            {
                var optionEntity = await _questionOptionRepository.FindAsync(option.Id);
                optionEntity.OptionText = option.OptionText;
                await _questionOptionRepository.UpdateAsync(optionEntity);
            }
        }
    }

    [Authorize(LearnPermissions.Question.Delete)]
    public async Task DeleteOptionsAsync(Guid questionId)
    {
        var optionQuery = await _questionOptionRepository.GetQueryableAsync();
        
        var questionOptions = await optionQuery
                                        .Where(x => x.QuestionId == questionId)
                                        .ToListAsync();

        await _questionOptionRepository.DeleteManyAsync(questionOptions);
    }

}
