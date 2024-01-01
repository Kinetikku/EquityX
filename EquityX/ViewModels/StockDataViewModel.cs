using EquityX.Models;
using EquityX.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.ComponentModel;

namespace EquityX.ViewModels
{
    public class StockDataViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableRangeCollection<StockData> Stock {  get; set; }
        public AsyncCommand BuyStock { get; }
        public AsyncCommand SellStock { get; }
        public AsyncCommand GetStock { get; }

        public StockDataViewModel()
        {
            Stock = new ObservableRangeCollection<StockData>();
        }

        public async Task AddStock(string logoCode, string companyName, int shares, double sharePrice, double gainPercentage, string email)
        {
            var newStock = new StockData
            {
                LogoCode = logoCode,
                CompanyName = companyName,
                Shares = shares,
                SharePrice = sharePrice,
                GainPercentage = gainPercentage,
                Email = email,
            };

            await UserService.BuyStock(newStock.LogoCode, newStock.CompanyName, newStock.Shares, newStock.SharePrice, newStock.GainPercentage, newStock.Email);
        }

        public async Task RemoveStock(string email, string logoCode, int amount)
        {
                await UserService.SellStock(logoCode, amount, email);
        }

        public async Task<List<StockData>> LoadStocks(string email)
        {
            var stocks = await UserService.GetStocks(email);
            Stock.Clear();
            Stock.AddRange(stocks);
            return new List<StockData>(Stock);
        }

    }
}
