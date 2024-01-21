namespace CineFile.Model;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;

public class DatabaseService
{
    private MySqlConnection _connection;

    public DatabaseService()
    {
        try
        {
            _connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString);
            // Console.Error.WriteLine($"Reussi lors de la connexion à la base de données.");
        }
        catch (Exception ex)
        {
            // Gérer les erreurs de configuration ou de connexion
            Console.Error.WriteLine($"Erreur lors de la connexion à la base de données.", ex);
        }
    }

    public DataTable ExecuteQuery(string query)
    {
        DataTable dataTable = new DataTable();

        try
        {
            _connection.Open();

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }
        }
        catch (Exception ex)
        {
            // Gérer les erreurs ici
            Console.WriteLine(ex.Message);
        }
        finally
        {
            _connection.Close();
        }

        return dataTable;
    }

    // Ajoutez d'autres méthodes pour les opérations d'écriture (INSERT, UPDATE, DELETE) si nécessaire
}