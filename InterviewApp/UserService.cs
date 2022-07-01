using System;
using System.Threading.Tasks;

namespace InterviewApp;

public class UserService : IUserService
{
    public IClientRepository clientRepository;
    public ICreditServiceClient creditService;

    public UserService(IClientRepository? clientRepository = null,
        ICreditServiceClient? creditService = null)
    {
        this.clientRepository = clientRepository ?? new ClientRepository();
        this.creditService = creditService ?? new CreditServiceClient();
    }

    public async Task<bool> AddUser(string firName, string surname, string email, DateTime birthDate, int clientId)
    {       
        var user = new User
        {           
            BirthDate = birthDate,
            EmailAddress = email,
            FirstName = firName,
            Surname = surname
        };

        if (!user.IsValidUser()) return false;

        user.Client = clientRepository.GetById(clientId);

        await GetCreditLimitAsync(user.Client.Tier,user);

        if (!user.HasValidCredit())  return false;

        UserDataAccess.AddUser(user);

        return true;
    }

    private async Task GetCreditLimitAsync(ClientTier tier, User user)
    {
        switch (tier)
        {
            case ClientTier.Platinum:
                user.HasCreditLimit = false;
                break;
            case ClientTier.Gold:
                user.HasCreditLimit = true;           
                var creditLimit = await creditService.GetCreditLimit(user.FirstName, user.Surname, user.BirthDate);
                user.CreditLimit = creditLimit * 2;
                break;
            case ClientTier.Bronze:
                user.HasCreditLimit = true;                
                user.CreditLimit = await creditService.GetCreditLimit(user.FirstName, user.Surname, user.BirthDate);
                break;
        }
    }
}