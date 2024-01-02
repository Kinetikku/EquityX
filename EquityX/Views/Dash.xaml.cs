using EquityX.Models;
using EquityX.ViewModel;
using EquityX.ViewModels;

namespace EquityX.Pages;

public partial class Dash : ContentPage
{
    private string currentEmail;
    private List<StockData> stockDataList;
    private UserDataViewModel viewModel;
    private StockDataViewModel stockModel;
    private CryptoDataViewModel cryptoModel;
    private List<CryptoData> cryptoDataList;
    public Dash()
    {
        InitializeComponent();

        stockModel = new StockDataViewModel();
        cryptoModel = new CryptoDataViewModel();
        viewModel = new UserDataViewModel();

        LoadAssetsData().ConfigureAwait(false);
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

    private async Task LoadAssetsData()
    {
        await GetCurrentEmailAsync();
        stockDataList = await stockModel.LoadStocks(currentEmail);
        cryptoDataList = await cryptoModel.LoadCrypto(currentEmail);

        CryptoStat.Text = ("C " + cryptoDataList.Count.ToString());
        StockStat.Text = ("S " + stockDataList.Count.ToString());

        DisplayAssets();
    }

    private void DisplayAssets()
    {
        if (stockDataList != null && cryptoDataList != null)
        {
            var stackLayout = new VerticalStackLayout();

            foreach (var stockItem in stockDataList)
            {
                // Create a horizontal Stack
                var horizontalStackRow = new FlexLayout
                {
                    JustifyContent = Microsoft.Maui.Layouts.FlexJustify.SpaceBetween,
                    Margin = new Thickness(10, 10, 20, 10)
                };
                // Store the Logo and Name in one container
                var group1 = new HorizontalStackLayout { };
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
                    Aspect = Aspect.AspectFit
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
                    FontSize = 16,
                    MaximumWidthRequest = 100,
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
                horizontalStackRow.Children.Add(group2);
                stackLayout.Children.Add(horizontalStackRow);
            }

            foreach (var stockItem in cryptoDataList)
            {
                // Create a horizontal Stack
                var horizontalStackRow = new FlexLayout
                {
                    JustifyContent = Microsoft.Maui.Layouts.FlexJustify.SpaceBetween,
                    Margin = new Thickness(10, 10, 20, 10)
                };
                // Store the Logo and Name in one container
                var group1 = new HorizontalStackLayout { };
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
                    Aspect = Aspect.AspectFit
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
                    FontSize = 16,
                    MaximumWidthRequest = 190,
                };

                var companySharesLabel = new Label
                {
                    Text = $"{stockItem.Coins} Shares",
                    TextColor = Colors.White,
                    FontFamily = "RobotoRegular",
                    FontSize = 10
                };

                double total = stockItem.Coins * stockItem.CoinPrice;
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
                    Text = $"{Math.Round(stockItem.CoinPrice, 2).ToString("C")}",
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
                horizontalStackRow.Children.Add(group2);
                stackLayout.Children.Add(horizontalStackRow);
            }

            AssetsContainer.Content = stackLayout;
        }
    }
}