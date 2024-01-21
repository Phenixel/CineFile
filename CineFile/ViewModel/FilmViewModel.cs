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

        private ObservableCollection<Categorie> _categories;
        public ObservableCollection<Categorie> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value, nameof(Categories)); }
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
                // Chargez les films
                string filmQuery = "SELECT titre, realisateur, annee_sortie, categorie_id, lien_image FROM films";
                DataTable filmResult = _databaseService.ExecuteQuery(filmQuery);

                // Chargez les catégories
                string categoryQuery = "SELECT categorie_id, nom_categorie FROM categories";
                DataTable categoryResult = _databaseService.ExecuteQuery(categoryQuery);

                if (filmResult.Rows.Count > 0)
                {
                    // Créer une liste de films
                    List<Film> films = new List<Film>();

                    // Parcourir toutes les lignes du résultat des films
                    foreach (DataRow row in filmResult.Rows)
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
                    // Aucune donnée trouvée pour les films
                    MessageBox.Show("Aucune donnée trouvée dans la base de données pour les films.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                if (categoryResult.Rows.Count > 0)
                {
                    // Créer une liste de catégories
                    List<Categorie> categories = new List<Categorie>();

                    // Parcourir toutes les lignes du résultat des catégories
                    foreach (DataRow row in categoryResult.Rows)
                    {
                        // Créer une nouvelle catégorie et l'ajouter à la liste
                        categories.Add(new Categorie
                        {
                            CategorieId = Convert.ToInt32(row["categorie_id"]),
                            Nom = Convert.ToString(row["nom_categorie"])
                        });
                    }

                    // Assigner la liste de catégories à la propriété Categories
                    Categories = new ObservableCollection<Categorie>(categories);
                }
                else
                {
                    // Aucune donnée trouvée pour les catégories
                    MessageBox.Show("Aucune donnée trouvée dans la base de données pour les catégories.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de connexion à la base de données
                MessageBox.Show($"Erreur de connexion à la base de données : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                // Ajouter des logs supplémentaires ici, par exemple :
                Console.WriteLine($"Error in LoadData: {ex}");
            }
        }
    }
}

