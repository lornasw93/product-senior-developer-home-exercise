using Microsoft.AspNetCore.Mvc;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Web.Validators;
using UKParliament.CodeTest.Web.ViewModels;

namespace UKParliament.CodeTest.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly ILogger<PersonController> _logger;
    private readonly IPersonService _service;

    public PersonController(ILogger<PersonController> logger, IPersonService service)
    {
        _logger = logger;
        _service = service;
    }

    /// <summary>
    /// Add a new person
    /// </summary>
    /// <param name="person">Person to add</param>
    /// <returns>Returns Ok with product or bad request</returns>
    /// <remarks>
    /// 
    ///     POST /api/person
    ///     {
    ///         "title": "Mr",
    ///         "firstName": "Alex",
    ///         "lastName": "Wilson",
    ///         "dateOfBirth": "2011-11-11T00:00:00.000Z",
    ///         "email": "alexandra@pal.com",
    ///         "departmentId": "2"
    ///     }
    /// 
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePerson([FromBody] PersonViewModel person)
    {
        var validationResult = new PersonValidator().Validate(person);
        if (!validationResult.IsValid)
        {
            _logger.LogError("Invalid validation: {errors}", validationResult.Errors);

            return BadRequest(validationResult.Errors);
        }

        var isCreated = await _service.CreateAsync(new Person
        {
            Title = person.Title,
            FirstName = person.FirstName,
            LastName = person.LastName,
            DateOfBirth = person.DateOfBirth,
            Email = person.Email,
            DepartmentId = person.DepartmentId
        });

        if (!isCreated)
        { 
            _logger.LogError("Failed to create person: {person}", person);
            return BadRequest("Failed to create person");
        }
        else
        {
            return Ok(person);
        }
    }

    /// <summary>
    /// Get a person by Id
    /// </summary>
    /// <param name="id">Id of person</param>
    /// <returns>Returns Ok with product or not found</returns>
    /// <remarks>
    /// 
    ///     GET /api/person/1
    ///     
    /// </remarks>
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<IActionResult> GetPerson(int id)
    {
        var person = await _service.GetAsync(id);
        if (person == null)
        {
            _logger.LogError("Failed to retrieve person: {person}", person);
            return NotFound();
        }

        return Ok(person);
    }

    /// <summary>
    /// Get all people
    /// </summary>
    /// <returns>Returns Ok with list of people</returns>
    /// <remarks>
    /// 
    ///     GET /api/person
    /// 
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPeople()
    {
        var people = await _service.GetAllAsync();

        return Ok(people);
    }

    /// <summary>
    /// Update a person
    /// </summary>
    /// <param name="id">Id of person</param>
    /// <param name="person">Person to update</param>
    /// <returns>Returns Ok if successful, bad request or not found</returns>
    /// <remarks>
    /// 
    ///     PUT /api/person/1
    ///     {
    ///         "title": "Mr",
    ///         "firstName": "Alex",
    ///         "lastName": "Wilson",
    ///         "dateOfBirth": "2011-11-11T00:00:00.000Z",
    ///         "email": "alexandra@pal.com",
    ///         "departmentId": "2"
    ///     }
    /// 
    /// </remarks>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePerson(int id, [FromBody] PersonViewModel personViewModel)
    {
        var validationResult = new PersonValidator().Validate(personViewModel);
        if (!validationResult.IsValid)
        {
            _logger.LogError("Invalid validation: {errors}", validationResult.Errors);

            return BadRequest(validationResult.Errors);
        }

        var person = await _service.GetAsync(id);
        if (person == null)
        {
            _logger.LogError("Failed to find person to update: {person}", person);

            return NotFound();
        }

        person.Title = personViewModel.Title;
        person.FirstName = personViewModel.FirstName;
        person.LastName = personViewModel.LastName;
        person.DateOfBirth = personViewModel.DateOfBirth;
        person.Email = personViewModel.Email;
        person.DepartmentId = personViewModel.DepartmentId;

        await _service.UpdateAsync(person);

        return Ok();
    }
}