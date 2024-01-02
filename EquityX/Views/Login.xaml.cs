using EquityX.Services;
using EquityX.ViewModel;

namespace EquityX.Pages;

public partial class Login : ContentPage
{
    private UserDataViewModel viewModel;
    public Login()
    {
        InitializeComponent();

        // Get API Data
        WebDataManager apiData = new WebDataManager();

        apiData.GetQuote();

        viewModel = new UserDataViewModel();
        this.BindingContext = viewModel;
    }

//    private void Entry_Completed(object sender, EventArgs e)
//    {
//#if ANDROID
//    if(Platform.CurrentActivity.CurrentFocus != null)
//        Platform.CurrentActivity.HideKeyboard(Platoform.CurrentActivity.CurrentFocus);
//#endif
//    }

    public async void GoToMain(Object sender, EventArgs e)
    {
        bool isValidUser = await viewModel.Login();
        if (isValidUser)
        {
            await Navigation.PushAsync(new EquityX.Pages.Landing());
            Error.Text = string.Empty;
        }
        else
        {
            Error.Text = "No user found.";
            Error.TextColor = Colors.Red;
        }
            
    }

    public void GoToSignUp(Object sender, EventArgs e)
    {
        Navigation.PushAsync(new EquityX.Pages.SignUp());
    }
}