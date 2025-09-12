using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace _2025_09_12_Avalonia.ViewModels;

public partial class CalculatorViewModel : ViewModelBase
{
    public double FirstNumber { get; set; } = 0;
    public double SecondNumber { get; set; } = 0;
    [ObservableProperty] private double result = 0;
    public string SelectedOperator { get; set; } = "+";
    public ObservableCollection<string> Operators { get; } = ["+", "-", "*", "/"];
    
    [RelayCommand]
    private void Calculate()
    {
        Result = SelectedOperator switch
        {
            "+" => FirstNumber + SecondNumber,
            "-" => FirstNumber - SecondNumber,
            "*" => FirstNumber * SecondNumber,
            "/" => SecondNumber != 0 ? FirstNumber / SecondNumber : double.NaN,
            _ => 0
        };
    }
}