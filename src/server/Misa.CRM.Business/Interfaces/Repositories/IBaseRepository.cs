namespace Misa.CRM.Business.Interfaces.Repositories;

public interface IBaseRepository<T> where T : class
{
    IEnumerable<T> GetAll();

    string getQuery();

    // PaginatedResult<T> Paginate(int pageNumber, int pageSize, out int totalRecords);

    T GetById(Guid id);

    int Add(T entity);

    int Update(T entity);

    int Delete(T entity, bool isHardDelete = false);

    int Delete(IEnumerable<T> Entities, bool isHardDelete = false);
}
