using Microsoft.AspNetCore.Mvc;
using UKParliament.CodeTest.Services;

namespace UKParliament.CodeTest.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly ILogger<DepartmentController> _logger;
    private readonly IDepartmentService _service;

    public DepartmentController(ILogger<DepartmentController> logger, IDepartmentService service)
    {
        _logger = logger;
        _service = service;
    }

    /// <summary>
    /// Get all departments
    /// </summary>
    /// <returns>Returns Ok with list of departments</returns>
    /// <remarks>
    /// 
    ///     GET /api/department
    /// 
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var departments = await _service.GetAllAsync();

        return Ok(departments);
    }
}