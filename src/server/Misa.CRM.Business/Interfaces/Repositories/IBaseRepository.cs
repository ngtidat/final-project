namespace Misa.CRM.Business.Interfaces.Repositories;

public interface IBaseRepository<T> where T : class
{
    IEnumerable<T> GetAll();

    string GetBaseQuery();

    T GetById(string id);

    int Add(T entity);

    int Update(T entity);

    int Delete(string id, bool isHardDelete = false);

    int Delete(IEnumerable<string> ids, bool isHardDelete = false);

    // bool CheckUnique(string tableName, string columnName, string columnValue, string primaryKeyName, string? primaryKeyValue);
}
