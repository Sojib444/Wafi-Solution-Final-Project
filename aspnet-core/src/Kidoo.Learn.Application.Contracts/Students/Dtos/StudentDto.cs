using Kidoo.Learn.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Kidoo.Learn.Students.Dtos;

public class StudentDto : EntityDto<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string GuardianName { get; set; }

    // personal details
    public DateTime? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailAddress { get; set; }
    public string Institution { get; set; }
    public string TransactionId { get; set; }
    public StudentPaymentStatus PaymentStatus { get; set; }
    public StudentAgeGroup? AgeGroup { get; set; }

    // School details
    public string Class { get; set; }
    public DateTime? EnrollmentDate { get; set; }

}
