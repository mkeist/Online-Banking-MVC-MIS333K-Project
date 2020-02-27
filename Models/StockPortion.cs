using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Team_4_Project.Models
{
    public class StockPortion
    {
        public Int32 StockPortionID { get; set; }

        [Display(Name = "Number of Shares:")]
        public Int32 ShareNumber { get; set; }

        [Display(Name = "Stock Price:")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal CurrentPrice { get; set; }

        public Stock Stock { get; set; }

        public Account Account { get; set; }
    }
}
