using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace InterviewApp;

public class ClientRepository : IClientRepository
{
    public Client GetById(int clientId)
    {
        Client client = null;

        var connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        using var connection = new SqlConnection(connectionString);

        var command = new SqlCommand
        {
            Connection = connection,
            CommandType = CommandType.StoredProcedure,
            CommandText = "GetClientById"
        };

        var clientIdParam = new SqlParameter("@clientId", SqlDbType.Int)
        {
            Value = clientId
        };
        command.Parameters.Add(clientIdParam);
        
        connection.Open();
        using var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
        {
            client = new Client
            {
                Id = (int)reader["ClientId"],
                Name = (string)reader["Name"],
                Tier = (ClientTier)reader["Tier"]
            };
        }

        return client ?? new Client();
    }
}