using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace InterviewApp.Test
{
    [TestClass]
    public class UserServiceTest
    {
        public UserService userService;
        public IClientRepository clientRepository;
        public ICreditServiceClient creditServiceClient;

        [TestInitialize]
        public void TestInit()
        {
            clientRepository = new ClientRepositoryMoq();
            creditServiceClient = new CreditServiceClientMoq();
            userService = new UserService(clientRepository, creditServiceClient);
        }

        [TestMethod]
        [DataRow("Bob", "Roberts", "bob.roberts@email.com", "20000325", 1000)]
        public async Task AddNewUser_SendValidUser_ReturnTrueAsync(string firName, string surname, string email, string birthDate, int clientId)
        {
            var result = await userService.AddUser(firName, surname, email, DateTime.ParseExact(birthDate, "yyyyMMdd", CultureInfo.InvariantCulture), clientId);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("Bob", "Roberts", "bob.roberts@email.com", "20200325" ,1000)]
        [DataRow("", "Roberts", "bob.roberts@email.com", "20200325" ,1000)]
        [DataRow("Bob", "Roberts", "bob.robertsemailcom", "20200325" ,1000)]
        public async Task AddNewUser_SendInvalidUser_ReturnFalseAsync(string firName, string surname, string email, string birthDate, int clientId)
        {
            var result = await userService.AddUser(firName, surname, email, DateTime.ParseExact(birthDate, "yyyyMMdd", CultureInfo.InvariantCulture), clientId);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("Bob", "Roberts", "bob.roberts@email.com", 1000)]
        public async Task AddNewUser_SendInvalidUserUnderAge_ReturnFalseAsync(string firName, string surname, string email, int clientId)
        {
            var result = await userService.AddUser(firName, surname, email, DateTime.Now.AddYears(-21).AddDays(1), clientId);
            Assert.IsFalse(result);
        }
    }
}
