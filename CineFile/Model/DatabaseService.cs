using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows;
using CineFile.ViewModel;

namespace CineFile.Model
{
    public class DatabaseService
    {
        private MySqlConnection _connection;

        public DatabaseService()
        {
            try
            {
                _connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString);
            }
            catch (Exception ex)
            {
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
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return dataTable;
        }

        public void ExecuteNonQuery(string query)
        {
            try
            {
                _connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, _connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public List<Categorie> GetCategories()
        {
            List<Categorie> categories = new List<Categorie>();

            try
            {
                _connection.Open();

                string query = "SELECT * FROM categories";
                using (MySqlCommand command = new MySqlCommand(query, _connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Categorie categorie = new Categorie
                            {
                                CategorieId = Convert.ToInt32(reader["categorie_id"]),
                                Nom = Convert.ToString(reader["nom_categorie"])
                            };

                            categories.Add(categorie);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return categories;
        }

        public void AddFilm(Film newFilm)
        {
            try
            {
                _connection.Open();

                string addFilmQuery = $"INSERT INTO films (titre, realisateur, annee_sortie, categorie_id, lien_image) VALUES ('{newFilm.Titre}', '{newFilm.Realisateur}', {newFilm.AnneeSortie}, {newFilm.CategorieId}, '{newFilm.LienImage}')";
                ExecuteNonQuery(addFilmQuery);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void UpdateFilm(Film updatedFilm)
        {
            try
            {
                _connection.Open();

                string updateFilmQuery = $"UPDATE films SET titre='{updatedFilm.Titre}', realisateur='{updatedFilm.Realisateur}', annee_sortie={updatedFilm.AnneeSortie}, categorie_id={updatedFilm.CategorieId}, lien_image='{updatedFilm.LienImage}' WHERE film_id={updatedFilm.FilmId}";
                ExecuteNonQuery(updateFilmQuery);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void DeleteFilm(int filmId)
        {
            try
            {
                _connection.Open();

                string deleteFilmQuery = $"DELETE FROM films WHERE film_id={filmId}";
                ExecuteNonQuery(deleteFilmQuery);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        // Ajoutez d'autres méthodes pour les opérations de mise à jour (UPDATE) et suppression (DELETE) si nécessaire
    }
}
