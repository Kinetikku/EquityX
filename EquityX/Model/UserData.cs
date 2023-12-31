using SQLite;

namespace EquityX.Model
{
    public class UserData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [PrimaryKey]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Mobile {  get; set; }
        public string City { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; } = string.Empty;
        public string County { get; set; }
        public string Country { get; set; }
        public double Balance { get; set; }
    }
}
