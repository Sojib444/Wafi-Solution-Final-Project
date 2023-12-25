using Kidoo.Learn.Enums;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Kidoo.Learn.Puzzles
{
    public interface IPuzzleManager : IDomainService
    {
        Task<Puzzle> CreateAsync(string puzzleUrl, string puzzleKey, ComplexityLevel complexityLevel, int displayOrderId,
                    Guid puzzleCategoryId, string description, string puzzleText, string puzzleFile, string password);
        Task<Puzzle> UpdateAsync(Guid id, string puzzleUrl, string puzzleKey, ComplexityLevel complexityLevel, int displayOrderId,
                            Guid puzzleCategoryId, string description, string puzzleText, string puzzleFile, string password);
    }
}
