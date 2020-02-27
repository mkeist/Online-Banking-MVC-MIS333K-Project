using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Team_4_Project.Models
{
    public enum AccountType { Checking, Savings, IRA, Stock }
    public enum Status { Active, Inactive}
    public enum Balanced { Balanced, Unbalanced }

    //make this class abstract so you have a different table for stock, ira, checkingsavings?
    public class Account
    {
        public Int32 AccountID { get; set; }

        [Display(Name = "Account Number:")]
        public Int32 AccountNumber { get; set; }

        [Display(Name = "Account Number:")]
        public String PartialAccountNumber { get; set; }

        //TODO: Establish the default name in the controllers
        [Display(Name = "Account Name:")]
        public string AccountName { get; set; }

        [Display(Name = "Status:")]
        public Status Status { get; set; }

        [Display(Name = "Account Value:")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required(ErrorMessage = "Balance is required.")]
        public Decimal Value { get; set; }

        [Display(Name = "Account Type:")]
        public AccountType accountType { get; set; }

        public AppUser User { get; set; }       

        public List<Transaction> Transactions { get; set; }

        public List<ViewModels.TransactionSearchViewModel> TransactionSearchViewModels { get; set; }

        //properties for Stock Portfolio
        [Display(Name = "Gains:")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal Gains { get; set; }

        [Display(Name = "Cash Value Portion:")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal CashValPortion { get; set; }

        [Display(Name = "Available Cash:")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal AvailableCash { get; set; }

        [Display(Name = "Fees:")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal CashValueFees { get; set; }

        [Display(Name = "Bonuses:")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal Bonuses { get; set; }

        [Display(Name = "Balanced?")]
        public Balanced Balanced { get; set; }

        //navigational property for Stock Portfolio
        public List<StockPortion> StockPortions { get; set; }

        public Account()
        {
            if (Transactions == null)
            {
                Transactions = new List<Transaction>();
            }
            if (StockPortions == null)
            {
                StockPortions = new List<StockPortion>();
            }

        }
    }

 }
