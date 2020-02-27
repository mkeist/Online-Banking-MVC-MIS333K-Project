using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Team_4_Project.Models
{
    public enum StockType { Ordinary, ETF, IndexFund, MutualFund }
    public class Stock
    {
        public Int32 StockID { get; set; }

        [Display(Name = "Stock Name:")]
        public string StockName { get; set; }

        [Display(Name = "Ticker Symbol:")]
        public string TickerSymbol { get; set; }

        [Display(Name = "Stock Type:")]
        public StockType StockType { get; set; }

        [Display(Name = "Stock Price:")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal CurrentPrice { get; set; }

        [Display(Name = "Stock Fees:")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal StockFee { get; set; }

        public List<StockPortion> StockPortions { get; set; }

        public Stock()
        {
            
            if (StockPortions == null)
            {
                StockPortions = new List<StockPortion>();
            }
        }

    }
}
