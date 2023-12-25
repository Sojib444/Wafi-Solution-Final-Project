using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Kidoo.Learn.Instructors;

public class InstructorManager : DomainService, IInstructorManager
{
    IRepository<Instructor,Guid> _instructorRepository;
    public InstructorManager(IRepository<Instructor, Guid> instructorRepository)
    {
        _instructorRepository = instructorRepository;
    }

    public async Task<Instructor> CreateInstructorAsync(
        string firstName, 
        string lastName, 
        string qualification, 
        string bio, 
        string email, 
        string instituteName, 
        DateTime? hireDate, 
        string department)
    {
        var isTitleExist = await _instructorRepository.AnyAsync(x => (x.FirstName == firstName && x.LastName == lastName));

        if (isTitleExist) throw new UserFriendlyException($"Same user already exists.");
        return new Instructor(GuidGenerator.Create(), firstName,lastName,qualification, bio, email, instituteName, hireDate, department);
    }

    public async Task<Instructor> GetAsync(Guid id)
    {
        var entity = await _instructorRepository.FindAsync(id);
        if (entity == null) throw new UserFriendlyException("Invalid Request, requested data not found.");
        
        return entity;
    }

    public async Task UpdateAsync(
        Instructor entity,
        string firstName,
        string lastName,
        string qualification,
        string bio,
        string email,
        string instituteName,
        DateTime? hireDate,
        string department)
    {
        var EntityUnchanged = entity.IsEqual(firstName, lastName, qualification, bio, email, instituteName, hireDate, department);
        if (!EntityUnchanged)
        {
            entity.Update(firstName, lastName, qualification, bio, email, instituteName, hireDate, department);
        }
    }
}
