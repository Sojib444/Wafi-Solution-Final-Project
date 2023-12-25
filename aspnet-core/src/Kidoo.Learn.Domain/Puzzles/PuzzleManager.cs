using Kidoo.Learn.Enums;
using Kidoo.Learn.PuzzleCategories;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Kidoo.Learn.Puzzles
{
    public class PuzzleManager : DomainService, IPuzzleManager
    {
        private readonly IRepository<Puzzle, Guid> _puzzleRepository;
        private readonly IRepository<PuzzleCategory, Guid> _puzzleCategoryRepository;
        public PuzzleManager(IRepository<Puzzle, Guid> puzzleRepository,
            IRepository<PuzzleCategory, Guid> puzzleCategoryRepository)
        {
            _puzzleRepository = puzzleRepository;
            _puzzleCategoryRepository = puzzleCategoryRepository;
        }
        public async Task<Puzzle> CreateAsync(string puzzleUrl, string puzzleKey, ComplexityLevel complexityLevel, int displayOrderId, Guid puzzleCategoryId, string description, string puzzleText, string puzzleFile, string password)
        {
            var isExistCategory = await _puzzleCategoryRepository.AnyAsync(x => x.Id == puzzleCategoryId);
            if (!isExistCategory)
                throw new BusinessException("Category not found!");

            return new Puzzle(GuidGenerator.Create(), puzzleUrl, puzzleKey, complexityLevel,
                displayOrderId, puzzleCategoryId, description, puzzleText, puzzleFile, password);
        }

        public async Task<Puzzle> UpdateAsync(Guid id, string puzzleUrl, string puzzleKey, ComplexityLevel complexityLevel, int displayOrderId, Guid puzzleCategoryId, string description, string puzzleText, string puzzleFile, string password)
        {
            var puzzle = await _puzzleRepository.GetAsync(x => x.Id == id);
            if (puzzle == null)
                throw new BusinessException("Puzzle not found!");

            var isExistCategory = await _puzzleCategoryRepository.AnyAsync(x => x.Id == puzzleCategoryId);
            if (!isExistCategory)
                throw new BusinessException("Category not found!");

            var result = puzzle.UpdateAsync(id, puzzleUrl, puzzleKey, complexityLevel,
                displayOrderId, puzzleCategoryId, description, puzzleText, puzzleFile, password);

            return result;
        }
    }
}
