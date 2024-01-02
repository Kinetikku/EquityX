using EquityX.Services;

namespace EquityX.ViewModels
{
    public class LandingViewModel : BindableObject
    {
        private double _totalAssetsValue;

        public double TotalAssetsValue
        {
            get => _totalAssetsValue;
            set
            {
                _totalAssetsValue = value;
                OnPropertyChanged(nameof(TotalAssetsValue));
            }
        }

        public async Task LoadTotalAssetsValue()
        {
            var email = await SecureStorage.GetAsync("CurrentUserEmail");
            var stocks = await UserService.GetStocks(email);
            var cryptos = await UserService.GetCryptos(email);

            double totalValue = 0;
            foreach (var stock in stocks)
            {
                totalValue += stock.Shares * stock.SharePrice;
            }

            foreach (var crypto in cryptos)
            {
                totalValue += crypto.Coins * crypto.CoinPrice;
            }

            TotalAssetsValue = totalValue;
        }
    }
}
