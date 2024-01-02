using EquityX.Model;
using EquityX.ViewModel;
using EquityX.Views;

namespace EquityX.Pages;

public partial class Account : ContentPage
{
    private UserDataViewModel viewModel;
    private UserData userData;

    private Label nameValue;
    private Label emailValue;
    private Label mobileValue;
    private Label passwordValue;
    private Label townValue;
    private Label address1Value;
    private Label address2Value;
    private Label countyValue;
    private Label countryValue;

    public Account()
    {
        InitializeComponent();

        viewModel = new UserDataViewModel();

        SetupUI();
    }

    private async Task<UserData> getUserInfo()
    {
        userData = await viewModel.GetUser();
        return userData;
    }

    private async void SetupUI()
    {
        userData = await getUserInfo();

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
            Source = "account_profile.png",
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

        nameValue = new Label { Text = userData.FirstName + " " + userData.LastName };
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

        emailValue = new Label { Text = userData.Email };
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

        mobileValue = new Label { Text = userData.Mobile };
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

        townValue = new Label { Text = userData.City };
        townValue.FontFamily = "RobotoRegular";
        Grid.SetColumn(townValue, 1);
        Grid.SetRow(townValue, 3);
        accountDetailsGrid.Children.Add(townValue);

        // Row 4 - Address Line 1
        var address1Label = new Label { Text = "Address Line 1:" };
        address1Label.FontFamily = "RobotoBold";
        Grid.SetColumn(address1Label, 0);
        Grid.SetRow(address1Label, 4);
        accountDetailsGrid.Children.Add(address1Label);

        address1Value = new Label { Text = userData.Address1 };
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

        address2Value = new Label { Text = userData.Address2 ?? "" };
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

        countyValue = new Label { Text = userData.County };
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

        countryValue = new Label { Text = userData.Country };
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

        var tapGestureRecognizer = new TapGestureRecognizer();
        tapGestureRecognizer.Tapped += EditButton_Tapped;
        editButton.GestureRecognizers.Add(tapGestureRecognizer);

        circleFrame.Content = circleImage;
        MainLayout.Children.Add(accountDetailsFrame);
        MainLayout.Children.Add(editButton);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        UpdateUIWithUserData();
    }

    private void UpdateUIWithUserData()
    {
        if (userData != null)
        {
            nameValue.Text = $"{userData.FirstName} {userData.LastName}";
            emailValue.Text = userData.Email;
            mobileValue.Text = userData.Mobile;
            townValue.Text = userData.City;
            address1Value.Text = userData.Address1;
            address2Value.Text = userData.Address2 ?? "";
            countyValue.Text = userData.County;
            countryValue.Text = userData.Country;
        }
    }

    private async void EditButton_Tapped(object sender, EventArgs e)
    {
        if (userData != null)
            await Navigation.PushAsync(new EditUser(userData));
        else
            await DisplayAlert("Error", "User data not available for editing.", "OK");
    }

}
