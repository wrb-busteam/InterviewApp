using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace InterviewApp;

public static class UserDataAccess
{ 
    public static void AddUser(User user)
    {
        var connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        using var connection = new SqlConnection(connectionString);

        var command = new SqlCommand
        {
            Connection = connection,
            CommandType = CommandType.StoredProcedure,
            CommandText = "AddUser"
        };

        var firstNameParam = new SqlParameter("@firstName", SqlDbType.NVarChar, 50)
        {
            Value = user.FirstName
        };
        command.Parameters.Add(firstNameParam);
        
        var surnameParam = new SqlParameter("@surName", SqlDbType.NVarChar, 50)
        {
            Value = user.Surname
        };
        command.Parameters.Add(surnameParam);
        
        var birthDateParam = new SqlParameter("@birthDate", SqlDbType.DateTime)
        {
            Value = user.BirthDate
        };
        command.Parameters.Add(birthDateParam);
        
        var emailParam = new SqlParameter("@email", SqlDbType.NVarChar, 75)
        {
            Value = user.EmailAddress
        };
        command.Parameters.Add(emailParam);
        
        var creditLimitParam = new SqlParameter("@creditLimit", SqlDbType.Int)
        {
            Value = user.CreditLimit
        };
        command.Parameters.Add(creditLimitParam);
        
        var hasCreditLimitParam = new SqlParameter("@hasCreditLimit", SqlDbType.Bit)
        {
            Value = user.HasCreditLimit
        };
        command.Parameters.Add(hasCreditLimitParam);
        
        var clientIdParam = new SqlParameter("@clientId", SqlDbType.Int)
        {
            Value = user.Client.Id
        };
        command.Parameters.Add(clientIdParam);
        
        connection.Open();
        command.ExecuteNonQuery();
    }
}