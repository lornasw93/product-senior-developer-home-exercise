using System.ComponentModel.DataAnnotations;

namespace UKParliament.CodeTest.Data;

public class Department
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

   // public ICollection<Person> People { get; set; }
}
