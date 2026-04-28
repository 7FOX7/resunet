using Dapper;
using Microsoft.Data.SqlClient;

namespace resunet.DAL
{
    public class DbHelper
    {
        // scalar accepts one or more params and returns exactly one value
        public static async Task<int> ExecuteScalarAsync(string sql, object model)
        {
            using (var connection = new SqlConnection(Environment.GetEnvironmentVariable("CONNECTION_STRING__RESUNET", EnvironmentVariableTarget.User)))
            {
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<int>(sql, model);
            }
        }

        // return MULTIPLE results of type T (you can then return a single or the entire collection to the client in DAL methods)
        public static async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sql, object model)  
        {
            using (var connection = new SqlConnection(Environment.GetEnvironmentVariable("CONNECTION_STRING__RESUNET", EnvironmentVariableTarget.User)))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<T>(sql, model);
            }
        }
    }
}
