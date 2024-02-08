using Api.Models;

/// <summary>
/// Contract of generic repository
/// Other CRUD operations might be added, but are not needed for this task
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> where T : WithId
{
    public Task<T?>Get(int id);

    public Task<IEnumerable<T>> GetAll();

    // TODO: insert, delete, update, but not needed for this task
}
