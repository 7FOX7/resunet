using resunet.DAL.Models;

namespace Resunet.Tests
{
    public class RegisterTests : Helpers.BaseTest
    {
        [SetUp]
        public void Setup() { }

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

            // make sure there's a user in the db
            Assert.IsNotNull(result); 

            // get a user using id
            UserAuth user = await authBLL.GetUser(id);

            // verify passwords
            Assert.IsTrue(user.Password == encrypt.HashPassword("Pass123", user.Salt)); 
        }
    }
}
