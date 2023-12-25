using Kidoo.Learn.Instructors.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Kidoo.Learn.Instructors;

public interface IInstructorAppService : IApplicationService
{
    Task<InstructorDto> CreateAsync(CreateUpdateInstructorDto input);
    Task<InstructorDto> GetAsync(Guid id);
    Task<List<InstructorDto>> GetListAsync(); 
    Task<InstructorDto> UpdateAsync(Guid id, CreateUpdateInstructorDto input);
    Task DeleteAsync(Guid id);
}
