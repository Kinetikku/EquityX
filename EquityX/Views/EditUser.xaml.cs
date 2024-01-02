using EquityX.Model;
using EquityX.ViewModel;

namespace EquityX.Views;

public partial class EditUser : ContentPage
{
    private UserData userData;
    private UserDataViewModel viewModel;
    public EditUser(UserData userData)
	{
		InitializeComponent();
        viewModel = new UserDataViewModel();
        this.userData = userData;
        BindingContext = userData;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        userData.FirstName = nameEntry.Text;
        userData.LastName = nameEntry.Text;
        userData.Mobile = mobileEntry.Text;
        userData.City = cityEntry.Text;
        userData.Address1 = address1Entry.Text;
        userData.Address2 = address2Entry.Text;
        userData.County = countyEntry.Text;
        userData.Country = countryEntry.Text;

        // update the user data in the database
        await viewModel.UpdateUser(userData);

        // Navigate back to the Account page
        await Navigation.PopAsync();
    }
}