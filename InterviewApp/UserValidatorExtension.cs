using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewApp
{
    public static class UserValidatorExtension
    {
        public static bool HasValidCredit(this User user) =>
            (user.HasCreditLimit && user.CreditLimit >= 500) || !user.HasCreditLimit;

        public static bool IsValidUser(this User user) => 
            IsValidName(user.FirstName, user.Surname)
            && IsValidEmail(user.EmailAddress)
            && IsValidAge(user.BirthDate);

        private static bool IsValidAge(DateTime birthDate)
        {
            int age = CalculateAge(birthDate);
            if (age < 21)
                return false;
            return true;
        }

        private static bool IsValidName(string firName, string surname)
        {
            if (string.IsNullOrEmpty(firName) || string.IsNullOrEmpty(surname))
                return false;
            return true;
        }

        private static bool IsValidEmail(string email)
        {
            if (!email.Contains("@") && !email.Contains("."))
                return false;
            return true;
        }

        private static int CalculateAge(DateTime birthDate)
        {
            var now = DateTime.Now;
            int age = now.Year - birthDate.Year;

            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                age -= 1;

            return age;
        }
    }
}
