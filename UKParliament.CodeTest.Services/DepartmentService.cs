using Microsoft.Extensions.Logging;
using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Services;

public class DepartmentService : IDepartmentService
{
    private readonly ILogger<DepartmentService> _logger;
    private readonly IRepository<Department> _repository;

    public DepartmentService(ILogger<DepartmentService> logger, IRepository<Department> repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<IEnumerable<Department>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
}