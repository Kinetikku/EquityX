using EquityX.Model;
using SQLite;

namespace EquityX.Services
{
    public static class UserService
    {
        static SQLiteAsyncConnection db;
        static async Task Init()
        {
            // Checks if the database already exists
            if (db != null)
                return;
            
            // Get path to the database
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "UserData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<UserData>();
        }

        // Adds a user to the database
        public static async Task AddUser(string firstName, string lastName, string email, string password, string mobile, string city, string address1, string address2, string county, string country, double balance)
        {
            await Init();

            if (await CheckUserExists(email))
                throw new Exception("User with that email already exists.");

            var user = new UserData
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                Mobile = mobile,
                City = city,
                Address1 = address1,
                Address2 = address2,
                County = county,
                Country = country,
                Balance = balance
            };

            await db.InsertAsync(user);
        }

        // Checks if user is already on the database
        public static async Task<bool> CheckUserExists(string email)
        {
            await Init();

            var user = await db.Table<UserData>().Where(u => u.Email == email).FirstOrDefaultAsync();

            return user != null;
        }

        // Gets the users balance
        public static async Task<double> GetUserBalance()
        {
            await Init();
            var email = await SecureStorage.GetAsync("CurrentUserEmail");

            // Gets the current users data with that specific email
            var user = await db.Table<UserData>().Where(u => u.Email == email).FirstOrDefaultAsync();

            // Check if the user exists
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // Return the users balance
            return user.Balance;
        }

        // Update the users balance
        public static async Task UpdateUserBalance(string email, double newBalance)
        {
            await Init();

            // Retrieve the user from the database
            var user = await db.Table<UserData>().Where(u => u.Email == email).FirstOrDefaultAsync();

            // Check if the user exists
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // Update the user's balance
            user.Balance += newBalance;

            // Save the updated user back to the database
            await db.UpdateAsync(user);
        }

        public static async Task<bool> ValidateLogin(string email, string password)
        {
            await Init();

            // it'll throw an error if the trim method is called on a null value
            if (email != null)
                email = email.Trim();

            var user = await db.Table<UserData>().Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();

            if (user == null)
                return false;
            else
                return true;
        }
    }
}
