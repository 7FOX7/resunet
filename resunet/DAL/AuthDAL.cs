using resunet.DAL.Models;

namespace resunet.DAL
{
    public class AuthDAL : IAuthDAL
    {
        public async Task<UserAuth> GetUser(string email)
        {
            string sql = @"SELECT UserID, Email, Password, Salt, Status 
                    FROM AppUser 
                    WHERE Email = @email
                ";

            // return an existing or anonymous user
            return (await DbHelper.ExecuteQueryAsync<UserAuth>(sql, new { email = email })).FirstOrDefault() ?? new UserAuth();
        }

        public async Task<UserAuth> GetUser(int id)
        {
            string sql = @"SELECT UserID, Email, Password, Salt, Status 
                    FROM AppUser 
                    WHERE UserID = @id
                ";

            // return an existing or anonymous user
            return (await DbHelper.ExecuteQueryAsync<UserAuth>(sql, new { id = id })).FirstOrDefault() ?? new UserAuth();
        }

        // create a user based on the model and return their id
        public async Task<int> CreateUser(UserAuth userAuth)
        {
            string sql = @"INSERT INTO AppUser(Email, Password, Salt, Status) 
                    OUTPUT inserted.UserID
                    VALUES(@Email, @Password, @Salt, @Status)
                ";

            return await DbHelper.ExecuteScalarAsync(sql, userAuth); 
        }
    }
}
