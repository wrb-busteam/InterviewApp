using System;
using System.Threading.Tasks;

namespace InterviewApp.Test
{
    public class CreditServiceClientMoq : ICreditServiceClient
    {
        public Task<int> GetCreditLimit(string firstName, string surname, DateTime birthDate)
        {
            throw new NotImplementedException();
        }
    }
}
