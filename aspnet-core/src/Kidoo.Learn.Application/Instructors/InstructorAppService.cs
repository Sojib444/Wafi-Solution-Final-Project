using Kidoo.Learn.Instructors.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Kidoo.Learn.Instructors;

public class InstructorAppService: ApplicationService, IInstructorAppService
{
    private readonly IInstructorManager _instructorManager;
    IRepository<Instructor, Guid> _instructorRepository;
    public InstructorAppService(IRepository<Instructor, Guid> repository, IInstructorManager instructorManager)
    {
        _instructorRepository = repository;
        _instructorManager = instructorManager;
    }

    public async Task<InstructorDto> CreateAsync(CreateUpdateInstructorDto input)
    {
        var newInstructor = await _instructorManager.CreateInstructorAsync(
                                    input.FirstName, 
                                    input.LastName, 
                                    input.Qualification, 
                                    input.Bio, 
                                    input.Email, 
                                    input.InstituteName, 
                                    input.HireDate, 
                                    input.Department);

        await _instructorRepository.InsertAsync(newInstructor);

        return ObjectMapper.Map<Instructor, InstructorDto>(newInstructor);
    }

    public async Task<InstructorDto> GetAsync(Guid id)
    {
        var entity = await _instructorManager.GetAsync(id);
        return ObjectMapper.Map<Instructor, InstructorDto>(entity);
    }
    public async Task<List<InstructorDto>> GetListAsync()
    {
        var entity = await _instructorRepository.GetListAsync();
        return ObjectMapper.Map<List<Instructor>, List<InstructorDto>>(entity);
    }
    public async Task DeleteAsync(Guid id)
    {
        var entity = await _instructorRepository.FindAsync(id);
        if (entity == null) throw new UserFriendlyException("Invalid Request, requested data not found.");

        await _instructorRepository.DeleteAsync(id);
    }
    public async Task<InstructorDto> UpdateAsync(Guid id, CreateUpdateInstructorDto input)
    {
        var entity = await _instructorRepository.FindAsync(id);
        if (entity == null) throw new UserFriendlyException("Update failed, Couldn't find the requested data.");

        await _instructorManager.UpdateAsync(
                                    entity,
                                    input.FirstName,
                                    input.LastName,
                                    input.Qualification,
                                    input.Bio,
                                    input.Email,
                                    input.InstituteName,
                                    input.HireDate,
                                    input.Department);

        await _instructorRepository.UpdateAsync(entity);

        return ObjectMapper.Map<Instructor, InstructorDto>(entity);
    }
}
