namespace UKParliament.CodeTest.Data;

public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<bool> CreateAsync(T entity);
    Task UpdateAsync(T entity);
}