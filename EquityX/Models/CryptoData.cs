using SQLite;

namespace EquityX.Models
{
    [Table("CryptoData")]
    public class CryptoData
    {
        [PrimaryKey]
        public string LogoCode { get; set; }
        public string CompanyName { get; set; }
        public int Coins { get; set; }
        public double CoinPrice { get; set; }
        public double GainPercentage { get; set; }
        public string Email { get; set; }
    }
}
