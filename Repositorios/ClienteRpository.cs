using espacioCliente;
using Microsoft.Data.Sqlite;

namespace espacioClienteRepository;
public class ClienteRepository
{
    string cadenaConexion = @"Data Source=Tienda.db;Cache=Shared";

    public Cliente ObetnerClientePorId(int id)
    {
        Cliente cli = new Cliente();
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            string query = "SELECT * FROM Clientes WHERE ClienteId=@id;";
            SqliteCommand command = new SqliteCommand(query, connection);
            connection.Open();
            command.Parameters.Add(new SqliteParameter("@id", id));
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    cli.ClienteId = Convert.ToInt32(reader["ClienteId"]);
                    cli.Email = reader["Email"].ToString();
                    cli.Nombre= reader["Nombre"].ToString();
                    cli.Telefono= reader["Telefono"].ToString();
                }

            }
            connection.Close();
        }
        return cli;
    }
}