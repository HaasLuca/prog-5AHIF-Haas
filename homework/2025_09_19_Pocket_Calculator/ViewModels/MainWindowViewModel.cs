using System;
using System.Data;
using System.Threading.Tasks;
using _2025_09_19_Pocket_Calculator.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace _2025_09_19_Pocket_Calculator.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] 
    private string result = "0";
    
    [RelayCommand]
    private void AddToDisplay(string content)
    {
        if (Result == "0" && "0123456789-+".Contains(content))
            Result = content;
        else
            Result += content;
    }
    
    [RelayCommand]
    private void Clear() => Result = "0";

    [RelayCommand]
    private async void Calculate()
    {
        try
        {
            Result = CalculatorLogic.CalculateFromString(Result).ToString();
        }
        catch (DivideByZeroException)
        {
            var box = MessageBoxManager.GetMessageBoxStandard("Error",
                "Division by 0 is not allowed!",
                ButtonEnum.Ok);
            Result = "0";

            // ShowAsync displays the message box, choosing the presentation style—popup or window—according to the application type:
            // - SingleViewApplicationLifetime (used in mobile or browser environments): shows as a popup
            // - ClassicDesktopStyleApplicationLifetime (desktop apps): shows as a window
            await box.ShowAsync();
        }
        catch (SyntaxErrorException)
        {
            var box = MessageBoxManager.GetMessageBoxStandard("Error",
                "Syntax error in the expression!",
                ButtonEnum.Ok);
            Result = "0";

            // ShowAsync displays the message box, choosing the presentation style—popup or window—according to the application type:
            // - SingleViewApplicationLifetime (used in mobile or browser environments): shows as a popup
            // - ClassicDesktopStyleApplicationLifetime (desktop apps): shows as a window
            await box.ShowAsync();
        }
    }
}
