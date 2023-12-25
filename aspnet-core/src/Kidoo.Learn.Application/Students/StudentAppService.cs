using Kidoo.Learn.Students.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Kidoo.Learn.Permissions;
using System.Text.RegularExpressions;
using Kidoo.Learn.Enums;
using Kidoo.Learn.EmailSenders;
using Volo.Abp.Domain.Entities;

namespace Kidoo.Learn.Students;

public class StudentAppService : ApplicationService, IStudentAppService
{
    public readonly IdentityUserManager _userManager;
    public readonly IdentityRoleManager _roleManager;
    private readonly IStudentManager _studentManager;
    private readonly IRepository<Student, Guid> _studentRepository;
    private readonly IMailJetEmailSender _emailSender;
    public StudentAppService(
        IdentityUserManager userManager,
        IdentityRoleManager roleManager,
        IRepository<Student, Guid> repository, IStudentManager studentManager,
        IMailJetEmailSender emailSender)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _studentRepository = repository;
        _studentManager = studentManager;
        _emailSender = emailSender;
    }
    public async Task<StudentDto> RegisterAsync(CreateUpdateStudentDto input)
    {
        var userId = GuidGenerator.Create();
        string phoneNumber = Regex.Replace(input.PhoneNumber, @"[^\d]", "");
        if (phoneNumber.Length > 11)
        {
            phoneNumber = phoneNumber.Substring(phoneNumber.Length - 11, 11);
        }

        var user = new IdentityUser(userId, phoneNumber, input.EmailAddress)
        {
            Name = input.FirstName,
            Surname = input.LastName
        };

        user.SetPhoneNumber(phoneNumber, false);

        //Register user
        var result = await _userManager.CreateAsync(user, input.Password, false);
        if (!result.Succeeded)
            throw new UserFriendlyException("Registration Failed!");

        // Add to role
        await _userManager.AddDefaultRolesAsync(user);

        var newStudent = await _studentManager.CreateAsync(
                            userId,
                            input.FirstName,
                            input.LastName,
                            input.GuardianName,
                            input.DateOfBirth,
                            input.Gender,
                            input.Address,
                            phoneNumber,
                            input.EmailAddress,
                            input.Class,
                            input.EnrollmentDate);

        newStudent.AgeGroup = input.AgeGroup;
        newStudent.TransactionId = input.TransactionId;
        newStudent.District = input.District;
        newStudent.Institution = input.Institution;
        newStudent.EnrollmentDate = Clock.Now;
        newStudent.Referral = input.Referral;
        await _studentRepository.InsertAsync(newStudent);

        await _emailSender.SendStudentRegistraionEmailAsync(input);

        return ObjectMapper.Map<Student, StudentDto>(newStudent);
    }


    [Authorize(LearnPermissions.Students.Create)]
    public async Task<StudentDto> CreateAsync(CreateUpdateStudentDto input)
    {
        var userId = GuidGenerator.Create();
        string phoneNumber = Regex.Replace(input.PhoneNumber, @"[^\d]", "");
        if (phoneNumber.Length > 11)
        {
            phoneNumber = phoneNumber.Substring(phoneNumber.Length - 11, 11);
        }

        var user = new IdentityUser(userId, phoneNumber, input.EmailAddress)
        {
            Name = input.FirstName,
            Surname = input.LastName
        };

        user.SetPhoneNumber(phoneNumber, false);

        //Register user
        var result = await _userManager.CreateAsync(user, input.Password, false);
        if (!result.Succeeded)
            throw new UserFriendlyException("Registration Failed!");

        // Add to role
        await _userManager.AddDefaultRolesAsync(user);

        var newStudent = await _studentManager.CreateAsync(
                            userId,
                            input.FirstName,
                            input.LastName,
                            input.GuardianName,
                            input.DateOfBirth,
                            input.Gender,
                            input.Address,
                            phoneNumber,
                            input.EmailAddress,
                            input.Class,
                            input.EnrollmentDate);

        newStudent.AgeGroup = input.AgeGroup;
        newStudent.TransactionId = input.TransactionId;
        newStudent.District = input.District;
        newStudent.Institution = input.Institution;
        newStudent.EnrollmentDate = Clock.Now;
        newStudent.Referral = input.Referral;

        await _studentRepository.InsertAsync(newStudent);

        return ObjectMapper.Map<Student, StudentDto>(newStudent);
    }

    [Authorize(LearnPermissions.Students.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        var entity = await _studentRepository.FindAsync(id);
        if (entity == null) throw new UserFriendlyException("Invalid Request, requested data not found.");

        await _studentRepository.DeleteAsync(id);
    }

    public async Task<StudentDto> GetAsync(Guid id)
    {
        var entity = await _studentManager.GetAsync(id);
        return ObjectMapper.Map<Student, StudentDto>(entity);
    }

    [Authorize]
    public async Task<StudentProfileDto> GetProfileAsync()
    {
        var userId = CurrentUser.Id.GetValueOrDefault();
        if (userId == Guid.Empty) return null;

        var entity = await _studentRepository.FindAsync(userId);
        if (entity == null) return null;

        var result = ObjectMapper.Map<Student, StudentProfileDto>(entity);
        return result;
    }

    [Authorize(LearnPermissions.Students.Default)]
    public async Task<PagedResultDto<StudentDto>> GetListAsync(GetStudentListDto input)
    {
        var query = await _studentRepository.GetQueryableAsync();

        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Student.CreationTime) + " asc";
        }

        #region filter
        if (input.FilterReferral != null) query = query.Where(x => x.Referral == input.FilterReferral);
        if (input.FilterGender != null) query = query.Where(x => x.Gender == input.FilterGender);
        if (input.FilterDistrict != null) query = query.Where(x => x.District == input.FilterDistrict);
        if (input.FilterAgeGroup != null) query = query.Where(x => x.AgeGroup == input.FilterAgeGroup);
        if (input.FilterPaymentStatus != null) query = query.Where(x => x.PaymentStatus == input.FilterPaymentStatus);

        if (!input.Filter.IsNullOrEmpty())
        {
            query = query.Where(x =>
                    x.FirstName.Contains(input.Filter) ||
                    x.LastName.Contains(input.Filter) ||
                    x.EmailAddress.Contains(input.Filter) ||
                    x.GuardianName.Contains(input.Filter) ||
                    x.PhoneNumber.Contains(input.Filter));
        }

        #endregion
        var totalCount = await query.CountAsync();

        var result = await query
                    .OrderByDescending(x => x.CreationTime)
                    .PageBy(input)
                    .Select(x => new StudentDto
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        EmailAddress = x.EmailAddress,
                        PhoneNumber = x.PhoneNumber,
                        EnrollmentDate = x.EnrollmentDate,
                        PaymentStatus = x.PaymentStatus,
                        AgeGroup = x.AgeGroup,
                        TransactionId = x.TransactionId
                    })
                    .ToListAsync();

        return new PagedResultDto<StudentDto>(
            totalCount,
            result
        );
    }

    [Authorize(LearnPermissions.Students.ResetPassword)]
    public async Task UpdatePasswordAsync(Guid id, UpdateStudentPasswordDto input)
    {
        IdentityUser user = await _userManager.FindByIdAsync(id.ToString());

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        await _userManager.ResetPasswordAsync(user, token, input.newPassword);
    }

    [Authorize(LearnPermissions.Dashboard.UpdateProfile)]
    public async Task<StudentDto> UpdateAsync(Guid id, UpdateStudentDto input)
    {
        var entity = await _studentRepository.FindAsync(id);
        if (entity == null) throw new UserFriendlyException("Update failed, Couldn't find the requested data.");

        await _studentManager.UpdateAsync(
                                    entity,
                                    input.FirstName,
                                    input.LastName,
                                    input.GuardianName,
                                    input.DateOfBirth,
                                    input.Gender,
                                    input.Address,
                                    input.PhoneNumber,
                                    input.EmailAddress,
                                    input.Class,
                                    input.AgeGroup);

        entity.District = input.District;
        entity.Institution = input.Institution;

        #region update IdentityUser

        IdentityUser user = await _userManager.FindByIdAsync(id.ToString());

        string phoneNumber = Regex.Replace(input.PhoneNumber, @"[^\d]", "");
        if (phoneNumber != null && phoneNumber.Length < 12) user.SetPhoneNumber(phoneNumber, false);
        if (input.PhoneNumber.Length > 14) throw new UserFriendlyException("Phone number can't be more than 11 digit.");

        user.Name = input.FirstName;
        user.Surname = input.LastName;
        await _userManager.SetEmailAsync(user, input.EmailAddress);

        await _userManager.UpdateAsync(user);

        #endregion

        #region change password
        if (input.OldPassword is not null && input.NewPassword is not null)
        {
            await _userManager.ChangePasswordAsync(user, input.OldPassword, input.NewPassword);
        }
        #endregion

        await _studentRepository.UpdateAsync(entity);


        return ObjectMapper.Map<Student, StudentDto>(entity);
    }

    public async Task<StudentDto> Approve(Guid id, StudentPaymentStatus paymentStatus)
    {
        var student = await _studentRepository.FindAsync(id);
        if (student == null) throw new UserFriendlyException("invalid request, requested data not found.");
        if (student.PaymentStatus == paymentStatus) throw new UserFriendlyException($"Can't update the status. The payment status is already set to {paymentStatus}");

        // approve the transfer
        student.setPaymentStatus(paymentStatus);

        // update entity in database
        await _studentRepository.UpdateAsync(student);

        // return the new dto.
        return ObjectMapper.Map<Student, StudentDto>(student);
    }

    //[Authorize(LearnPermissions.Students.Create)]
    //public async Task SendRegistrationEmailToUsers()
    //{
    //    var studentQuery = await _studentRepository.GetQueryableAsync();
    //    var allStudents = await studentQuery.Select(x => new CreateUpdateStudentDto
    //    {
    //        FirstName = x.FirstName,
    //        LastName = x.LastName,
    //        EmailAddress = x.EmailAddress,
    //        PhoneNumber = x.PhoneNumber,
    //        GuardianName = x.GuardianName,
    //        AgeGroup = x.AgeGroup.GetValueOrDefault(),
    //        Password = "রেজিস্ট্রেশনের সময় যে পাসওয়ার্ড দিয়েছেন"
    //    }).ToListAsync();

    //    foreach(var student in allStudents)
    //    {
    //        await _emailSender.SendStudentRegistraionEmailAsync(student);
    //    }
    //}
}
