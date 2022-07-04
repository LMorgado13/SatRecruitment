using Sat.Recruitment.Api.Clases;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.DTOs;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1:UsersController
    {
        [Fact]
        public void Test1()
        {
            //var userController = new UsersController();

            ClsCreateUser objCU = new ClsCreateUser();
            var result = objCU.CreateUser("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            Assert.Equal(true, result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public void Test2()
        {
            //var userController = new UsersController();

            ClsCreateUser objCU = new ClsCreateUser();
            var result = objCU.CreateUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");


            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }
    }
}
