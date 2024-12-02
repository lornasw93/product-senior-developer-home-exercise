using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Web.Controllers;
using Xunit;

namespace UKParliament.CodeTest.Tests.Controllers;

public class DepartmentControllerTests
{
    private readonly Mock<IDepartmentService> _serviceMock;
    private readonly DepartmentController _controller;

    public DepartmentControllerTests()
    {
        _serviceMock = new Mock<IDepartmentService>();
        _controller = new DepartmentController(Mock.Of<ILogger<DepartmentController>>(), _serviceMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkWithDepartments()
    {
        // Arrange
        var departments = new List<Department>
        {
            new Department { Id = 1, Name = "Department 1" },
            new Department { Id = 2, Name = "Department 2" }
        };

        _serviceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(departments);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Department>>(okResult.Value);
        Assert.Equal(departments.Count, model.Count());
    }
}
