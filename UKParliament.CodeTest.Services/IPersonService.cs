using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Services;

public interface IPersonService
{
    Task<bool> CreateAsync(Person person);
    Task<Person> GetAsync(int personId);
    Task<IEnumerable<Person>> GetAllAsync();
    Task UpdateAsync(Person person);
}