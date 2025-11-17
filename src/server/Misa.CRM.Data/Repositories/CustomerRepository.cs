using System.Data;
using Dapper;
using Misa.CRM.Business.Entities.Common;
using Misa.CRM.Business.Interfaces.Repositories;

namespace Misa.CRM.Data.Repositories;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(MisaDbContext context) : base(context)
    {
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
}
