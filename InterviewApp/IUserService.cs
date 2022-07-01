using System;
using System.Threading.Tasks;

namespace InterviewApp
{
    public interface IUserService
    {
       Task<bool> AddUser(string firName, string surname, string email, DateTime birthDate, int clientId);
    }
}
