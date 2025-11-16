using System.Data;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Misa.CRM.Data;

public class MisaDbContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public MisaDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("MyConnection")!;
    }

    public IDbConnection CreateConnection()
        => new MySqlConnection(_connectionString);
}
