using Kidoo.Learn.Enums; 
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Kidoo.Learn.Students;

public interface IStudentManager : IDomainService
{ 
    public Task<Student> CreateAsync(
                           Guid UserId,
                           string firstName,
                           string lastName,
                           string guardianName,
                           DateTime? dateOfBirth,
                           Gender gender,
                           string address,
                           string phoneNumber,
                           string email,
                           string studentClass,
                           DateTime? enrollmentDate);
    public Task<Student> GetAsync(Guid id);
    public Task<Student> UpdateAsync(Student student,
                           string firstName,
                           string lastName,
                           string guardianName,
                           DateTime? dateOfBirth,
                           Gender gender,
                           string address,
                           string phoneNumber,
                           string email,
                           string studentClass,
                           StudentAgeGroup AgeGroup);
}
