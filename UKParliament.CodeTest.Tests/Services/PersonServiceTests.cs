using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Services;
using Xunit;

namespace UKParliament.CodeTest.Tests.Services;

public class PersonServiceTests
{
    private readonly Mock<ILogger<PersonService>> _loggerMock;
    private readonly Mock<IRepository<Person>> _productRepositoryMock;
    private readonly PersonService _service;

    public PersonServiceTests()
    {
        _loggerMock = new Mock<ILogger<PersonService>>();
        _productRepositoryMock = new Mock<IRepository<Person>>();
        _service = new PersonService(_loggerMock.Object, _productRepositoryMock.Object);
    }

    [Fact]
    public async Task GetPersonAsync_WithExistingId_ReturnsPerson()
    {
        // Arrange
        var person = new Person { Id = 1, FirstName = "John", LastName = "Doe" };
        _productRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(person);

        // Act
        var result = await _service.GetAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(person.Id, result.Id);
        Assert.Equal(person.FirstName, result.FirstName);
        Assert.Equal(person.LastName, result.LastName);
    }

    [Fact]
    public async Task GetPersonAsync_WithNotExistingId_ThrowsKeyNotFoundException()
    {
        // Arrange
        _productRepositoryMock.Setup(x => x.GetByIdAsync(1)).Throws<KeyNotFoundException>();

        // Act
        async Task Act() => await _service.GetAsync(1);

        // Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(Act);
    }

    [Fact]
    public async Task GetAllPeopleAsync_ReturnsAllPeople()
    {
        // Arrange
        var people = new[]
        {
            new Person { Id = 1, FirstName = "John", LastName = "Doe" },
            new Person { Id = 2, FirstName = "Jane", LastName = "Smith" }
        };
        _productRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(people);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(people.Length, result.Count());
        Assert.Equal(people[0].Id, result.First().Id);
        Assert.Equal(people[0].FirstName, result.First().FirstName);
        Assert.Equal(people[0].LastName, result.First().LastName);
        Assert.Equal(people[1].Id, result.Last().Id);
        Assert.Equal(people[1].FirstName, result.Last().FirstName);
        Assert.Equal(people[1].LastName, result.Last().LastName);
    }

    [Fact]
    public async Task UpdatePersonAsync_WithPerson_UpdatesPerson()
    {
        // Arrange
        var person = new Person { Id = 1, FirstName = "John", LastName = "Doe" };

        // Act
        await _service.UpdateAsync(person);

        // Assert
        _productRepositoryMock.Verify(x => x.UpdateAsync(person), Times.Once);
    }
}
