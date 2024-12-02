using Microsoft.Extensions.Logging;
using Moq;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Services;
using Xunit;

namespace UKParliament.CodeTest.Tests.Services;

public class DepartmentServiceTests
{
    private readonly Mock<ILogger<DepartmentService>> _loggerMock;
    private readonly Mock<IRepository<Department>> _departmentRepositoryMock;
    private readonly DepartmentService _service;

    public DepartmentServiceTests()
    {
        _loggerMock = new Mock<ILogger<DepartmentService>>();
        _departmentRepositoryMock = new Mock<IRepository<Department>>();
        _service = new DepartmentService(_loggerMock.Object, _departmentRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllDepartmentsAsync_ReturnsAllDepartments()
    {
        // Arrange
        var departments = new List<Department>
        {
            new Department { Id = 1, Name = "Department 1" },
            new Department { Id = 2, Name = "Department 2" },
            new Department { Id = 3, Name = "Department 3" }
        };
        _departmentRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(departments);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(departments.Count, result.Count());
        Assert.Equal(departments, result);
    }
}
