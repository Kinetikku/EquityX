using EquityX.Models;
using EquityX.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.ComponentModel;

namespace EquityX.ViewModels
{
    public class CryptoDataViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableRangeCollection<CryptoData> Crypto { get; set; }
        public AsyncCommand BuyStock { get; }
        public AsyncCommand SellStock { get; }
        public AsyncCommand GetStock { get; }

        public CryptoDataViewModel()
        {
            Crypto = new ObservableRangeCollection<CryptoData>();
        }

        public async Task AddCrypto(string logoCode, string companyName, int coins, double coinPrice, double gainPercentage, string email)
        {
            var newCrypto = new CryptoData
            {
                LogoCode = logoCode,
                CompanyName = companyName,
                Coins = coins,
                CoinPrice = coinPrice,
                GainPercentage = gainPercentage,
                Email = email,
            };

            await UserService.BuyCrypto(newCrypto.LogoCode, newCrypto.CompanyName, newCrypto.Coins, newCrypto.CoinPrice, newCrypto.GainPercentage, newCrypto.Email);
        }

        public async Task RemoveCrypto(string email, string logoCode, int amount)
        {
            await UserService.SellCrypto(logoCode, amount, email);
        }

        public async Task<List<CryptoData>> LoadCrypto(string email)
        {
            var cryptos = await UserService.GetCryptos(email);
            Crypto.Clear();
            Crypto.AddRange(cryptos);
            return new List<CryptoData>(Crypto);
        }
    }
}
