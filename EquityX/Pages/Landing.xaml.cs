namespace EquityX.Pages;

public partial class Landing : ContentPage
{
	public Landing()
	{
		InitializeComponent();

		ListView listView = new ListView();
	}

	public void Add_Money_Clicked(Object sender, EventArgs e)
	{
		Navigation.PushAsync(new EquityX.Pages.AddMoney());

    }

    public void Sell_Assets_Clicked(Object sender, EventArgs e)
    {
        Navigation.PushAsync(new EquityX.Pages.SellAssets());

    }

    public void Withdraw_Clicked(Object sender, EventArgs e)
    {
        Navigation.PushAsync(new EquityX.Pages.Withdraw());

    }
}