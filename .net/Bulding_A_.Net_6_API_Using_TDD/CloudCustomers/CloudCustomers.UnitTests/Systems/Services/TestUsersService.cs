using CloudCustomers.API.Services;
using System.Threading.Tasks;
using Xunit;

namespace CloudCustomers.UnitTests.Systems.Services
{
    public class TestUsersService
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHTTPGetRequest()
        {
            //Arrange
            var sut = new UsersService();

            //Act
            await sut.GetAllUsers();

            //Assert
            //Verify HttpRequest us made!
        }
    }
}
