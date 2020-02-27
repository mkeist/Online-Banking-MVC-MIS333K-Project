using System;
using System.ComponentModel.DataAnnotations;

namespace Team_4_Project.Models.ViewModels
{
    public enum DateRange { Last15Days, Last30Days, Last60Days, AllDates, CustomRange }
    public enum AmountRange { UpTo100, OneTo200, TwoTo300, Over300, CustomRange, AllAmounts }

    public class TransactionSearchViewModel
    {
        public Int32 TransactionSearchViewModelID { get; set; }

        [Display(Name = "Search by description:")]
        public string SearchDescription { get; set; }

        [Display(Name = "Search Transaction Type:")]
        public TransacType SelectedTransactionType { get; set; }

        [Display(Name = "Search by amount:")]
        public AmountRange SearchAmountRange { get; set; }

        [Display(Name = "Amount Greater Than:")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal SearchAmountLower { get; set; }

        [Display(Name = "Amount Less Than:")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal SearchAmountUpper { get; set; }

        [Display(Name = "Search by Transaction Number:")]
        public string SearchTransactionNumber { get; set; }

        [Display(Name = "Search By Date:")]
        public DateRange SearchDateRange { get; set; }

        [Display(Name = "From:")]
        [DisplayFormat(DataFormatString = "{0: MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? SearchDateLower { get; set; }

        [Display(Name = "To:")]
        [DisplayFormat(DataFormatString = "{0: MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? SearchDateUpper { get; set; }

        public Account Account { get; set; }
    }
    
}
