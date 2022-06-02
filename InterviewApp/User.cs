using System;

namespace InterviewApp;

public class User
{
    public Client Client { get; set; }
    public DateTime BirthDate { get; set; }
    public string EmailAddress { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public bool HasCreditLimit { get; set; }
    public int CreditLimit { get; set; }
}