using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace EquityX.Model
{
    public class StockData
    {
        public string LogoCode { get; set; }
        public string CompanyName { get; set; }
        public int Shares { get; set; }
        public double SharePrice { get; set; }
        public double GainPercentage { get; set; }
    }

}
