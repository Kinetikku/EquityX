using EquityX.Model;
using EquityX.ViewModel;
using Microsoft.Maui.Controls;
using System.Reflection;
using System.Text.Json;

namespace EquityX.Pages;

public partial class SellAssets : ContentPage
{
    private string runningNumberInput = "";
    private double stockCost;
    private List<StockData> stockDataList;
    private StockData selectedStockData;
    private UserDataViewModel viewModel;

    public SellAssets()
	{
		InitializeComponent();
        LoadStockData().ConfigureAwait(false);
        DisplayAssets();
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

    private async Task LoadStockData()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "EquityX.Resources.data.json"; // Adjust the namespace

        using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        using (StreamReader reader = new StreamReader(stream))
        {
            string json = await reader.ReadToEndAsync();
            stockDataList = JsonSerializer.Deserialize<List<StockData>>(json);
        }
    }

    private void DisplayAssets()
    {
        if (stockDataList != null)
        {
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

                var companyTotalLabel = new Label
                {
                    Text = $"Total: ${stockItem.Shares * stockItem.SharePrice}",
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
                    Text = $"{stockItem.SharePrice}",
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
                var sellButton = new Button
                {
                    Text = "Sell",
                    HeightRequest = 20,
                    WidthRequest = 50,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontFamily = "RobotoBold",
                    FontSize = 14,
                    CommandParameter = stockItem
                };

                sellButton.Clicked += SellButtonClicked;

                // Add the label to the StackLayout
                group2.Children.Add(sellButton);
                horizontalStackRow.Children.Add(group2);
                stackLayout.Children.Add(horizontalStackRow);
            }

            // Set the StackLayout as the content of the ContentView
            SellSection.Content = stackLayout;
        }
    }

    private void SellButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is StockData stockData)
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
        // Get the current selected stocks value
        // Slider value needs to be multiplied by current stock price
        // Display multiplied value in the currency section
        double stockValue = stockCost;
		double value = Math.Round(args.NewValue, MidpointRounding.ToEven);

        runningNumberInput = value.ToString();
		ShareStockAmount.Text = value.ToString();
        ShareSellAmount.Text = "$" + (value * stockValue).ToString();
	}

    private void ClearButtonClicked(object sender, EventArgs e)
    {
        SellSlider.Value = 0;
        runningNumberInput = "";
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

    private async void ConfirmButtonClicked(object sender, EventArgs e)
    {
        if (selectedStockData != null)
        {
            int sharesToSell = (int)SellSlider.Value;
            double saleAmount = sharesToSell * selectedStockData.SharePrice;

            // Deduct the sold shares from the total
            // This will be fixed more later on
            selectedStockData.Shares -= sharesToSell;

            // Update user's balance after sale
            await viewModel.UpdateBalance(viewModel.CurrentBalance + saleAmount);

            // Now retrieve and use the updated balance
            double updatedBalance = await viewModel.Balance();

            if (updatedBalance >= 0)
            {
                // The balance was updated successfully
                // Refresh or update the UI as needed
                DisplayAssets();
            }
            else
            {
                // Handle the error case
            }
        }
    }

}