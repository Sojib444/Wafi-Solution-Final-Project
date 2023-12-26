using Kidoo.Learn.Enums;
using System;
using Volo.Abp.Application.Dtos;

namespace Kidoo.Learn.Students.Dtos;

public class GetStudentListDto : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
    public string FilterAddress { get; set; }

    public DateTime? FilterDateOfBirthStart { get; set; }
    public DateTime? FilterDateOfBirthEnd { get; set; }

    public Referral? FilterReferral { get; set; }
    public Gender? FilterGender { get; set; }
    public AccountOwner FilterAccountOwner { get; set; }
    public StudentAgeGroup? FilterAgeGroup { get; set; }
    public District? FilterDistrict { get; set; }
    public StudentPaymentStatus? FilterPaymentStatus { get; set; }
}
