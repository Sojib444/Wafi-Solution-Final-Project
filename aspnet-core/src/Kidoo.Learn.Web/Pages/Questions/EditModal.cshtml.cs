using Kidoo.Learn.Enums;
using Kidoo.Learn.Questions;
using Kidoo.Learn.Questions.Dtos;
using Kidoo.Learn.Questions.QuestionOptionDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.ObjectMapping;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Kidoo.Learn.Web.Pages.Questions;

public class EditModal : KidooPageModel
{
    [BindProperty(SupportsGet = false)]
    public List<QuestionOptionDto> QuestionOptionDto { get; set; } = new List<QuestionOptionDto>();

    [BindProperty]
    public QuestionDto EditQuestionDto { get; set; }

    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    private readonly IQuestionAppService _questionAppService;
    public EditModal(IQuestionAppService questionAppService)
    {
        _questionAppService = questionAppService;
    }
    public async Task OnGetAsync()
    {
        EditQuestionDto = await _questionAppService.GetAsync(Id);
        if (EditQuestionDto.QuestionType == QuestionType.MultipleChoice && EditQuestionDto.Options.Count > 0)
        {
            if (EditQuestionDto.Options[0].Id == EditQuestionDto.CorrectOptionId)
            {
                EditQuestionDto.MultipleChoiceCurrectAnswer = Options.OptionA;
            }
            else if (EditQuestionDto.Options[1].Id == EditQuestionDto.CorrectOptionId)
            {
                EditQuestionDto.MultipleChoiceCurrectAnswer = Options.OptionB;
            }
            else if (EditQuestionDto.Options[2].Id == EditQuestionDto.CorrectOptionId)
            {
                EditQuestionDto.MultipleChoiceCurrectAnswer = Options.OptionC;
            }
            else if (EditQuestionDto.Options[3].Id == EditQuestionDto.CorrectOptionId)
            {
                EditQuestionDto.MultipleChoiceCurrectAnswer = Options.OptionD;
            }
            QuestionOptionDto = EditQuestionDto.Options;

        }
        else
        {
            QuestionOptionDto = new List<QuestionOptionDto>
            {
                new QuestionOptionDto(),
                new QuestionOptionDto(),
                new QuestionOptionDto(),
                new QuestionOptionDto()
            };
            EditQuestionDto.MultipleChoiceCurrectAnswer = null;
        }

    }

    public async Task OnPostAsync()
    {
        #region validation
        if (EditQuestionDto.Subject == Subject.Story && EditQuestionDto.StoryGroup == null) throw new UserFriendlyException("select a story group");
        if (EditQuestionDto.Subject != Subject.Story)
        {
            EditQuestionDto.StoryGroup = null;
        }
        #endregion

        var QuestionDto = await _questionAppService.GetAsync(Id);

        if (QuestionDto.QuestionType == QuestionType.MultipleChoice
            && EditQuestionDto.QuestionType == QuestionType.MultipleChoice)
        {
            // update the question-options 
            var updateQuestionOptionDto = ObjectMapper.Map<List<QuestionOptionDto>, List<UpdateQuestionOptionDto>>(QuestionOptionDto);
            await _questionAppService.UpdateOptionsAsync(Id, updateQuestionOptionDto);

            // update the questionDto 
            var update = ObjectMapper.Map<QuestionDto, UpdateQuestionDto>(EditQuestionDto);
            await _questionAppService.UpdateAsync(Id, update);
        }
        else if (QuestionDto.QuestionType != QuestionType.MultipleChoice
            && EditQuestionDto.QuestionType == QuestionType.MultipleChoice)
        {
            // add the options
            var updateQuestionOptionDto = ObjectMapper.Map<List<QuestionOptionDto>, List<CreateUpdateQuestionOptionDto>>(QuestionOptionDto);
            updateQuestionOptionDto[0].options = Options.OptionA;
            updateQuestionOptionDto[1].options = Options.OptionB;
            updateQuestionOptionDto[2].options = Options.OptionC;
            updateQuestionOptionDto[3].options = Options.OptionD;
            var optDto = await _questionAppService.AddQuestionOptionAsync(updateQuestionOptionDto, Id);

            // update the questionDto
            var correcOptionId = GetCorrectOptionIdByOption(optDto, EditQuestionDto.MultipleChoiceCurrectAnswer);
            EditQuestionDto.CorrectOptionId = correcOptionId;

            var update = ObjectMapper.Map<QuestionDto, UpdateQuestionDto>(EditQuestionDto);
            await _questionAppService.UpdateAsync(Id, update);

        }
        else if (QuestionDto.QuestionType == QuestionType.MultipleChoice
            && EditQuestionDto.QuestionType != QuestionType.MultipleChoice)
        {
            // remove the options 
            await _questionAppService.DeleteOptionsAsync(Id);

            // update the questionDto 
            var update = ObjectMapper.Map<QuestionDto, UpdateQuestionDto>(EditQuestionDto);
            await _questionAppService.UpdateAsync(Id, update);

        }
        else
        {
            // update the questionDto 
            var update = ObjectMapper.Map<QuestionDto, UpdateQuestionDto>(EditQuestionDto);
            await _questionAppService.UpdateAsync(Id, update);
        }

    }

    private Guid GetCorrectOptionIdByOption(List<QuestionOptionDto> optionDto, Options? op)
    {
        foreach (var option in optionDto)
            if (option.Options == op) return option.Id;

        return Guid.Empty;
    }

}