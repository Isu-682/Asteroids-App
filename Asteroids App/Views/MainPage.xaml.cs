using Asteroids_App.ViewModels;

namespace Asteroids_App.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new NeoViewModel();
    }
}