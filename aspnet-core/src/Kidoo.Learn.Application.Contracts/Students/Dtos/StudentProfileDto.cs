using Kidoo.Learn.Enums;
using System;
using Volo.Abp.Application.Dtos;

namespace Kidoo.Learn.Students.Dtos;

public class StudentProfileDto : EntityDto<Guid>
{
    public AccountOwner? AccountOwner { get; set; }
    public string GuardianName { get; set; }
    public Gender Gender { get; set; }

    public DateTime EnrollmentDate { get; set; }
    public string Institution { get; set; }
    public string Class { get; set; }
    public District? District { get; set; }

    public Referral Referral { get; set; }
    public StudentAgeGroup AgeGroup { get; set; }

}
