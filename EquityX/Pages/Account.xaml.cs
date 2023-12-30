namespace EquityX.Pages;

public partial class Account : ContentPage
{
    public Account()
    {
        InitializeComponent();

        // Create a frame with rounded corners
        Frame circleFrame = new Frame
        {
            HeightRequest = 150,
            WidthRequest = 150,
            CornerRadius = 75,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Padding = 0,
            Margin = 30,
        };

        // Create an image
        Image circleImage = new Image
        {
            Source = "account_profile.png", // Replace with your image path
            Aspect = Aspect.AspectFill,
            HeightRequest = 150,
            WidthRequest = 150,
        };

        // Add the frame to the VerticalStackLayout
        MainLayout.Children.Add(circleFrame);

        // Create a border with rounded corners for the account details
        Border accountDetailsFrame = new Border
        {
            Padding = 10,
            Margin = new Thickness(20),
        };

        // Create a grid for the labels and values
        Grid accountDetailsGrid = new Grid
        {
            RowDefinitions =
        {
            new RowDefinition { Height = GridLength.Auto },
            new RowDefinition { Height = GridLength.Auto },
            new RowDefinition { Height = GridLength.Auto },
            new RowDefinition { Height = GridLength.Auto },
            new RowDefinition { Height = GridLength.Auto },
            new RowDefinition { Height = GridLength.Auto },
            new RowDefinition { Height = GridLength.Auto },
            new RowDefinition { Height = GridLength.Auto },
        },
            ColumnDefinitions =
        {
            new ColumnDefinition { Width = GridLength.Auto },
            new ColumnDefinition { Width = GridLength.Star },
        }
        };

        // Row 0 - Name
        var nameLabel = new Label { Text = "Name:" };
        nameLabel.FontFamily = "RobotoBold";
        Grid.SetColumn(nameLabel, 0);
        Grid.SetRow(nameLabel, 0);
        accountDetailsGrid.Children.Add(nameLabel);

        var nameValue = new Label { Text = "Gareth Craig" };
        nameValue.FontFamily = "RobotoRegular";
        Grid.SetColumn(nameValue, 1);
        Grid.SetRow(nameValue, 0);
        accountDetailsGrid.Children.Add(nameValue);

        // Row 1 - Email
        var emailLabel = new Label { Text = "Email:" };
        emailLabel.FontFamily = "RobotoBold";
        Grid.SetColumn(emailLabel, 0);
        Grid.SetRow(emailLabel, 1);
        accountDetailsGrid.Children.Add(emailLabel);

        var emailValue = new Label { Text = "garethcraig@atu.ie" };
        emailValue.FontFamily = "RobotoRegular";
        Grid.SetColumn(emailValue, 1);
        Grid.SetRow(emailValue, 1);
        accountDetailsGrid.Children.Add(emailValue);

        // Row 2 - Mobile
        var mobileLabel = new Label { Text = "Mobile:" };
        mobileLabel.FontFamily = "RobotoBold";
        Grid.SetColumn(mobileLabel, 0);
        Grid.SetRow(mobileLabel, 2);
        accountDetailsGrid.Children.Add(mobileLabel);

        var mobileValue = new Label { Text = "0871234567" };
        mobileValue.FontFamily = "RobotoRegular";
        Grid.SetColumn(mobileValue, 1);
        Grid.SetRow(mobileValue, 2);
        accountDetailsGrid.Children.Add(mobileValue);

        // Row 3 - Town/City
        var townLabel = new Label { Text = "Town/City:" };
        townLabel.FontFamily = "RobotoBold";
        Grid.SetColumn(townLabel, 0);
        Grid.SetRow(townLabel, 3);
        accountDetailsGrid.Children.Add(townLabel);

        var townValue = new Label { Text = "Raphoe" };
        townValue.FontFamily = "RobotoRegular";
        Grid.SetColumn(townValue, 1);
        Grid.SetRow(townValue, 3);
        accountDetailsGrid.Children.Add(townValue);

        // Row 4 - Address Line 1
        var address1Label = new Label { Text = "Address Line 1:" };
        Grid.SetColumn(address1Label, 0);
        Grid.SetRow(address1Label, 4);
        accountDetailsGrid.Children.Add(address1Label);

        var address1Value = new Label { Text = "Common" };
        address1Value.FontFamily = "RobotoRegular";
        Grid.SetColumn(address1Value, 1);
        Grid.SetRow(address1Value, 4);
        accountDetailsGrid.Children.Add(address1Value);

        // Row 5 - Address Line 2
        var address2Label = new Label { Text = "Address Line 2:" };
        address2Label.FontFamily = "RobotoBold";
        Grid.SetColumn(address2Label, 0);
        Grid.SetRow(address2Label, 5);
        accountDetailsGrid.Children.Add(address2Label);

        var address2Value = new Label { Text = "" };
        address2Value.FontFamily = "RobotoRegular";
        Grid.SetColumn(address2Value, 1);
        Grid.SetRow(address2Value, 5);
        accountDetailsGrid.Children.Add(address2Value);

        // Row 6 - County
        var countyLabel = new Label { Text = "County:" };
        countyLabel.FontFamily = "RobotoBold";
        Grid.SetColumn(countyLabel, 0);
        Grid.SetRow(countyLabel, 6);
        accountDetailsGrid.Children.Add(countyLabel);

        var countyValue = new Label { Text = "Donegal" };
        countyValue.FontFamily = "RobotoRegular";

        Grid.SetColumn(countyValue, 1);
        Grid.SetRow(countyValue, 6);
        accountDetailsGrid.Children.Add(countyValue);

        // Row 7 - Country
        var countryLabel = new Label { Text = "Country:" };
        countryLabel.FontFamily = "RobotoBold";
        Grid.SetColumn(countryLabel, 0);
        Grid.SetRow(countryLabel, 7);
        accountDetailsGrid.Children.Add(countryLabel);

        var countryValue = new Label { Text = "Ireland" };
        countryValue.FontFamily = "RobotoRegular";
        Grid.SetColumn(countryValue, 1);
        Grid.SetRow(countryValue, 7);
        accountDetailsGrid.Children.Add(countryValue);

        // Add the grid to the frame
        accountDetailsFrame.Content = accountDetailsGrid;

        // Create Edit Label
        var label = new Label
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontFamily = "RobotoBold",
            FontSize = 18,
            Text = "Edit"
        };

        // Create Edit Border
        var editButton = new Border
        {
            HeightRequest = 40,
            WidthRequest = 80,
            Content = label
        };

        circleFrame.Content = circleImage;
        MainLayout.Children.Add(accountDetailsFrame);
        MainLayout.Children.Add(editButton);
    }
}
