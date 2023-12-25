using Kidoo.Learn.Questions.Dtos;
using Kidoo.Learn.Questions.QuestionOptionDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Kidoo.Learn.Questions;

public interface IQuestionAppService : IApplicationService
{
    Task<List<QuestionOptionDto>> AddQuestionOptionAsync(List<CreateUpdateQuestionOptionDto> input, Guid questionId);
    Task<QuestionDto> CreateAsync(CreateUpdateQuestionDto input);
    Task<QuestionDto> GetAsync(Guid id);
    Task<PagedResultDto<QuestionDto>> GetListAsync(GetQuestionListDto input);
    Task<QuestionDto> UpdateAsync(Guid id, UpdateQuestionDto input);
    Task DeleteAsync(Guid id);
    Task<List<SelectListDto>> GetQuestionDropdownAsync();
    Task UpdateOptionsAsync(Guid questionId, List<UpdateQuestionOptionDto> questionOptionDto);
    Task DeleteOptionsAsync(Guid questionId);
}
