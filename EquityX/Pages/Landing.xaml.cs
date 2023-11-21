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

    public void Stock_Clicked(Object sender, EventArgs e)
    {
        Navigation.PushAsync(new EquityX.Pages.Stocks());
    }

    public void Crypto_Clicked(Object sender, EventArgs e)
    {
        Navigation.PushAsync(new EquityX.Pages.Crypto());
    }
    public void Wallet_Clicked(Object sender, EventArgs e)
    {
        Navigation.PushAsync(new EquityX.Pages.Wallet());
    }
    public void Dash_Clicked(Object sender, EventArgs e)
    {
        Navigation.PushAsync(new EquityX.Pages.Dash());
    }
    public void Account_Clicked(Object sender, EventArgs e)
    {
        Navigation.PushAsync(new EquityX.Pages.Account());
    }
}