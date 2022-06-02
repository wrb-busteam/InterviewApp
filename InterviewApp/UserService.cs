using System;
using System.Threading.Tasks;

namespace InterviewApp;

public class UserService
{
    public async Task<bool> AddUser(string firName, string surname, string email, DateTime birthDate, int clientId)
    {
        if (string.IsNullOrEmpty(firName) || string.IsNullOrEmpty(surname))
            return false;

        if (!email.Contains("@") && !email.Contains("."))
            return false;

        var now = DateTime.Now;
        int age = now.Year - birthDate.Year;

        if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
            age -= 1;

        if (age < 21)
            return false;

        var clientRepository = new ClientRepository();
        var client = clientRepository.GetById(clientId);

        var user = new User
        {
            Client = client,
            BirthDate = birthDate,
            EmailAddress = email,
            FirstName = firName,
            Surname = surname
        };

        if (client.Tier == ClientTier.Platinum)
            user.HasCreditLimit = false;
        else if (client.Tier == ClientTier.Gold)
        {
            user.HasCreditLimit = true;
            var creditService = new CreditServiceClient();
            var creditLimit = await creditService.GetCreditLimit(user.FirstName, user.Surname, user.BirthDate);
            user.CreditLimit = creditLimit * 2;
        }
        else if (client.Tier == ClientTier.Bronze)
        {
            user.HasCreditLimit = true;
            var creditService = new CreditServiceClient();
            var creditLimit = await creditService.GetCreditLimit(user.FirstName, user.Surname, user.BirthDate);
            user.CreditLimit = creditLimit;
        }

        if (user.HasCreditLimit && user.CreditLimit < 500)
            return false;

        UserDataAccess.AddUser(user);

        return true;
    }
}