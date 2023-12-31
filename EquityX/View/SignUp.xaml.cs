using EquityX.ViewModel;

namespace EquityX.Pages;

public partial class SignUp : ContentPage
{
    private UserDataViewModel viewModel;
	public SignUp()
	{
		InitializeComponent();

        viewModel = new UserDataViewModel();
        this.BindingContext = viewModel;
	}

    public async void GoToLogin(Object sender, EventArgs e)
    {
        await viewModel.AddUser.ExecuteAsync();
        await Navigation.PushAsync(new EquityX.Pages.Login());
    }

    public void GoBack(Object sender, EventArgs e)
    {
        Navigation.PushAsync(new EquityX.Pages.Login());
    }
}