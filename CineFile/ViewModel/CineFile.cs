using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CineFile
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Film> _films;

        public ObservableCollection<Film> Films
        {
            get { return _films; }
            set
            {
                _films = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            // Initialiser la liste des films ici
            Films = new ObservableCollection<Film>()
            {
                new Film { Title = "Film 1", ImageUrl = "../medias/Ciel Bird.png" },
                new Film { Title = "Film 2", ImageUrl = "../medias/Ciel Verseau.png" }
            };
        }

        // Implémentation de INotifyPropertyChanged pour le data binding
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}