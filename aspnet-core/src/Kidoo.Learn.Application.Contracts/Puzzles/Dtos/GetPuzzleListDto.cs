using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Kidoo.Learn.Puzzles.Dtos
{
    public class GetPuzzleListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
