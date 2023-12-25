using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Kidoo.Learn.PuzzleCategories
{
    public interface IPuzzleCategoryAppService : IApplicationService
    {
        Task<PuzzleCategoryDto> CreateAsync(CreateUpdatePuzzleCategoryDto input);
        Task<PagedResultDto<PuzzleCategoryDto>> GetListAsync(GetPuzzleCategoryListDto input);
        Task<PuzzleCategoryDto> UpdateAsync(Guid id, CreateUpdatePuzzleCategoryDto input);
        Task<PuzzleCategoryDto> GetAsync(Guid id);
    }
}
