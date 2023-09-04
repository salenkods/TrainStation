using GuiApp.ViewModels;
using System.Windows;

namespace GuiApp;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private MainViewModel? viewModel;

    public MainWindow() {
        InitializeComponent();
    }

    private void OnLoaded(object sender, RoutedEventArgs e) {
        viewModel = ServiceLocator.GetInstance<MainViewModel>();
        DataContext = viewModel;

        viewModel.Activate();
    }
}
