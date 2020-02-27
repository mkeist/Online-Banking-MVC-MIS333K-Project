using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Team_4_Project.Models
{
    public enum DisputeStatus {Submitted, Accepted, Rejected, Adjusted}
    public class Disputes
    {
       
        public Int32 DisputesID {get; set;}

        public Transaction Transaction { get; set; }

        [Display(Name = "Comments:")]
        [Required(ErrorMessage = "Comments are required.")]
        public string DisputeComment { get; set; }

        [Display(Name = "Manager Comments:")]
        public string ManagerComment { get; set; }

        [Display(Name = "Manager Adjusted Amount:")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal ManagerAdjustAmount { get; set; }

        [Display(Name = "Dispute Status:")]
        public DisputeStatus DisputeStatus { get; set; }

        [Display(Name = "Correct Amount:")]
        [Required(ErrorMessage = "Correct amount is required.")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal CorrectAmount { get; set; }

        [Display(Name = "Request for Deletion")]
        public Boolean RequestDelete { get; set; }
    }
}
