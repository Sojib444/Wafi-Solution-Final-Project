using Kidoo.Learn.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using Kidoo.Learn.Consts;

namespace Kidoo.Learn.Students.Dtos;

public class UpdateStudentDto
{
    [Required(ErrorMessage = "First Name is required")]
    [StringLength(StudentConsts.MaxFirstNameLength, ErrorMessage = "First Name cannot exceed {1} characters")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required")]
    [StringLength(StudentConsts.MaxLastNameLength, ErrorMessage = "Last Name cannot exceed {1} characters")]
    public string LastName { get; set; }

    [StringLength(StudentConsts.MaxFatherNameLength, ErrorMessage = "Gaurdian Name cannot exceed {1} characters")]
    [Required]
    public string GuardianName { get; set; }
     
    public DateTime? DateOfBirth { get; set; }

    public Gender Gender { get; set; }

    [StringLength(StudentConsts.MaxAddressLength, ErrorMessage = "Address cannot exceed {1} characters")]
    public string Address { get; set; }

    [StringLength(StudentConsts.MaxPhoneNumberLength, ErrorMessage = "Phone Number cannot exceed {1} characters")]
    public string PhoneNumber { get; set; }

    [StringLength(StudentConsts.MaxEmailLength, ErrorMessage = "Email cannot exceed {1} characters")]
    [EmailAddress]
    public string EmailAddress { get; set; }
    public StudentAgeGroup AgeGroup { get; set; }
    public string Institution { get; set; }
    [StringLength(StudentConsts.MaxClassLength, ErrorMessage = "Class cannot exceed {1} characters")]
    public string Class { get; set; }  
    public District? District { get; set; }
    public string OldPassword {  get; set; }
    public string NewPassword {  get; set; }
}
