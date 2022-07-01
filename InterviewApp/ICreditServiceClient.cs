using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewApp
{
    public interface ICreditServiceClient
    {
        Task<int> GetCreditLimit(string firstName, string surname, DateTime birthDate);
    }
}
