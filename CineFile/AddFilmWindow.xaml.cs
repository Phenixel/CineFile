using System.Windows;
using CineFile.ViewModel;

namespace CineFile;

public partial class AddFilmWindow : Window
{
    public AddFilmWindow()
    {
        InitializeComponent();
        DataContext = new AddFilmViewModel();
    }
}