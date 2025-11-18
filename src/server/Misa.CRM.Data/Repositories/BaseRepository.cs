using System.Data;
using Dapper;
using Misa.CRM.Business.Common.Models;
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
        var tableName = DapperMetadataHelper.GetTableName<T>();

        var columnMappings = DapperMetadataHelper.GetColumnMappings<T>();

        var selectColumns = string.Join(", ", columnMappings.Select(c => $"{c.Value} AS {c.Key}"));

        var sql = $"SELECT {selectColumns} FROM {tableName}";

        using var connection = _context.CreateConnection();
        return connection.Query<T>(sql);
    }

    public virtual string GetBaseQuery()
    {
        var tableName = DapperMetadataHelper.GetTableName<T>();
        var columnMappings = DapperMetadataHelper.GetColumnMappings<T>();
        var selectColumns = string.Join(", ", columnMappings.Select(c => $"{c.Value} AS {c.Key}"));
        return $"SELECT {selectColumns} FROM {tableName}";
    }

    public T GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public PaginatedResult<T> Paginate(string baseQuery, string searchColumns, int pageIndex, int pageSize, string? strSearch, string? sortColumn, int sortDirection)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_base_query", baseQuery);
        parameters.Add("p_search_columns", searchColumns);
        parameters.Add("p_page_index", pageIndex);
        parameters.Add("p_page_size", pageSize);
        parameters.Add("p_search", strSearch);
        parameters.Add("p_sort_column", sortColumn);
        parameters.Add("p_sort_direction", sortDirection);

        using var _connection = _context.CreateConnection();
        var items = _connection.QueryMultiple(
            "proc_paginating_items",
            parameters,
            commandType: CommandType.StoredProcedure
        );

        var totalObj = items.ReadFirst<dynamic>();
        int totalRecords = (int)totalObj.total_records;

        return new PaginatedResult<T>(
            pageIndex,
            pageSize,
            totalRecords,
            [.. items.Read<T>().ToList()]
        );
    }

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
