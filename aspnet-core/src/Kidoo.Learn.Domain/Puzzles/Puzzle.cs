using Kidoo.Learn.Enums;
using Kidoo.Learn.PuzzleCategories;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Kidoo.Learn.Puzzles
{
    public class Puzzle : FullAuditedAggregateRoot<Guid>
    {
        public string PuzzleUrl { get; private set; }
        public string PuzzleKey { get; private set; }
        public ComplexityLevel ComplexityLevel { get; private set; }
        public int DisplayOrderId { get; private set; }
        public Guid PuzzleCategoryId { get; private set; }
        public PuzzleCategory PuzzleCategory { get; private set; }
        public string Description { get; private set; }
        public string PuzzleText { get; private set; }
        public string PuzzleFile { get; private set; }
        public string Password { get; private set; }
        private Puzzle() { }
        public Puzzle(
            Guid id,
            string puzzleUrl,
            string puzzleKey,
            ComplexityLevel complexityLevel,
            int displayOrderId,
            Guid puzzleCategoryId,
            string description,
            string puzzleText,
            string puzzleFile,
            string password) : base(id)
        {
            PuzzleUrl = puzzleUrl;
            PuzzleKey = puzzleKey;
            ComplexityLevel = complexityLevel;
            DisplayOrderId = displayOrderId;
            PuzzleCategoryId = puzzleCategoryId;
            Description = description;
            PuzzleText = puzzleText;
            PuzzleFile = puzzleFile;
            Password = password;
        }

        public Puzzle UpdateAsync(
            Guid id,
            string puzzleUrl,
            string puzzleKey,
            ComplexityLevel complexityLevel,
            int displayOrderId,
            Guid puzzleCategoryId,
            string description,
            string puzzleText,
            string puzzleFile,
            string password)
        {
            Id = id;
            PuzzleUrl = puzzleUrl;
            PuzzleKey = puzzleKey;
            ComplexityLevel = complexityLevel;
            DisplayOrderId = displayOrderId;
            PuzzleCategoryId = puzzleCategoryId;
            Description = description;
            PuzzleText = puzzleText;
            PuzzleFile = puzzleFile;
            Password = password;

            return this;
        }
    }
}
