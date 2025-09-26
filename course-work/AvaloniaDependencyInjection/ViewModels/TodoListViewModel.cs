using System.Collections.ObjectModel;
using AvaloniaDependencyInjection.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaDependencyInjection.ViewModels;

public record TodoItem(string Title, bool IsDone)
{
    public override string ToString()
    {
        return Title + (IsDone ? " (Done)" : "");
    }
}

public partial class TodoListViewModel : ViewModelBase
{
    public TodoListViewModel()
    {
        for (int i = 0; i < 100; i++)
        {
            Items.Add(new TodoItem($"Task {i + 1}", i % 3 == 0));
        }
    }


    [RelayCommand]
    private void AddItem()
    {
        if (!string.IsNullOrWhiteSpace(NewItemTitle))
        {
            Items.Add(new TodoItem(NewItemTitle, false));
            NewItemTitle = string.Empty;
        }
    }

    public ObservableCollection<TodoItem> Items { get; } =
    [
        new TodoItem("Buy groceries", false),
        new TodoItem("Walk the dog", true),
        new TodoItem("Finish project", false)
    ];

    [ObservableProperty]
    private string newItemTitle = string.Empty;
}