using Kidoo.Learn.Enums;
using Kidoo.Learn.Students.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Kidoo.Learn.Students;

public interface IStudentAppService : IApplicationService
{
    Task<StudentDto> CreateAsync(CreateUpdateStudentDto input);
    Task<StudentDto> RegisterAsync(CreateUpdateStudentDto input);
    Task<StudentDto> GetAsync(Guid id);
    Task<StudentProfileDto> GetProfileAsync();
    Task<PagedResultDto<StudentDto>> GetListAsync(GetStudentListDto input);
    Task<StudentDto> UpdateAsync(Guid id, UpdateStudentDto input);
    Task DeleteAsync(Guid id);
    Task<StudentDto> Approve(Guid id, StudentPaymentStatus paymentStatus);
    Task UpdatePasswordAsync(Guid id, UpdateStudentPasswordDto input);
}
