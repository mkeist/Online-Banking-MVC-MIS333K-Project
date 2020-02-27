using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Team_4_Project.Models
{
    public enum TransacType { Deposit, Withdrawal, Transfer, Fee, Contribution, Distribution, SellingStock, BuyingStock, AllTransactions }
    public enum TransactionStatus { Approved, Pending}
    public enum Distribution { Qualified, Unqualified }
    public class Transaction
    {
        public Account Account { get; set; }

        public Payee Payee { get; set; }

        public List<Disputes> Disputes { get; set; }


        //TODO: come back and edit this
        [Display(Name = "Transaction Type:")]
        public TransacType TransactionType { get; set; }

        public Int32 TransactionID { get; set; }

        [Display(Name = "Transaction Number:")]
        public int TransactionNumber { get; set; }

        [Display(Name = "Transaction Status:")]
        public TransactionStatus Status { get; set; }

        [Display(Name = "Amount:")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal TransactionAmount { get; set; }

        [Display(Name = "Transaction Date:")]
        [Required(ErrorMessage = "Birthday is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime TransactionDate { get; set; }

        [Display(Name = "Description:")]
        public string Description { get; set; }

        [Display(Name = "Comments:")]
        public string Comments { get; set; }

        [Display(Name = "Distibution Type:")]
        public Distribution Distribution { get; set; }
        public Transaction()
        {
            if (Disputes == null)
            {
                Disputes = new List<Disputes>();
            }
        }
    }
}
