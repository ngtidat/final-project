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

    public new Customer GetById(string id)
    {
        using var connection = _context.CreateConnection();
        var result = connection.Query<Customer, CustomerType, Customer>(
        sql: "proc_get_customer_by_id",
        map: (customer, customerType) =>
            {
                customer.CustomerType = customerType;
                return customer;
            },
            param: new { p_customer_id = id },  
            commandType: CommandType.StoredProcedure,
            splitOn: "CustomerTypeId"
        ).FirstOrDefault();

        return result;
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

    // Override Add để dùng proc_add_customer
    public new int Add(Customer entity)
    {
        using var connection = _context.CreateConnection();

        entity.CreatedAt = DateTime.Now;

        var parameters = new DynamicParameters();
        // parameters.Add("p_customer_name")
        parameters.Add("p_customer_name", entity.CustomerName);
        parameters.Add("p_customer_address", entity.CustomerAddress);
        parameters.Add("p_customer_phone", entity.CustomerPhone);
        parameters.Add("p_customer_email", entity.CustomerEmail);
        parameters.Add("p_customer_tax_code", entity.CustomerTaxCode);
        parameters.Add("p_customer_type_id", entity.CustomerTypeId);
        parameters.Add("p_customer_industry", entity.CustomerIndustry);
        parameters.Add("p_gender", entity.Gender);
        parameters.Add("p_other_phone_number", entity.OtherPhoneNumber);
        parameters.Add("p_last_purchase_date", entity.LastPurchaseDate);
        parameters.Add("p_purchase_items", entity.PurchaseItems);
        parameters.Add("p_purchase_item_name", entity.PurchaseItemName);
        parameters.Add("p_shipping_address", entity.ShippingAddress);
        parameters.Add("p_created_at", entity.CreatedAt);

        return connection.Execute(
            "proc_add_customer",
            parameters,
            commandType: CommandType.StoredProcedure
        );
    }

    public ImportResult Import(List<Customer> customers)
    {
        var result = new ImportResult();
        result.Total = customers.Count;

        using var connection = _context.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction();

        try
        {
            int index = 0;
            foreach (var customer in customers)
            {
                index++;
                customer.CreatedAt = DateTime.Now;
                var parameters = new DynamicParameters();
                parameters.Add("p_customer_name", customer.CustomerName);
                parameters.Add("p_customer_address", customer.CustomerAddress);
                parameters.Add("p_customer_phone", customer.CustomerPhone);
                parameters.Add("p_customer_email", customer.CustomerEmail);
                parameters.Add("p_customer_tax_code", customer.CustomerTaxCode);
                parameters.Add("p_customer_type_id", customer.CustomerTypeId);
                parameters.Add("p_customer_industry", customer.CustomerIndustry);
                parameters.Add("p_gender", customer.Gender);
                parameters.Add("p_other_phone_number", customer.OtherPhoneNumber);
                parameters.Add("p_last_purchase_date", customer.LastPurchaseDate);
                parameters.Add("p_purchase_items", customer.PurchaseItems);
                parameters.Add("p_purchase_item_name", customer.PurchaseItemName);
                parameters.Add("p_shipping_address", customer.ShippingAddress);
                parameters.Add("p_created_at", customer.CreatedAt);

                connection.ExecuteScalar<string>(
                    "proc_add_customer",
                    parameters,
                    commandType: CommandType.StoredProcedure,
                    transaction: transaction
                );

                result.Success++;
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }


        return result;
    }

    public string GetNewCustomerId()
    {
        using var connection = _context.CreateConnection();

        // Nếu func_cu_gen_customer_id là function trong SQL Server
        var sql = "SELECT func_cu_gen_customer_id()";

        // Dapper QuerySingleOrDefault sẽ trả về 1 giá trị duy nhất
        var newCustomerId = connection.QuerySingleOrDefault<string>(sql);

        return newCustomerId!;
    }

    public int CheckEmailUnique(string email)
    {
        using var connection = _context.CreateConnection();

        var sql = "SELECT func_check_email_unique(@Email);";

        var result = connection.ExecuteScalar<int>(sql, new { Email = email });

        return result;
    }

    public int CheckPhoneUnique(string phone)
    {
        var sql = "SELECT func_check_phone_unique(@Phone);";

        using var connection = _context.CreateConnection();
        var result = connection.ExecuteScalar<int>(sql, new { Phone = phone });

        return result;
    }
}
