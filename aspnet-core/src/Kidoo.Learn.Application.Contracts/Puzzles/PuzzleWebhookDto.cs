using System;
using System.Collections.Generic;
using System.Text;

namespace Kidoo.Learn.Puzzles
{
    public class PuzzleWebhookDto
    {
        public string puzzleKey { get; set; }
        public object results { get; set; }
        public string account { get; set; }
        public string type { get; set; }
    }
}
