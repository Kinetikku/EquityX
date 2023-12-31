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
		TotalMoney.Text = bal.ToString("C");
	}

    private void OnAmountTapped(object sender, EventArgs e)
    {
        var tappedAmount = (e as TappedEventArgs)?.Parameter;
        if (tappedAmount != null)
        {
            try
            {
                // Convert the current amount in DepositAmount.Text to a number
                if (double.TryParse(DepositAmount.Text.Substring(1), out double currentAmount))
                {
                    // Convert the tapped amount to a number
                    if (double.TryParse(tappedAmount.ToString(), out double tappedValue))
                    {
                        // Add the tapped amount to the current amount
                        double newAmount = currentAmount + tappedValue;
                        DepositAmount.Text = $"${newAmount}";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    private async void AddFunds(object sender, EventArgs e)
    {
        try
        {
            double.TryParse(DepositAmount.Text.Substring(1), out double currentAmount);

            await viewModel.UpdateBalance(currentAmount);

            DepositAmount.Text = "$0.00";

            var update = await viewModel.Balance();

            TotalMoney.Text = update.ToString("C");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private async void OnDepositAmountTapped(object sender, EventArgs e)
    {
        // Open a numeric input dialog or use a numeric entry to allow the user to enter a custom amount
        string result = await DisplayPromptAsync("Custom Deposit", "Enter deposit amount:", keyboard: Keyboard.Numeric);
        if (!string.IsNullOrWhiteSpace(result))
        {
            DepositAmount.Text = $"${result}";
        }
    }
}