using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data;
using Xunit;

namespace UKParliament.CodeTest.Tests.Repositories;

public class PersonRepositoryTests
{
    private DbContextOptions<PersonManagerContext> _dbContextOptions;

    public PersonRepositoryTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<PersonManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task CreateAsync_ShouldAddEntity()
    {
        // Arrange
        using var context = new PersonManagerContext(_dbContextOptions);
        var repository = new Repository<Person>(context);
        var person = new Person { Id = 1, Title = "Mr", FirstName = "John", LastName = "Doe", Email = "john@pal.com", DateOfBirth = DateTime.Now.AddYears(-30) };

        // Act
        await repository.CreateAsync(person);

        // Assert
        var result = await context.People.FirstOrDefaultAsync(p => p.Id == 1);
        Assert.NotNull(result);
        Assert.Equal("John", result.FirstName);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnEntity_WhenEntityExists()
    {
        // Arrange
        using var context = new PersonManagerContext(_dbContextOptions);
        var repository = new Repository<Person>(context);
        var person = new Person { Id = 1, Title = "Miss", FirstName = "Jane", LastName = "Doe", Email = "jane@pal.com", DateOfBirth = DateTime.Now.AddYears(-25) };
        context.People.Add(person);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Jane", result.FirstName);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowException_WhenEntityDoesNotExist()
    {
        // Arrange
        using var context = new PersonManagerContext(_dbContextOptions);
        var repository = new Repository<Person>(context);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => repository.GetByIdAsync(99));
    }
}
