using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Kidoo.Learn.Instructors.Dtos;

public class InstructorDto : EntityDto<Guid>
{
    // names
    public string FullName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Qualification { get; private set; } // BSC in CSE from ULAB

    // institue information
    public string Bio { get; private set; }
    public string Email { get; set; }
    public string InstituteName { get; set; } // Institute working for
    public DateTime? HireDate { get; private set; }
    public string Department { get; private set; } // Mathematics, English, Bangla

}
