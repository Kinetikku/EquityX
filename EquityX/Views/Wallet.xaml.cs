using EquityX.ViewModel;

namespace EquityX.Pages;

public partial class Wallet : ContentPage
{
    private UserDataViewModel viewModel;
    public Wallet()
	{
		InitializeComponent();

        viewModel = new UserDataViewModel();
        this.BindingContext = viewModel;

        ReadCurrencyAmount();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var amount = await viewModel.Balance();
        TotalMoney.Text = amount.ToString("C");
    }

    public async void ReadCurrencyAmount()
    {
        var bal = await viewModel.Balance();
        TotalMoney.Text = bal.ToString();
    }

    public void GoToDeposit(Object sender, EventArgs e)
    {
        Navigation.PushAsync(new EquityX.Pages.AddMoney());
    }
    public void GoToWithdraw(Object sender, EventArgs e)
    {
        Navigation.PushAsync(new EquityX.Pages.Withdraw());
    }
}