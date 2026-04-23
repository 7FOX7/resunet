using resunet.BLL.Auth;
using resunet.DAL.Models;

namespace Resunet.Tests
{
    public class AuthTests : Helpers.BaseTest
    {
        [SetUp]
        public void Setup() { }

        #region Base Registration 
        [Test]
        public async Task BaseRegistrationTest()
        {
            // create an unique email
            string email = Guid.NewGuid().ToString() + "@test.com";

            // validate the email
            var result = await authBLL.ValidateEmail(email);

            // make sure there's no user with just created email
            Assert.IsNull(result);

            // create a user and get id
            int id = await authBLL.CreateUser(new UserAuth()
            {
                Email = email,
                Password = "Pass123"
            });

            // validate id
            Assert.Greater(id, 0); 

            // validate the email
            result = await authBLL.ValidateEmail(email);

            // make sure there's already a user with the same email
            Assert.IsNotNull(result);

            /* get a user using id and verify it's the same user that just been added */
            UserAuth user = await authBLL.GetUser(id);
            // make sure user's email has email that we initially added
            Assert.AreEqual(email, user.Email);
            // make sure there's a salt
            Assert.IsNotNull(user.Salt);

            /* get a user using email and verify it's the same user that just been added */
            user = await authBLL.GetUser(email);
            // make sure user's email has email that we initially added
            Assert.AreEqual(email, user.Email);
            // make sure there's a salt
            Assert.IsNotNull(user.Salt);

            /* compare passwords */
            Assert.AreEqual(encrypt.HashPassword("Pass123", user.Salt), user.Password); 
        }
        #endregion


        #region Base Login
        [Test]
        public async Task BaseLoginTest()
        {
            // create an unique email
            string email = Guid.NewGuid().ToString() + "@test.com";

            // validate the email
            var result = await authBLL.ValidateEmail(email);

            // make sure there's no user with just created email
            Assert.IsNull(result);

            // create a user and get id
            int id = await authBLL.CreateUser(new UserAuth()
            {
                Email = email,
                Password = "Pass123"
            });

            // get just created user
            UserAuth user = await authBLL.GetUser(id);

            // try to authenticate a user with the wrong email and password
            Assert.Throws<AuthorizationException>(delegate { authBLL.Authenticate("wrong@test.com", "123", false).GetAwaiter().GetResult(); });

            // try to authenticate a user with the right email and wrong password
            Assert.Throws<AuthorizationException>(delegate { authBLL.Authenticate(email, "123", false).GetAwaiter().GetResult(); });

            // try to authenticate a user with the wrong email and right password
            Assert.Throws<AuthorizationException>(delegate { authBLL.Authenticate("wrong@test.com", "Pass123", false).GetAwaiter().GetResult(); });

            // try to authenticate a user with the right email and password
            Assert.AreEqual(await authBLL.Authenticate(email, "Pass123", false), id);
        }
        #endregion
    }
}
