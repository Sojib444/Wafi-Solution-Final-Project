using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Kidoo.Learn.PuzzleCategories
{
    public class PuzzleCategoryManager : DomainService, IPuzzleCategoryManager
    {
        private readonly IRepository<PuzzleCategory, Guid> _puzzleCategoryRepository;
        public PuzzleCategoryManager(IRepository<PuzzleCategory, Guid> puzzleCategoryRepository)
        {
            _puzzleCategoryRepository = puzzleCategoryRepository;
        }
        public async Task<PuzzleCategory> CreateAsync(string title, string description, string thumbnailUrl)
        {
            var isExistTitle = await _puzzleCategoryRepository.AnyAsync(x => x.Title == title);
            if (isExistTitle) throw new BusinessException($"'{title}' is already exist!");

            return new PuzzleCategory(GuidGenerator.Create(), title, description, thumbnailUrl);
        }

        public async Task<PuzzleCategory> UpdateAsync(Guid id, string title, string description, string thumbnailUrl)
        {
            var isExistTitle = await _puzzleCategoryRepository.FirstOrDefaultAsync(x => x.Title == title);
            if (isExistTitle != null) throw new BusinessException($"'{title}' is already exist!");

            var puzzleCategory = await _puzzleCategoryRepository.GetAsync(x => x.Id == id);

            var result = puzzleCategory.UpdateAsync(id, title, description, thumbnailUrl);

            return result;
        }
    }
}
