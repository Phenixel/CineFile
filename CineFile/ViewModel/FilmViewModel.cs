using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using CineFile.Model;

namespace CineFile.ViewModel
{
    public class FilmViewModel : ObservableObject
    {
        private DatabaseService _databaseService;

        private ObservableCollection<Film> _films;
        public ObservableCollection<Film> Films
        {
            get { return _films; }
            set { SetProperty(ref _films, value, nameof(Films)); }
        }

        public FilmViewModel()
        {
            _databaseService = new DatabaseService();
            LoadData(); // Appel à une méthode pour charger les données depuis la base de données
        }

        public void LoadData()
        {
            try
            {
                string query = "SELECT titre, realisateur, annee_sortie, categorie_id, lien_image FROM films";
                DataTable result = _databaseService.ExecuteQuery(query);

                if (result.Rows.Count > 0)
                {
                    // Créer une liste de films
                    List<Film> films = new List<Film>();

                    // Parcourir toutes les lignes du résultat
                    foreach (DataRow row in result.Rows)
                    {
                        // Créer un nouvel objet Film et l'ajouter à la liste
                        films.Add(new Film
                        {
                            Titre = Convert.ToString(row["titre"]),
                            Realisateur = Convert.ToString(row["realisateur"]),
                            AnneeSortie = Convert.ToInt32(row["annee_sortie"]),
                            CategorieId = Convert.ToInt32(row["categorie_id"]),
                            LienImage = Convert.ToString(row["lien_image"]),
                        });
                    }

                    // Assigner la liste de films à la propriété Films
                    Films = new ObservableCollection<Film>(films);

                    // Afficher un message de réussite
                    MessageBox.Show("Données chargées avec succès depuis la base de données.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Aucune donnée trouvée
                    MessageBox.Show("Aucune donnée trouvée dans la base de données.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de connexion à la base de données
                MessageBox.Show($"Erreur de connexion à la base de données : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                // Ajoutez des logs supplémentaires ici, par exemple :
                Console.WriteLine($"Error in LoadData: {ex}");
            }
        }
    }
}
