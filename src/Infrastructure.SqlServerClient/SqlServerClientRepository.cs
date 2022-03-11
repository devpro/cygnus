using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Cygnus.Domain.Models;
using Cygnus.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Cygnus.Infrastructure.SqlServerClient
{
    public class SqlServerClientRepository : IDataSynchronizationRepository
    {
        private readonly ILogger<SqlServerClientRepository> _logger;

        private readonly SqlServerClientConfiguration _configuration;

        public SqlServerClientRepository(ILogger<SqlServerClientRepository> logger, SqlServerClientConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<List<Dictionary<string, string>>> ReadAsync(SourceModel source, List<FieldModel> fields, string correlationField)
        {
            try
            {
                var builder = new SqlConnectionStringBuilder
                {
                    DataSource = _configuration.DataSource,
                    UserID = _configuration.UserId,
                    Password = _configuration.Password,
                    InitialCatalog = _configuration.InitialCatalog
                };

                using var connection = new SqlConnection(builder.ConnectionString);

                connection.Open();

                var data = new List<Dictionary<string, string>>();
                var correlationFieldValues = new List<string>();

                using var command = new SqlCommand(source.Query, connection);
                using var reader = command.ExecuteReader();
                while (await reader.ReadAsync())
                {
                    // avoid issues with duplicates in source
                    if (!correlationFieldValues.Contains(reader[correlationField].ToString()))
                    {
                        data.Add(ReadEntityFields(reader, fields));
                        correlationFieldValues.Add(reader[correlationField].ToString());
                    }
                }

                return data;
            }
            catch (Exception exc)
            {
                _logger.LogWarning("An error is raised on SQL Server database call [DataSource={DataSource}] [ExceptionMessage={ExceptionMessage}] [SqlQuery={SqlQuery}]",
                    _configuration.DataSource, exc.Message, source.Query);
                throw;
            }
        }

        public Task WriteAsync(List<Dictionary<string, string>> data, DestinationModel destination)
        {
            throw new NotImplementedException();
        }

        private Dictionary<string, string> ReadEntityFields(SqlDataReader reader, List<FieldModel> fields)
        {
            var output = new Dictionary<string, string>();
            foreach (var field in fields)
            {
                output[field.Name] = reader[field.MapFrom].ToString();
            }
            return output;
        }
    }
}
