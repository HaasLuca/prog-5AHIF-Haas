using Avalonia.Controls;
using AvaloniaDependencyInjection.ViewModels;

namespace AvaloniaDependencyInjection.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public MainWindow(MainWindowViewModel viewModel) : this()
    {
        DataContext = viewModel;
    }
}