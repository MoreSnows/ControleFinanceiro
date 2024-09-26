using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace ControleFinanceiro.Infrastructure.Database
{
    public class DbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(string connectionString) =>
            _connectionString = connectionString;

        public IDbConnection GetConnection() =>
            new SqlConnection(_connectionString);
    }
}
