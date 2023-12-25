using Kidoo.Learn.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Identity;

namespace Kidoo.Learn.Students;

public class StudentManager : DomainService, IStudentManager
{
    public readonly IdentityUserManager _userManager;
    IRepository<Student, Guid> _studentRepository;
    public StudentManager(
        IdentityUserManager userManager,
        IRepository<Student, Guid> studentRepository)
    {
        _userManager = userManager;
        _studentRepository = studentRepository;
    }
     

    public async Task<Student> CreateAsync(
                           Guid userId,
                           string firstName,
                           string lastName,
                           string guardianName,
                           DateTime? dateOfBirth,
                           Gender gender,
                           string address,
                           string phoneNumber,
                           string email,
                           string studentClass,
                           DateTime? enrollmentDate)
    {
        var user = await _userManager.GetByIdAsync(userId);

        var studentExists = await _studentRepository.AnyAsync(x => x.EmailAddress == email && x.PhoneNumber == phoneNumber);
        if (studentExists) throw new UserFriendlyException($"Student already exists.");
        
        var student = new Student(
                        userId,
                        userId,
                        firstName, 
                        lastName,
                        guardianName, 
                        dateOfBirth, 
                        gender, 
                        address, 
                        phoneNumber, 
                        email, 
                        studentClass, 
                        enrollmentDate);

        student.PaymentStatus = StudentPaymentStatus.Pending;
        student.Status = StudentStatus.Active;

        return student;
    }

    public async Task<Student> GetAsync(Guid id)
    {
        var entity = await _studentRepository.FindAsync(id);
        if (entity == null) throw new UserFriendlyException("Invalid Request, requested data not found.");

        return entity;
    }
    public async Task<Student> UpdateAsync(
                           Student student,
                           string firstName,
                           string lastName,
                           string guardianName,
                           DateTime? dateOfBirth,
                           Gender gender,
                           string address,
                           string phoneNumber,
                           string email,
                           string studentClass,
                           StudentAgeGroup ageGroup)
    {
       return student.Update(firstName, lastName, guardianName, dateOfBirth, gender, address, phoneNumber, email, studentClass, ageGroup);
    }
}
