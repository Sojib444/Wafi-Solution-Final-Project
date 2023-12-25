using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Kidoo.Learn.Instructors;

public interface IInstructorManager : IDomainService
{
    Task<Instructor> CreateInstructorAsync(
            string firstName,
            string lastName,
            string qualification,
            string bio,
            string email,
            string instituteName,
            DateTime? hireDate,
            string department);
    Task<Instructor> GetAsync(Guid id);
    Task UpdateAsync(
        Instructor entity,
        string firstName,
        string lastName,
        string qualification,
        string bio,
        string email,
        string instituteName,
        DateTime? hireDate,
        string department
        );
}
