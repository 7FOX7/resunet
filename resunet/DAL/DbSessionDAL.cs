using Dapper;
using Microsoft.Data.SqlClient;
using resunet.DAL.Models;

namespace resunet.DAL
{
    public class DbSessionDAL : IDbSessionDAL
    {
        // will be called when a user successfully registers / logins. We'll be creating a session and add its id to cookies
        public async Task<int> CreateSession(SessionModel sessionModel)
        {
            using (var connection = new SqlConnection(Environment.GetEnvironmentVariable("CONNECTION_STRING__RESUNET", EnvironmentVariableTarget.User)))
            {
                await connection.OpenAsync();

                string sql = @"INSERT INTO DbSession(CreatedAt, LastAccessedAt, UserID) 
                    OUTPUT inserted.DbSessionID
                    VALUES(@CreatedAt, @LastAccessedAt, @UserID)
                ";

                return await connection.QueryFirstOrDefaultAsync<int>(sql, sessionModel);
            }
        }

        // will be called when we want to update (extend) a session for the current user

        // I think this method should not return anything because it will be called at the same time we extend our cookies - session id already exists
        public async Task UpdateSession(int sessionId, DateTime createdAt, DateTime lastAccessedAt)
        {
            using (var connection = new SqlConnection(Environment.GetEnvironmentVariable("CONNECTION_STRING__RESUNET", EnvironmentVariableTarget.User)))
            {
                await connection.OpenAsync();

                string sql = @"UPDATE DbSessionID 
                    SET CreatedAt = @createdAt, LastAccessedAt = @lastAccessedAt
                    WHERE DbSessionID = @sessionId
                ";

                await connection.ExecuteAsync(sql, new { CreatedAt = createdAt, LastAccessedAt = lastAccessedAt, DbSessionID = sessionId });
            }
        }
    }
}
