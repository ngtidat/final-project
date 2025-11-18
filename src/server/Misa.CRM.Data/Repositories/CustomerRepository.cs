using System.Data;
using Dapper;
using Misa.CRM.Business.Common.Models;
using Misa.CRM.Business.Entities.Common;
using Misa.CRM.Business.Interfaces.Repositories;
using Misa.CRM.Data.SqlQueries;

namespace Misa.CRM.Data.Repositories;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(MisaDbContext context) : base(context)
    {
    }

    public override string GetBaseQuery()
    {
        return CustomerQueries.BaseQuery;
    }

    public IEnumerable<Customer> GetCustomersWithTypeAsync()
    {
        var sql = "proc_cu_get_customers_with_type";
        using var connection = _context.CreateConnection();
        return [.. connection.Query<Customer, CustomerType, Customer> (
            sql,
            map: (customer, customerType) =>
            {
                customer.CustomerType = customerType;
                return customer;
            },
            commandType: CommandType.StoredProcedure,
            splitOn: "CustomerTypeId"
        )];
    }

    public PaginatedResult<Customer> SearchAndPaginate(string? search, int pageIndex, int pageSize, string? sortColumn, int sortDirection)
    {
        using var connection = _context.CreateConnection();

        sortColumn = string.IsNullOrWhiteSpace(sortColumn) ? "c.created_at" : sortColumn;

        var parameters = new DynamicParameters();
        parameters.Add("p_page_index", pageIndex, DbType.Int32);
        parameters.Add("p_page_size", pageSize, DbType.Int32);
        parameters.Add("p_search", search ?? string.Empty, DbType.String);
        parameters.Add("p_sort_column", sortColumn, DbType.String);
        parameters.Add("p_sort_direction", sortDirection, DbType.Int16);

        using var multi = connection.QueryMultiple(
            "proc_paginate_customers",
            parameters,
            commandType: CommandType.StoredProcedure
        );

        // Map Customer + CustomerType using splitOn
        var customers = multi.Read<Customer, CustomerType, Customer>(
            (c, ct) =>
            {
                c.CustomerType = ct;
                return c;
            },
            splitOn: "CustomerTypeId"
        ).ToList();

        // Read total count from second result set
        var totalRecords = multi.ReadFirst<int>();

        return new PaginatedResult<Customer>(
            pageIndex,
            pageSize,
            totalRecords,
            [.. customers]
        );
    }

    // public PaginatedResult<Customer> SearchAndPaginate(int pageIndex, int pageSize, string? strSearch, string? sortColumn, int sortDirection)
    // {
    //     throw new NotImplementedException();
    // }

    // public PaginatedResult<Customer> SearchWithPaginate(int pageIndex, int pageSize, string? strSearch, string? sortColumn, int sortDirection)
    // {
    //     return Paginate(
    //     CustomerQueries.BaseQuery,
    //     pageIndex,
    //     pageSize,
    //     strSearch,
    //     sortColumn,
    //     sortDirection
    //     );
    // }
}
