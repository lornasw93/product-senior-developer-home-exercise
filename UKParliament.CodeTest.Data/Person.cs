using System.ComponentModel.DataAnnotations;

namespace UKParliament.CodeTest.Data;

public class Person
{
    [Key]
    public int Id { get; set; }

    public int DepartmentId { get; set; }
    public Department Department { get; set; }

    public string Title { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }

    public string Initials => $"{FirstName[0]}{LastName[0]}";
}