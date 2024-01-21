using System;
using System.Windows;
using CineFile.Model;

namespace CineFile.ViewModel
{
    public class AddFilmViewModel : ObservableObject
    {
        private string _titre;
        private string _realisateur;
        private int _anneeSortie;
        private int _selectedCategorieId;
        private string _lienImage;
        private DatabaseService _databaseService;

        public string Titre
        {
            get { return _titre; }
            set { SetProperty(ref _titre, value, nameof(Titre)); }
        }

        public string Realisateur
        {
            get { return _realisateur; }
            set { SetProperty(ref _realisateur, value, nameof(Realisateur)); }
        }

        public int AnneeSortie
        {
            get { return _anneeSortie; }
            set { SetProperty(ref _anneeSortie, value, nameof(AnneeSortie)); }
        }

        public int SelectedCategorieId
        {
            get { return _selectedCategorieId; }
            set { SetProperty(ref _selectedCategorieId, value, nameof(SelectedCategorieId)); }
        }

        public string LienImage
        {
            get { return _lienImage; }
            set { SetProperty(ref _lienImage, value, nameof(LienImage)); }
        }

        private RelayCommand<object> _addFilmCommand;
        public RelayCommand<object> AddFilmCommand
        {
            get
            {
                return _addFilmCommand ?? (_addFilmCommand = new RelayCommand<object>(AddFilm));
            }
        }

        public AddFilmViewModel()
        {
            _databaseService = new DatabaseService();
        }

        private void AddFilm(object parameter)
        {
            try
            {
                // Vérifiez que les champs requis sont remplis avant d'ajouter le film
                if (string.IsNullOrWhiteSpace(Titre) || string.IsNullOrWhiteSpace(Realisateur) || AnneeSortie <= 0 || SelectedCategorieId <= 0)
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Créez un nouvel objet Film avec les données saisies
                Film newFilm = new Film
                {
                    Titre = Titre,
                    Realisateur = Realisateur,
                    AnneeSortie = AnneeSortie,
                    CategorieId = SelectedCategorieId,
                    LienImage = LienImage
                };

                // Ajoutez le film à la base de données
                _databaseService.AddFilm(newFilm);

                MessageBox.Show("Le film a été ajouté avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ajout du film : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
