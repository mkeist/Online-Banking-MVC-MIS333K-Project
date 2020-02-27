using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Team_4_Project.Models
{
    public enum PayeeType { CreditCard, Utilities, Rent, Mortgage, Other}
    public class Payee
    {
        public Int32 PayeeID {get; set;}

        public List<Transaction> Transactions { get; set; }

        [Display(Name = "Payee Name:")]
        public string Name { get; set; }

        [Display(Name = "Street Address:")]
        public string PayeeStreetAddress { get; set; }

        [Display(Name = "City Address:")]
        public string PayeeCityAddress { get; set; }

        [Display(Name = "State Address:")]
        public string PayeeStateAddress { get; set; }

        [Display(Name = "Zip Code:")]
        public string PayeeZipCode { get; set; }

        [Display(Name = "Phone Number:")]
        [Phone]
        public string PayeePhoneNumber { get; set; }

        [Display(Name = "Payee Type:")]
        public PayeeType PayeeType { get; set; }


    }
}
