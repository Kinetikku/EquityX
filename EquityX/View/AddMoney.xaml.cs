using EquityX.ViewModel;
using System.Runtime.CompilerServices;

namespace EquityX.Pages;

public partial class AddMoney : ContentPage
{
    private UserDataViewModel viewModel;
    public AddMoney()
	{
		InitializeComponent();

        viewModel = new UserDataViewModel();
		this.BindingContext = viewModel;

        ReadCurrencyAmount();
	}

	public async void ReadCurrencyAmount()
	{
		var bal = await viewModel.Balance();
		TotalMoney.Text = bal.ToString();
	}
}