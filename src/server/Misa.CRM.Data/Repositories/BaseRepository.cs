using System.Data;
using Dapper;
using Misa.CRM.Business.Common.Exceptions;
using Misa.CRM.Business.Common.Models;
using Misa.CRM.Business.Entities;
using Misa.CRM.Business.Helpers;
using Misa.CRM.Business.Interfaces.Repositories;

namespace Misa.CRM.Data.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
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

        var sql = $"SELECT {selectColumns} FROM {tableName} WHERE is_deleted = 0";

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

    public T GetById(string id)
    {
        var tableName = DapperMetadataHelper.GetTableName<T>();
        var columnMappings = DapperMetadataHelper.GetColumnMappings<T>();
        var selectColumns = string.Join(", ", columnMappings.Select(c => $"{c.Value} AS {c.Key}"));
        var primaryKeyName = DapperMetadataHelper.GetPrimaryKey<T>();

        var sql = $"SELECT {selectColumns} FROM {tableName} WHERE {primaryKeyName} = @Id AND is_deleted = 0";

        using var connection = _context.CreateConnection();
        return connection.QueryFirstOrDefault<T>(sql, new { Id = id }) ?? throw new ResourceNotFoundException($"{typeof(T).Name} with Id {id} not found.");
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
        var now = DateTime.Now;
        entity.CreatedAt = now;
        entity.UpdatedAt = now;

        var tableName = DapperMetadataHelper.GetTableName<T>();
        var columnMappings = DapperMetadataHelper.GetColumnMappings<T>();

        var columns = columnMappings.Values.ToList();
        var parameters = columnMappings.Keys.Select(p => "@" + p).ToList();

        // columns.Add("created_at");
        // columns.Add("updated_at");
        // parameters.Add("@CreatedAt");
        // parameters.Add("@UpdatedAt");

        var sql = $"INSERT INTO {tableName} ({string.Join(", ", columns)}) VALUES ({string.Join(", ", parameters)})";

        using var connection = _context.CreateConnection();
        return connection.Execute(sql, entity);
    }

    public int Update(T entity)
    {
        var tableName = DapperMetadataHelper.GetTableName<T>();
        var columnMappings = DapperMetadataHelper.GetColumnMappings<T>();

        var pkProperty = DapperMetadataHelper.GetPrimaryKeyProperty<T>();
        var pkColumn = DapperMetadataHelper.GetPrimaryKey<T>();

        var setClauses = columnMappings
            .Where(c => c.Key != pkProperty && c.Key != "CreatedAt")
            .Select(c => $"{c.Value} = @{c.Key}")
            .ToList();

        var sql = $"UPDATE {tableName} SET {string.Join(", ", setClauses)} WHERE {pkColumn} = @{pkProperty}";

        using var connection = _context.CreateConnection();
        return connection.Execute(sql, entity);
    }

    public int Delete(string id, bool isHardDelete = false)
    {
        var tableName = DapperMetadataHelper.GetTableName<T>();
        var pk = DapperMetadataHelper.GetPrimaryKey<T>();
        var pkProperty = DapperMetadataHelper.GetPrimaryKeyProperty<T>();

        using var connection = _context.CreateConnection();

        if (isHardDelete)
        {
            var sql = $"DELETE FROM {tableName} WHERE {pk} = @{pkProperty}";
            return connection.Execute(sql, new { CustomerId = id });
        }
        else
        {
            var sql = $"UPDATE {tableName} SET is_deleted = 1 WHERE {pk} = @{pkProperty}";
            return connection.Execute(sql, new { CustomerId = id });
        }
    }

    public int Delete(IEnumerable<string> ids, bool isHardDelete = false)
    {
        int affectedRows = 0;
        foreach (var id in ids)
        {
            affectedRows += Delete(id, isHardDelete);
        }
        return affectedRows;
    }

    public bool CheckUnique(string tableName, string columnName, string columnValue, string primaryKeyName, string? primaryKeyValue)
    {
        using var connection = _context.CreateConnection();

        var parameters = new DynamicParameters();
        parameters.Add("p_table_name", tableName);
        parameters.Add("p_column_name", columnName);
        parameters.Add("p_column_value", columnValue);
        parameters.Add("p_primary_key_name", primaryKeyName);
        parameters.Add("p_primary_key_value", primaryKeyValue);

        var count = connection.ExecuteScalar<int>(
            "proc_check_unique",
            parameters,
            commandType: CommandType.StoredProcedure
        );

        return count > 0;
    }
}
