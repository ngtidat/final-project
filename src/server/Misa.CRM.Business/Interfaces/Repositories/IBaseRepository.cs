using Misa.CRM.Business.Common.Models;

namespace Misa.CRM.Business.Interfaces.Repositories;

public interface IBaseRepository<T> where T : class
{
    IEnumerable<T> GetAll();

    string GetBaseQuery();

    T GetById(Guid id);

    int Add(T entity);

    int Update(T entity);

    int Delete(T entity, bool isHardDelete = false);

    int Delete(IEnumerable<T> Entities, bool isHardDelete = false);
}
