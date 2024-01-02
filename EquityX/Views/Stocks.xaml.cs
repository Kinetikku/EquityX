using EquityX.Models;
using EquityX.Services;
using System.Text.Json;

namespace EquityX.Pages;

public partial class Stocks : ContentPage
{
    private List<StockData> stockDataList;
    private IDispatcherTimer _timer;

    public Stocks()
	{
		InitializeComponent();

        LoadStockData().ConfigureAwait(true);

        _timer = Application.Current.Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromSeconds(60);
        _timer.Tick += (sender, e) => LoadStockData().ConfigureAwait(true);
        _timer.Start();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _timer?.Stop();
    }

    private async Task LoadStockData()
    {
        WebDataManager apiData = new WebDataManager();
        apiData.GetQuote();

        string filePath = Path.Combine(FileSystem.AppDataDirectory, "data.json");

        if (File.Exists(filePath))
        {
            string json = await File.ReadAllTextAsync(filePath);

            // Deserialize JSON to a list of StockData
            var allItems = JsonSerializer.Deserialize<List<StockData>>(json);

            // Filter to only include items where Type is "Stock"
            stockDataList = allItems?.Where(item => item.Type == "Stock").ToList();
        }
        else
        {
            Console.WriteLine("No data can be obtained");  
        }

        DisplayAssets();
    }


    private void DisplayAssets()
    {
        if (stockDataList != null)
        {
            // Create a vertical scroll for the rows
            var scrollLayout = new ScrollView();
            scrollLayout.Orientation = ScrollOrientation.Vertical;
            scrollLayout.HeightRequest = 650;

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
                var group1 = new HorizontalStackLayout { };
                group1.Margin = new Thickness(10, 0, 0, 0);
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
                    Source = stockItem.Image??"",
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
                    MaximumWidthRequest = 120,
                };

                verticalStackCompanyName.Children.Add(companyNameLabel);

                group1.Children.Add(verticalStackCompanyName);
                horizontalStackRow.Children.Add(group1);

                // Group 2 will store the view and buy button and the share price
                var group2 = new HorizontalStackLayout { };
                group2.HorizontalOptions = LayoutOptions.Center;
                group2.Margin = new Thickness(0, -10, 10, 0);

                var companyStockPriceLabel = new Label
                {
                    Text = $"{Math.Round(stockItem.SharePrice, 2).ToString("C")}",
                    TextColor = Colors.White,
                    FontFamily = "RobotoRegular",
                    FontSize = 16,
                    Margin = new Thickness(0, 33, 10 , 0),
                };

                group2.Children.Add(companyStockPriceLabel);

                // Create a button to view
                var viewButton = new Border
                {
                    HeightRequest = 30,
                    WidthRequest = 50,
                    BackgroundColor = Colors.White,
                };

                var viewLabel = new Label
                {
                    Text = "View",
                    TextColor = Colors.Black,
                    FontFamily = "RobotoRegular",
                    FontSize = 14,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };

                viewButton.BindingContext = stockItem;

                // Create the TapGestureRecognizer
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += ViewButtonClicked;
                viewButton.GestureRecognizers.Add(tapGestureRecognizer);
                tapGestureRecognizer.CommandParameter = stockItem;

                viewButton.Content = viewLabel;
                group2.Children.Add(viewButton);

                // Create a button to buy
                var buyButton = new Border
                {
                    HeightRequest = 30,
                    WidthRequest = 50,
                    BackgroundColor = Colors.White,
                };

                var buyLabel = new Label
                {
                    Text = "Buy",
                    TextColor = Colors.Black,
                    FontFamily = "RobotoRegular",
                    FontSize = 14,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };

                buyButton.BindingContext = stockItem;

                // Create the TapGestureRecognizer
                var tapGestureRecognizer1 = new TapGestureRecognizer();
                tapGestureRecognizer1.Tapped += BuyButtonClicked;
                buyButton.GestureRecognizers.Add(tapGestureRecognizer1);
                tapGestureRecognizer1.CommandParameter = stockItem;

                buyButton.Content = buyLabel;
                group2.Children.Add(buyButton);
                horizontalStackRow.Children.Add(group2);
                stackLayout.Children.Add(horizontalStackRow);
            }
            scrollLayout.Content = stackLayout;
            // Set the StackLayout as the content of the ContentView
            StockOptionsContainer.Content = scrollLayout;
        }
    }

    private void ViewButtonClicked(object sender, EventArgs e)
    {
        // Will update the Live View with data after being clicked
    }

    private void BuyButtonClicked(object sender, EventArgs e)
    {
        if (sender is View view && view.BindingContext is StockData stockData)
        {
            var buyAssetsPage = new EquityX.Pages.BuyAssets(stockData);
            Navigation.PushAsync(buyAssetsPage);
        }
    }
}