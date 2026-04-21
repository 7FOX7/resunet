using resunet.DAL.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace resunet.DAL
{
    public class AuthDAL : IAuthDAL
    {
        public async Task<UserAuth> GetUser(string email)
        {
            using (var connection = new SqlConnection(Environment.GetEnvironmentVariable("CONNECTION_STRING__RESUNET", EnvironmentVariableTarget.User)))
            {
                await connection.OpenAsync();

                // return an existing or anonymous user
                return await connection.QueryFirstOrDefaultAsync<UserAuth>(
                    @"SELECT UserID, Email, Password, Salt, Status 
                    FROM AppUser 
                    WHERE Email = @email", new { email = email }
                ) ?? new UserAuth(); 
            }
        }

        public async Task<UserAuth> GetUser(int id)
        {
            using (var connection = new SqlConnection(Environment.GetEnvironmentVariable("CONNECTION_STRING__RESUNET", EnvironmentVariableTarget.User)))
            {
                await connection.OpenAsync();

                // return an existing or anonymous user
                return await connection.QueryFirstOrDefaultAsync<UserAuth>(
                    @"SELECT UserID, Email, Password, Salt, Status 
                    FROM AppUser 
                    WHERE UserID = @id", new { id = id }
                ) ?? new UserAuth();
            }
        }

        // create a user based on the model and return their id
        public async Task<int> CreateUser(UserAuth userAuth)
        {
            using (var connection = new SqlConnection(Environment.GetEnvironmentVariable("CONNECTION_STRING__RESUNET", EnvironmentVariableTarget.User)))
            {
                await connection.OpenAsync();

                string sql = @"INSERT INTO AppUser(Email, Password, Salt, Status) 
                    OUTPUT inserted.UserID
                    VALUES(@Email, @Password, @Salt, @Status)
                ";

                return await connection.QueryFirstOrDefaultAsync<int>(sql, userAuth); 
            }
        }
    }
}
