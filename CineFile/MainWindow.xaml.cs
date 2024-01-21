using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CineFile.ViewModel;

using System.Windows;

namespace CineFile
{
    public partial class MainWindow : Window
    {
        private FilmViewModel _filmViewModel;

        public MainWindow()
        {
            InitializeComponent();

            _filmViewModel = new FilmViewModel();
            DataContext = _filmViewModel; // Définir le contexte de données pour le DataContext
        }

        private void AddFilmMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Créez une instance de AddFilmWindow et affichez-la
            var addFilmWindow = new AddFilmWindow();
            addFilmWindow.ShowDialog();
        }
    }
}
