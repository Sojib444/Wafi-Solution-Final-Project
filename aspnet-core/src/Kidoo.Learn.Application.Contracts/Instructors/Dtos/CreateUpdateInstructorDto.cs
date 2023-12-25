using System;
using System.ComponentModel.DataAnnotations;
using Kidoo.Learn.Consts;

namespace Kidoo.Learn.Instructors.Dtos;

public class CreateUpdateInstructorDto
{
    [Required(ErrorMessage = "First name is required.")]
    [StringLength(InstructorConsts.MaxFirstNameLength, ErrorMessage = "First Name cannot exceed {1} characters")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = " Last name is required.")]
    [StringLength(InstructorConsts.MaxLastNameLength, ErrorMessage = "Last Name cannot exceed {1} characters")]
    public string LastName { get; set; }

    [StringLength(InstructorConsts.MaxQualificationLength, ErrorMessage = "Qualification cannot exceed {1} characters")]
    public string Qualification { get; set; }

    [StringLength(InstructorConsts.MaxBioLength, ErrorMessage = "Bio cannot exceed {1} characters")]
    public string Bio { get; set; }

    [StringLength(InstructorConsts.MaxEmailLength, ErrorMessage = "Email cannot exceed {1} characters")]
    [EmailAddress]
    public string Email { get; set; }

    [StringLength(InstructorConsts.MaxInstituteNameLength, ErrorMessage = "Institute Name cannot exceed {1} characters")]
    public string InstituteName { get; set; }

    public DateTime? HireDate { get; set; }

    [StringLength(InstructorConsts.MaxDepartmentLength, ErrorMessage = "Department cannot exceed {1} characters")]
    public string Department { get; set; }
}
