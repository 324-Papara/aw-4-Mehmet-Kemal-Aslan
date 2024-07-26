using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Para.Data.Domain;
using Para.Schema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Data.CustomerReportRepository
{
    public class CustomerReportRepository : ICustomerReportRepository
    {
        private readonly IConfiguration _configuration;

        public CustomerReportRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IEnumerable<CustomerReportRequestResponse>> GetCustomerReport()
        {
            await using var connection = new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
            string query = @"SELECT * FROM dbo.Customer c
            LEFT JOIN dbo.CustomerDetails cd ON c.Id = cd.CustomerId
            LEFT JOIN dbo.CustomerAddress ca ON c.Id = ca.CustomerId
            LEFT JOIN dbo.CustomerPhone cp ON c.Id = cp.CustomerId
			WHERE c.IsActive = 1";
            connection.Open();
            var report = await connection.QueryAsync<CustomerReportRequestResponse>(query);
            return report.ToList();
        }
    }
}
