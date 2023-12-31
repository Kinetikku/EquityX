using SQLite;

namespace EquityX.Models
{
    [Table("StockData")]
    public class StockData
    {
        [PrimaryKey]
        public string LogoCode { get; set; }
        public string CompanyName { get; set; }
        public int Shares { get; set; }
        public double SharePrice { get; set; }
        public double GainPercentage { get; set; }
        public string Email { get; set; }
    }
}
