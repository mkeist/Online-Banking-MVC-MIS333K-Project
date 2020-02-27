using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Team_4_Project.Models.ViewModels
{
    public class TransferViewModel
    {
        [Display(Name = "From Account:")]
        public Int32 FromAccountID { get; set; }

        [Display(Name = "To Account:")]
        public Int32 ToAccountID { get; set; }

        [Display(Name = "Transfer Amount:")]
        [Required(ErrorMessage = "Amount is required")]
        public decimal TransferAmount { get; set; }

        [Display(Name = "Transfer Date:")]
        [Required(ErrorMessage = "Transfer date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime TransferDate { get; set; }

        [Display(Name = "Comments:")]
        public string TransferComments { get; set; }


    }
}
