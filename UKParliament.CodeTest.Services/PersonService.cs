using Microsoft.Extensions.Logging;
using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Services;

public class PersonService : IPersonService
{
    private readonly ILogger<PersonService> _logger;
    private readonly IRepository<Person> _repository;

    public PersonService(ILogger<PersonService> logger, IRepository<Person> repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<bool> CreateAsync(Person person)
    {
      return await _repository.CreateAsync(person);
    }

    public async Task UpdateAsync(Person person)
    {
        await _repository.UpdateAsync(person);
    }

    public async Task<Person> GetAsync(int personId)
    {
        return await _repository.GetByIdAsync(personId);
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
}