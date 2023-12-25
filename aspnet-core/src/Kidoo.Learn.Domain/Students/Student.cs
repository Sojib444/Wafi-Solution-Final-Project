using Kidoo.Learn.Enums;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Kidoo.Learn.Students;

public class Student : FullAuditedAggregateRoot<Guid>
{
    // names
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string GuardianName { get; set; }
    public AccountOwner AccountOwner { get; set; }
    public string TransactionId { get; set; }
    public StudentAgeGroup? AgeGroup { get; set; }

    // personal details
    public DateTime? DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailAddress { get; set; }
    public string Institution { get; set; }
    public StudentStatus Status { get; set; }
    public StudentPaymentStatus PaymentStatus { get; set; }
    // School details
    public string Class { get; set; }
    public Referral? Referral { get; set; }
    public DateTime? EnrollmentDate { get; set; }
    public District? District { get; set; }
    public Guid UserId { get; set; }

    // Constructors
    private Student() { }

    public Student(
       Guid id,
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
       DateTime? enrollmentDate
       ) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        GuardianName = guardianName;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        Address = address;
        PhoneNumber = phoneNumber;
        EmailAddress = email;
        Class = studentClass;
        EnrollmentDate = enrollmentDate;
        UserId = userId;
    }

    public Student Update(
        string firstName,
        string lastName,
        string guardianName,
        DateTime? dateOfBirth,
        Gender gender,
        string address,
        string phoneNumber,
        string email,
        string studentClass, 
        StudentAgeGroup ageGroup
    )
    {
        FirstName = firstName;
        LastName = lastName;
        GuardianName = guardianName;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        Address = address;
        PhoneNumber = phoneNumber;
        EmailAddress = email;
        Class = studentClass; 
        AgeGroup = ageGroup; 
        return this;
    }

    public void setPaymentStatus(StudentPaymentStatus paymentStatus)
    {
        PaymentStatus = paymentStatus;
    }
}
