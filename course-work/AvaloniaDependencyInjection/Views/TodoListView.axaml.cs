using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaDependencyInjection.ViewModels;

namespace AvaloniaDependencyInjection.Views;


public partial class TodoListView : UserControl
{
    public TodoListView()
    {
        InitializeComponent();
    }

    public TodoListView(TodoListViewModel viewModel, TodoListView todoListView) : this()
    {
        DataContext = viewModel;
    }
}