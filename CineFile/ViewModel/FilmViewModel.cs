using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CineFile.Model;

namespace CineFile.ViewModel
{
    public class FilmViewModel : ObservableObject
    {
        private DatabaseService _databaseService;
        private ObservableCollection<Film> _allFilms;

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

        private Categorie _selectedCategory;
        public Categorie SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                SetProperty(ref _selectedCategory, value, nameof(SelectedCategory));
                ToggleCategorySelection(SelectedCategory);
            }
        }

        private RelayCommand<Categorie> _toggleCategorySelectionCommand;
        public RelayCommand<Categorie> ToggleCategorySelectionCommand
        {
            get
            {
                return _toggleCategorySelectionCommand ?? (_toggleCategorySelectionCommand = new RelayCommand<Categorie>(ToggleCategorySelection));
            }
        }

        public FilmViewModel()
        {
            _databaseService = new DatabaseService();
            LoadData();
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

                    // Assigner la liste complète des films à la propriété _allFilms
                    _allFilms = Films;

                    // Afficher un message de réussite
                    Console.WriteLine("Données chargées avec succès depuis la base de données.");
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
                Console.WriteLine($"Error in LoadData: {ex}");
            }
        }

        private void ToggleCategorySelection(Categorie clickedCategory)
        {
            // Inverser la sélection de la catégorie cliquée
            clickedCategory.IsSelected = !clickedCategory.IsSelected;

            // Filtrer la liste des films en fonction des catégories sélectionnées
            var selectedCategories = Categories.Where(c => c.IsSelected).ToList();

            if (selectedCategories.Count > 0)
            {
                var filteredFilms = _allFilms.Where(film => selectedCategories.Any(category => film.CategorieId == category.CategorieId)).ToList();
                Films = new ObservableCollection<Film>(filteredFilms);
            }
            else
            {
                // Si aucune catégorie n'est sélectionnée, réinitialiser la liste complète des films
                Films = _allFilms;
            }

            // Rafraîchir l'affichage
            RaisePropertyChanged(nameof(Films));
        }

    }
}
