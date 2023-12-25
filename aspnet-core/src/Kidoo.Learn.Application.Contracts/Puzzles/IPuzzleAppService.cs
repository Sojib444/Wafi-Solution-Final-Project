using Kidoo.Learn.Puzzles.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Kidoo.Learn.Puzzles
{
    public interface IPuzzleAppService : IApplicationService
    {
        Task<PuzzleDto> CreateAsync(CreateUpdatePuzzleDto input);
        Task<PagedResultDto<PuzzleDto>> GetListAsync(GetPuzzleListDto input);
        Task<PuzzleDto> UpdateAsync(Guid id, CreateUpdatePuzzleDto input);
        Task<PuzzleDto> GetAsync(Guid id);
        Task<ListResultDto<PuzzleCategoryLookupDto>> GetPuzzleCategoryLookupAsync();
        Task Complete(PuzzleCompletionListenerDto input);
        Task Webhook(PuzzleWebhookDto input);
    }
}
