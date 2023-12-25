using Volo.Abp.Application.Dtos;

namespace Kidoo.Learn.PuzzleCategories
{
    public class GetPuzzleCategoryListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
