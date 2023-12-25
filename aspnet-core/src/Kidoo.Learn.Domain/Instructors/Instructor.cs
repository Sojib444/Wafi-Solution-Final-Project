using System;
using Volo.Abp.Domain.Entities;

namespace Kidoo.Learn.Instructors;

public class Instructor : AggregateRoot<Guid>
{
    public string FullName { get; private set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Qualification { get; private set; } // BSC in CSE from ULAB


    // institue information
    public string Bio { get; private set; }
    public string Email { get; set; }
    public string InstituteName { get; set; } // Institute working for
    public DateTime? HireDate { get; private set; }
    public string Department { get; private set; } // Mathematics, English, Bangla

    public Instructor(
            Guid id,
            string firstName,
            string lastName,
            string qualification,
            string bio,
            string email,
            string instituteName,
            DateTime? hireDate,
            string department
        ) : base(id)
    {
        FullName = firstName + " " + lastName;
        FirstName = firstName;
        LastName = lastName;
        Qualification = qualification;
        Bio = bio;
        Email = email;
        InstituteName = instituteName;
        HireDate = hireDate;
        Department = department;
    }

    internal bool IsEqual(string firstName, string lastName, string qualification, string bio, string email, string instituteName, DateTime? hireDate, string department)
    {
        if (
            FirstName == firstName &&
            LastName == lastName &&
            Qualification == qualification &&
            Bio == bio &&
            Email == email &&
            InstituteName == instituteName &&
            HireDate == hireDate &&
            Department == department
            )
        {
            return true;
        }
        return false;
    }

    internal void Update(string firstName, string lastName, string qualification, string bio, string email, string instituteName, DateTime? hireDate, string department)
    {
        FullName = firstName + " " + lastName;
        FirstName = firstName;
        LastName = lastName;
        Qualification = qualification;
        Bio = bio;
        Email = email;
        InstituteName = instituteName;
        HireDate = hireDate;
        Department = department;
    }
}
