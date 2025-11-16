using Dapper;
using Misa.CRM.Business.Helpers;
using Misa.CRM.Business.Interfaces.Repositories;

namespace Misa.CRM.Data.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly MisaDbContext _context;

    public BaseRepository(MisaDbContext context)
    {
        _context = context;
    }

    public IEnumerable<T> GetAll()
    {
        // Lấy tên bảng
        var tableName = DapperMetadataHelper.GetTableName<T>();

        // Lấy danh sách cột có attribute / override
        var columnMappings = DapperMetadataHelper.GetColumnMappings<T>();

        // Build SELECT: customer_type_id AS Id, customer_type_name AS CustomerTypeName
        var selectColumns = string.Join(", ", columnMappings.Select(c => $"{c.Value} AS {c.Key}"));

        var sql = $"SELECT {selectColumns} FROM {tableName}";

        using var connection = _context.CreateConnection();
        return connection.Query<T>(sql);
    }

    public string getQuery()
    {
        // Lấy tên bảng
        var tableName = DapperMetadataHelper.GetTableName<T>();

        // Lấy danh sách cột có attribute / override
        var columnMappings = DapperMetadataHelper.GetColumnMappings<T>();

        // Build SELECT: customer_type_id AS Id, customer_type_name AS CustomerTypeName
        var selectColumns = string.Join(", ", columnMappings.Select(c => $"{c.Value} AS {c.Key}"));

        var sql = $"SELECT {selectColumns} FROM {tableName}";

        return sql;
    }

    public T GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    // public PaginatedResult<T> Paginate(int pageNumber, int pageSize, out int totalRecords)
    // {
    //     throw new NotImplementedException();
    // }

    public int Add(T entity)
    {
        throw new NotImplementedException();
    }

    public int Update(T entity)
    {
        throw new NotImplementedException();
    }

    public int Delete(T entity, bool isHardDelete = false)
    {
        throw new NotImplementedException();
    }

    public int Delete(IEnumerable<T> Entities, bool isHardDelete = false)
    {
        throw new NotImplementedException();
    }
}
