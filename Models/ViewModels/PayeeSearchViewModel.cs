using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Team_4_Project.Models.ViewModels
{
    public class PayeeSearchViewModel
    {

        [Display(Name = "From Account:")]
        public int FromAccountID { get; set; }

        [Display(Name = "Payment Amount:")]
        [Required(ErrorMessage = "Amount is required")]
        public decimal PaymentAmount { get; set; }

        [Display(Name = "Payment Date:")]
        [Required(ErrorMessage = "Payment date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Payee Name:")]
        public Int32 PayeeID { get; set; }
    }
}
