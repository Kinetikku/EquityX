using EquityX.Models;
using EquityX.ViewModel;
using EquityX.ViewModels;

namespace EquityX.Pages;

public partial class BuyAssets : ContentPage
{
    private string runningNumberInput = "";
    private string currentEmail;

    private int stockAmountBought;

    private double currentBalance;
    private double spendAmount;

    private UserDataViewModel viewModel;
    private StockDataViewModel stockModel;
    private CryptoDataViewModel cryptoModel;

    private StockData _stockData;
    private CryptoData _cryptoData;

    public BuyAssets(StockData stockData)
	{
		InitializeComponent();

        stockModel = new StockDataViewModel();
        viewModel = new UserDataViewModel();
        this.BindingContext = viewModel;

        _stockData = stockData;

        // Create a Grid
        var grid = new Grid
        {
            RowSpacing = 5,
            ColumnSpacing = 5
        };

        // Define rows
        for (int i = 0; i < 4; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        }

        // Define columns
        for (int i = 0; i < 3; i++)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 70 });
        }

        // Add buttons
        AddButton(grid, "7", 0, 0);
        AddButton(grid, "8", 0, 1);
        AddButton(grid, "9", 0, 2);
        AddButton(grid, "4", 1, 0);
        AddButton(grid, "5", 1, 1);
        AddButton(grid, "6", 1, 2);
        AddButton(grid, "1", 2, 0);
        AddButton(grid, "2", 2, 1);
        AddButton(grid, "3", 2, 2);
        AddButton(grid, "Clear", 3, 0);
        AddButton(grid, "0", 3, 1);
        AddButton(grid, "Del", 3, 2);

        // Add the Grid to the ContentPage placeholder section
        calculatorPlaceholder.Content = grid;
    }
    public BuyAssets(CryptoData cryptoData)
    {
        InitializeComponent();

        cryptoModel = new CryptoDataViewModel();
        viewModel = new UserDataViewModel();
        this.BindingContext = viewModel;

        _cryptoData = cryptoData;

        // Create a Grid
        var grid = new Grid
        {
            RowSpacing = 5,
            ColumnSpacing = 5
        };

        // Define rows
        for (int i = 0; i < 4; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        }

        // Define columns
        for (int i = 0; i < 3; i++)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 70 });
        }

        // Add buttons
        AddButton(grid, "7", 0, 0);
        AddButton(grid, "8", 0, 1);
        AddButton(grid, "9", 0, 2);
        AddButton(grid, "4", 1, 0);
        AddButton(grid, "5", 1, 1);
        AddButton(grid, "6", 1, 2);
        AddButton(grid, "1", 2, 0);
        AddButton(grid, "2", 2, 1);
        AddButton(grid, "3", 2, 2);
        AddButton(grid, "Clear", 3, 0);
        AddButton(grid, "0", 3, 1);
        AddButton(grid, "Del", 3, 2);

        // Add the Grid to the ContentPage placeholder section
        calculatorPlaceholder.Content = grid;
    }
    private void AddButton(Grid grid, string text, int row, int column)
    {
        var button = new Button
        {
            Text = text,
            WidthRequest = 70,
            HeightRequest = 40,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            BackgroundColor = Microsoft.Maui.Graphics.Color.FromArgb("#3A5066"),
            TextColor = Colors.White
        };

        // Need to check to see if buttons contain specific text values
        if (text == "Clear")
        {
            button.Clicked += ClearButtonClicked;
        }
        else if (text == "Del")
        {
            button.Clicked += DelButtonClicked;
        }
        else
        {
            button.Clicked += NumberButtonClicked;
        }

        Grid.SetRow(button, row);
        Grid.SetColumn(button, column);

        grid.Children.Add(button);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        currentBalance = await viewModel.Balance();
        BuySlider.Maximum = currentBalance;
        await GetCurrentEmailAsync();
    }

    private async Task GetCurrentEmailAsync()
    {
        try
        {
            string email = await SecureStorage.GetAsync("CurrentUserEmail");
            currentEmail = email;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving email from secure storage: {ex.Message}");
        }
    }

    void OnSliderChange(object sender, ValueChangedEventArgs args)
    {
        double assetValue = (_stockData != null) ? _stockData.SharePrice : _cryptoData.CoinPrice;
        spendAmount = Math.Round(args.NewValue, 2);

        double availableAssets = (spendAmount / assetValue);
        stockAmountBought = (int)Math.Round(availableAssets, 0);

        string assetType = (_stockData != null) ? "Shares" : "Coins";
        ShareStockAmount.Text = $"{stockAmountBought} {assetType}";
        PriceAmount.Text = spendAmount.ToString("C");
    }

    private void ClearButtonClicked(object sender, EventArgs e)
    {
        BuySlider.Value = 0;
        runningNumberInput = "";
        PriceAmount.Text = "$0.00";
    }

    private void DelButtonClicked(object sender, EventArgs e)
    {
        if (BuySlider.Value >= 10)
        {
            int currentSliderValue = (int)BuySlider.Value;
            int newValue = currentSliderValue / 10; // Remove the last digit
            runningNumberInput = newValue.ToString();
            BuySlider.Value = newValue;
        }
        else
        {
            BuySlider.Value = 0;
        }
    }

    private void NumberButtonClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button != null)
        {
            runningNumberInput += button.Text; // Append the button's number to the current input
            UpdateSliderValue();
        }
    }

    private void UpdateSliderValue()
    {
        if (double.TryParse(runningNumberInput, out double value))
        {
            value = Math.Min(value, BuySlider.Maximum); // Ensure value does not exceed maximum
            value = Math.Max(value, BuySlider.Minimum); // Ensure value is not below minimum
            BuySlider.Value = value;
        }
    }

    private void AdjustSliderSettings()
    {
        try
        {
            BuySlider.Maximum = currentBalance;

            // Work out how much the buy was and minus that from current balance
            
            BuySlider.Value = 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private async void ConfirmButtonClicked(object sender, EventArgs e)
    {
        if (_stockData != null)
            await stockModel.AddStock(_stockData.LogoCode, _stockData.CompanyName, stockAmountBought, _stockData.SharePrice, _stockData.GainPercentage, currentEmail);
        else if (_cryptoData != null)
            await cryptoModel.AddCrypto(_cryptoData.LogoCode, _cryptoData.CompanyName, stockAmountBought, _cryptoData.CoinPrice, _cryptoData.GainPercentage, currentEmail);

        await viewModel.WithdrawBalance(spendAmount);
        AdjustSliderSettings();
    }
}