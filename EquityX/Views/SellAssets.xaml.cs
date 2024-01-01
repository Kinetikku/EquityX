using EquityX.Models;
using EquityX.ViewModel;
using EquityX.ViewModels;
using System.Reflection;
using System.Text.Json;

namespace EquityX.Pages;

public partial class SellAssets : ContentPage
{
    private string runningNumberInput = "";
    private string currentEmail;
    private int stockAmountSold;
    private double stockCost;

    private List<StockData> stockDataList;
    private StockData selectedStockData;
    private UserDataViewModel viewModel;
    private StockDataViewModel stockModel;

    public SellAssets()
	{
		InitializeComponent();
        LoadStockData().ConfigureAwait(false);

        stockModel = new StockDataViewModel();
        viewModel = new UserDataViewModel();
        this.BindingContext = viewModel;

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
        AddButton(grid, "1", 0, 0);
        AddButton(grid, "2", 0, 1);
        AddButton(grid, "3", 0, 2);
        AddButton(grid, "4", 1, 0);
        AddButton(grid, "5", 1, 1);
        AddButton(grid, "6", 1, 2);
        AddButton(grid, "7", 2, 0);
        AddButton(grid, "8", 2, 1);
        AddButton(grid, "9", 2, 2);
        AddButton(grid, "Clear", 3, 0);
        AddButton(grid, "0", 3, 1);
        AddButton(grid, "Del", 3, 2);

        // Add the Grid to the ContentPage placeholder section
        calculatorPlaceholder.Content = grid;
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

    private async Task LoadStockData()
    {
        await GetCurrentEmailAsync();
        stockDataList = await stockModel.LoadStocks(currentEmail);
        DisplayAssets();
    }


    private void DisplayAssets()
    {
        if (stockDataList != null)
        {
            // Create a vertical scroll for the rows
            var scrollLayout = new ScrollView();
            scrollLayout.Orientation = ScrollOrientation.Vertical;

            // Create a new StackLayout
            var stackLayout = new VerticalStackLayout();

            foreach (var stockItem in stockDataList)
            {
                // Create a horizontal Stack
                var horizontalStackRow = new FlexLayout
                { 
                    JustifyContent = Microsoft.Maui.Layouts.FlexJustify.SpaceBetween,
                    Margin = new Thickness(0, 10, 0, 10)
                };
                // Store the Logo and Name in one container
                var group1 = new HorizontalStackLayout{ };
                //Create a Circle with the logo inside
                var companyLogoContainer = new Frame
                {
                    HeightRequest = 50,
                    WidthRequest = 50,
                    CornerRadius = 25,
                    BackgroundColor = Colors.White,
                    Margin = new Thickness(5, 0, 5, 0)
                };

                var companyLogo = new Image
                {
                    Source = "",
                    WidthRequest = 40,
                    HeightRequest = 40,
                    Aspect= Aspect.AspectFit
                };

                companyLogoContainer.Content = companyLogo;
                group1.Children.Add(companyLogoContainer);
                //Create a Vertical stack with the company name, Total Shares and Total Value
                var verticalStackCompanyName = new VerticalStackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };

                var companyNameLabel = new Label
                {
                    Text = $"{stockItem.CompanyName}",
                    TextColor = Colors.White,
                    FontFamily = "RobotoBold",
                    FontSize = 16
                };

                var companySharesLabel = new Label
                {
                    Text = $"{stockItem.Shares} Shares",
                    TextColor = Colors.White,
                    FontFamily = "RobotoRegular",
                    FontSize = 10
                };

                double total = stockItem.Shares * stockItem.SharePrice;
                var companyTotalLabel = new Label
                {
                    Text = $"Total: {Math.Round(total, 2).ToString("C")}",
                    TextColor = Colors.White,
                    FontFamily = "RobotoRegular",
                    FontSize = 10
                };

                verticalStackCompanyName.Children.Add(companyNameLabel);
                verticalStackCompanyName.Children.Add(companySharesLabel);
                verticalStackCompanyName.Children.Add(companyTotalLabel);

                group1.Children.Add(verticalStackCompanyName);
                horizontalStackRow.Children.Add(group1);
                // Group 2 will store the sell button and the share info
                var group2 = new HorizontalStackLayout { };
                // Create another Vertical stack with the stock price per stock and the Gain percentage
                var verticalStackCompanyDetails = new VerticalStackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                };

                var companyStockPriceLabel = new Label
                {
                    Text = $"{Math.Round(stockItem.SharePrice, 2).ToString("C")}",
                    TextColor = Colors.White
                };

                var companyGainLabel = new Label
                {
                    Text = $"{stockItem.GainPercentage}%",
                    TextColor = Colors.White
                };

                verticalStackCompanyDetails.Children.Add(companyStockPriceLabel);
                verticalStackCompanyDetails.Children.Add(companyGainLabel);

                group2.Children.Add(verticalStackCompanyDetails);
                // Create a button to sell
                var sellButton = new Border
                {
                    HeightRequest = 20,
                    WidthRequest = 50,
                    BackgroundColor = Colors.White,
                };

                var sellLabel = new Label
                {
                    Text = "Sell",
                    TextColor = Colors.Black,
                    FontFamily = "RobotoRegular",
                    FontSize = 12,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };

                sellButton.BindingContext = stockItem;

                // Create the TapGestureRecognizer
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += SellButtonClicked;
                sellButton.GestureRecognizers.Add(tapGestureRecognizer);
                tapGestureRecognizer.CommandParameter = stockItem;

                sellButton.Content = sellLabel;
                group2.Children.Add(sellButton);
                horizontalStackRow.Children.Add(group2);
                stackLayout.Children.Add(horizontalStackRow);
            }
            scrollLayout.Content = stackLayout;
            // Set the StackLayout as the content of the ContentView
            SellSection.Content = scrollLayout;
        }
    }

    private void SellButtonClicked(object sender, EventArgs e)
    {
        if (sender is View view && view.BindingContext is StockData stockData)
        {
            selectedStockData = stockData;
            SellSlider.IsEnabled = true;
            // Set the maximum slider value to the total shares of the selected stock
            SellSlider.Maximum = stockData.Shares;

            // Recalculate the total value based on the stock's current price
            double totalValue = stockData.Shares * stockData.SharePrice;
            
            ShareSellAmount.Text = $"$0.00";
            runningNumberInput = "";
            SellSlider.Value = 0;

            stockCost = stockData.SharePrice;
            SellStockLogo.Text = stockData.CompanyName;
        }
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


    void OnSliderChange(object sender, ValueChangedEventArgs args)
	{
        double newValue = args.NewValue;
        double stockValue = stockCost;

        double stockQuantity = Math.Round(newValue, 0);

        double output = (stockQuantity *  stockValue);
        output = Math.Round(output, 2);

		ShareStockAmount.Text = stockQuantity.ToString() + " Shares";
        ShareSellAmount.Text = output.ToString("C");
	}

    private void ClearButtonClicked(object sender, EventArgs e)
    {
        SellSlider.Value = 0;
        runningNumberInput = "";
        ShareSellAmount.Text = "$0.00";
    }

    private void DelButtonClicked(object sender, EventArgs e)
    {
        if (SellSlider.Value >= 10)
        {
            int currentSliderValue = (int)SellSlider.Value;
            int newValue = currentSliderValue / 10; // Remove the last digit
            runningNumberInput = newValue.ToString();
            SellSlider.Value = newValue;
        }
        else
        {
            SellSlider.Value = 0;
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
            value = Math.Min(value, SellSlider.Maximum); // Ensure value does not exceed maximum
            value = Math.Max(value, SellSlider.Minimum); // Ensure value is not below minimum
            SellSlider.Value = value;
        }
    }

    private void AdjustSliderSettings()
    {
        try
        {
            SellSlider.Maximum = selectedStockData.Shares - stockAmountSold;

            // Deduct the sold shares from the total
            // This will be fixed more later on
            selectedStockData.Shares -= stockAmountSold;
            SellSlider.Value = 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private async void ConfirmButtonClicked(object sender, EventArgs e)
    {
        if (selectedStockData != null)
        {
            stockAmountSold = (int)SellSlider.Value;
            double saleAmount = stockAmountSold * selectedStockData.SharePrice;

            // Update user's balance after sale
            await viewModel.UpdateBalance(viewModel.CurrentBalance + saleAmount);
            // Need to edit the remove to be seen on the UI - Needs done
            await stockModel.RemoveStock(selectedStockData.Email, selectedStockData.LogoCode, stockAmountSold);

            AdjustSliderSettings();

            DisplayAssets();
        }
    }
}