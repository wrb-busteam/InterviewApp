using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace InterviewApp;

public class CreditServiceClient
{
    public async Task<int> GetCreditLimit(string firstName, string surname, DateTime birthDate)
    {
        var url = $"{ConfigurationManager.AppSettings["CreditServiceUrl"]}/credit";

        HttpClient client = new HttpClient();
        var x = await client.PostAsync(url, new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "FirstName", firstName },
            { "LastName", surname },
            { "BirthDate", birthDate.Date.ToString("s") },
        }));

        var result = await x.Content.ReadAsStringAsync();
        return int.Parse(result);
    }
}