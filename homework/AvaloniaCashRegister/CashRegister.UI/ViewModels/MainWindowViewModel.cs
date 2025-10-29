using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CashRegister.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

namespace CashRegister.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly IDbContextFactory<ApplicationDataContext> contextFactory;

    public ObservableCollection<Product> Products { get; set; } = new();

    public ObservableCollection<ReceiptLineViewModel> ReceiptLines { get; set; } = new();
    
    [ObservableProperty]
    private decimal _totalPrice = 0;

    public MainWindowViewModel(IDbContextFactory<ApplicationDataContext> contextFactory)
    {
        this.contextFactory = contextFactory;
        InitializeAsync();
    }

    [RelayCommand]
    public void AddProduct(Product productToAdd)
    {
        var lineOfProductToAdd = ReceiptLines.FirstOrDefault(x => x.ProductId == productToAdd.Id);
        if (lineOfProductToAdd is null)
        {
            ReceiptLines.Add(new ReceiptLineViewModel
            {
                ProductName= productToAdd.Name,
                ProductId = productToAdd.Id, 
                Amount = 1, 
                TotalPrice = productToAdd.Price
            });
        }
        else
        {
            lineOfProductToAdd.Amount++;
            lineOfProductToAdd.TotalPrice += productToAdd.Price;
        }

        CalculateTotalPrice();
    }

    private void CalculateTotalPrice()
    {
        TotalPrice = ReceiptLines.Sum(x => x.TotalPrice);
    }

    [RelayCommand]
    private void Checkout()
    {
        using var context = contextFactory.CreateDbContext();
        var receipt = new Receipt
        {
            ReceiptLines = ReceiptLines.Select(x => new ReceiptLine
            {
                ProductId = x.ProductId,
                Amount = x.Amount,
                TotalPrice = x.TotalPrice
            }).ToList(),
            TotalPrice = TotalPrice
        };
        context.Receipts.AddAsync(receipt);
        context.SaveChangesAsync();
        ReceiptLines.Clear();
        TotalPrice = 0;
    }

    private async void InitializeAsync()
    {
        await SeedDatabaseAsync();
        await LoadProductsAsync();
    }

    private async Task SeedDatabaseAsync()
    {
        using var context = await contextFactory.CreateDbContextAsync();
        if (await context.Products.AnyAsync())
        {
            return;
        }

        var products = new List<Product>
        {
            new() { Name = "Bananen 1kg", Price = 1.99m },
            new() { Name = "Äpfel 1kg", Price = 2.99m },
            new() { Name = "Trauben Weiß 500g", Price = 3.49m },
            new() { Name = "Himbeeren 125g", Price = 2.99m },
            new() { Name = "Karotten 500g", Price = 1.29m },
            new() { Name = "Eissalat 1 Stück", Price = 1.98m },
            new() { Name = "Zucchini 1 Stück", Price = 0.99m },
            new() { Name = "Knoblauch 150g", Price = 1.49m },
            new() { Name = "Joghurt 200g", Price = 0.89m },
            new() { Name = "Butter", Price = 2.49m }
        };

        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }

    private async Task LoadProductsAsync()
    {
        var context = await contextFactory.CreateDbContextAsync();
        var products = await context.Products.ToListAsync();

        Products.Clear();
        foreach (var product in products)
        {
            Products.Add(product);
        }
    }
}

public partial class ReceiptLineViewModel : ObservableObject
{
    [ObservableProperty]
    private int _productId;

    [ObservableProperty]
    private string _productName = string.Empty;

    [ObservableProperty]
    private int _amount;

    [ObservableProperty]
    private decimal _unitPrice;

    [ObservableProperty]
    private decimal _totalPrice;
}