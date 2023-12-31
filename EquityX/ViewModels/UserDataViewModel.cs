using EquityX.Model;
using EquityX.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.ComponentModel;

namespace EquityX.ViewModel
{
    public class UserDataViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Binding properties to the UI from Sign Up page
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string Mobile { get; set; }

        // Binding properties to the UI from the Login page
        public string EmailLogin { get; set; }
        public string PasswordLogin { get; set; }

        private double _currentBalance;
        public double CurrentBalance
        {
            get { return _currentBalance; }
            private set
            {
                if (_currentBalance != value)
                {
                    _currentBalance = value;
                    OnPropertyChanged(nameof(CurrentBalance));
                }
            }
        }

        public ObservableRangeCollection<UserData> User {  get; set; }
        public AsyncCommand AddUser { get; }
        public AsyncCommand GetUserBalance { get; }
        public AsyncCommand LoginUser { get; }

        public UserDataViewModel()
        {
            User = new ObservableRangeCollection<UserData>();
            AddUser = new AsyncCommand(Add);
            LoginUser = new AsyncCommand(Login);
            GetUserBalance = new AsyncCommand(Balance);
        }

        async Task Add()
        {
            var newUser = new UserData
            {
                FirstName = FirstName.Trim(),
                LastName = LastName.Trim(),
                Email = Email.Trim(),
                Password = Password,
                City = City.Trim(),
                Address1 = Address1.Trim(),
                Address2 = Address2.Trim(),
                County = County.Trim(),
                Country = Country.Trim(),
                Mobile = Mobile.Trim()
            };

            await UserService.AddUser(newUser.FirstName, newUser.LastName, newUser.Email, newUser.Password, newUser.Mobile, newUser.City, newUser.Address1, newUser.Address2, newUser.County, newUser.Country, 100.00);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }


        public async Task<bool> Login()
        {
            bool isValidUser = await UserService.ValidateLogin(EmailLogin, PasswordLogin);

            if (isValidUser)
            {
                await SecureStorage.SetAsync("CurrentUserEmail", EmailLogin.Trim());
                return true;
            }
            else
                return false;
        }

        public async Task UpdateBalance(double newBalance)
        {
            try
            {
                var email = await SecureStorage.GetAsync("CurrentUserEmail");
                await UserService.UpdateUserBalance(email, newBalance);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task WithdrawBalance(double withdrawAmount)
        {
            try
            {
                var email = await SecureStorage.GetAsync("CurrentUserEmail");
                await UserService.UpdateUserBalanceWithdraw(email, withdrawAmount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<double> Balance()
        {
            try
            {
                CurrentBalance = await UserService.GetUserBalance();
                return CurrentBalance;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
    }
}
