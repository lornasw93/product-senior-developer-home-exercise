using Microsoft.EntityFrameworkCore;

namespace UKParliament.CodeTest.Data;

public class PersonManagerContext : DbContext
{
    public DbSet<Person> People { get; set; }
    public DbSet<Department> Departments { get; set; }

    public PersonManagerContext(DbContextOptions<PersonManagerContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Department>().HasData(
            new Department { Id = 1, Name = "Engineering" },
            new Department { Id = 2, Name = "Human Resources" },
            new Department { Id = 3, Name = "Marketing" }
        );

        modelBuilder.Entity<Person>()
         .HasOne(p => p.Department)
         .WithMany()
         .HasForeignKey(p => p.DepartmentId);

        modelBuilder.Entity<Person>().HasData(
            new Person { Id = 1, Title = "Miss", FirstName = "Alice", LastName = "Smith", DateOfBirth = new DateTime(1990, 5, 1), DepartmentId = 1, Email = "alice@pm.com" },
            new Person { Id = 2, Title = "Mr", FirstName = "Bob", LastName = "Brown", DateOfBirth = new DateTime(1985, 8, 12), DepartmentId = 2, Email = "bob@pm.com" },
            new Person { Id = 3, Title = "Mrs", FirstName = "Charlie", LastName = "Davis", DateOfBirth = new DateTime(1992, 3, 25), DepartmentId = 1, Email = "charlie@pm.com" },
            new Person { Id = 4, Title = "Mr", FirstName = "David", LastName = "Evans", DateOfBirth = new DateTime(1980, 11, 5), DepartmentId = 3, Email = "david@pm.com" },
            new Person { Id = 5, Title = "Ms", FirstName = "Emily", LastName = "Garcia", DateOfBirth = new DateTime(1982, 7, 15), DepartmentId = 2, Email = "emily@pm.com" },
            new Person { Id = 6, Title = "Mr", FirstName = "Frank", LastName = "Hernandez", DateOfBirth = new DateTime(1987, 9, 30), DepartmentId = 1, Email = "frank@pm.com" },
            new Person { Id = 7, Title = "Ms", FirstName = "Grace", LastName = "Johnson", DateOfBirth = new DateTime(1989, 4, 10), DepartmentId = 3, Email = "grace@pm.com" },
            new Person { Id = 8, Title = "Mr", FirstName = "Henry", LastName = "King", DateOfBirth = new DateTime(1984, 6, 20), DepartmentId = 2, Email = "henry@pm.com" },
            new Person { Id = 9, Title = "Ms", FirstName = "Isabella", LastName = "Lee", DateOfBirth = new DateTime(1983, 10, 2), DepartmentId = 1, Email = "isabella@pm.com" },
            new Person { Id = 10, Title = "Mr", FirstName = "Jack", LastName = "Martinez", DateOfBirth = new DateTime(1986, 12, 7), DepartmentId = 3, Email = "jack@pm.com" });
    }
}