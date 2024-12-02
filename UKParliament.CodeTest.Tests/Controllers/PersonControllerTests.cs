using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Web.Controllers;
using UKParliament.CodeTest.Web.ViewModels;
using Xunit;

namespace UKParliament.CodeTest.Tests.Controllers;

public class PersonControllerTests
{
    private readonly Mock<IPersonService> _serviceMock;
    private readonly PersonController _controller;

    public PersonControllerTests()
    {
        _serviceMock = new Mock<IPersonService>();
        _controller = new PersonController(Mock.Of<ILogger<PersonController>>(), _serviceMock.Object);
    }

    [Fact]
    public async Task GetPerson_ShouldReturnOk_WhenPersonExists()
    {
        // Arrange
        var person = new Person { Id = 1, FirstName = "John", LastName = "Doe" };
        _serviceMock.Setup(service => service.GetAsync(1)).ReturnsAsync(person);

        // Act
        var result = await _controller.GetPerson(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPerson = Assert.IsType<Person>(okResult.Value);
        Assert.Equal("John", returnedPerson.FirstName);
    }

    [Fact]
    public async Task GetPerson_ShouldReturnNotFound_WhenPersonDoesNotExist()
    {
        // Arrange
        _serviceMock.Setup(service => service.GetAsync(1)).ReturnsAsync((Person)null);

        // Act
        var result = await _controller.GetPerson(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetAllPeople_ShouldReturnOk_WhenPersonsExist()
    {
        // Arrange
        var persons = new List<Person>
        {
            new Person { Id = 1, FirstName = "John", LastName = "Doe" },
            new Person { Id = 2, FirstName = "Jane", LastName = "Doe" }
        };
        _serviceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(persons);

        // Act
        var result = await _controller.GetAllPeople();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPersons = Assert.IsType<List<Person>>(okResult.Value);
        Assert.Equal(2, returnedPersons.Count);
    }
     
    [Fact]
    public async Task CreatePerson_ShouldReturnBadRequest_WhenValidatorFailed()
    {
        // Arrange
        var person = new PersonViewModel { FirstName = "John", LastName = "Doe" };
        _serviceMock.Setup(service => service.CreateAsync(It.IsAny<Person>())).ReturnsAsync(true);

        // Act
        var result = await _controller.CreatePerson(person);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreatePerson_ShouldReturnBadRequest_WhenPersonIsNotCreated()
    {
        // Arrange
        var person = new PersonViewModel { FirstName = "John", LastName = "Doe" };
        _serviceMock.Setup(service => service.CreateAsync(It.IsAny<Person>())).ReturnsAsync(false);

        // Act
        var result = await _controller.CreatePerson(person);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreatePerson_ShouldReturnBadRequest_WhenPersonViewModelIsInvalid()
    {
        // Arrange
        var person = new PersonViewModel { FirstName = "John" };
        _controller.ModelState.AddModelError("LastName", "LastName is required");

        // Act
        var result = await _controller.CreatePerson(person);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdatePerson_ShouldReturnBadRequest_WhenValidatorFailed()
    {
        // Arrange
        var person = new PersonViewModel { FirstName = "John" };
        _controller.ModelState.AddModelError("LastName", "LastName is required");

        // Act
        var result = await _controller.UpdatePerson(1, person);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    } 
}
