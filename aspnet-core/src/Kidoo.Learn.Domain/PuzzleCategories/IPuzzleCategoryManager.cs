using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Kidoo.Learn.PuzzleCategories
{
    public interface IPuzzleCategoryManager : IDomainService
    {
        Task<PuzzleCategory> CreateAsync(string title, string description, string thumbnailUrl);
        Task<PuzzleCategory> UpdateAsync(Guid id, string title, string description, string thumbnailUrl);
    }
}
