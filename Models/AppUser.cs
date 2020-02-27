using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Team_4_Project.Models
{
    public class AppUser : IdentityUser
    {
         
        [Display(Name = "Customer First Name:")]
        [Required(ErrorMessage = "First name is required")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Customer first name may only contain letters")]
        public String FirstName { get; set; }

        [Display(Name = "Customer Middle Initial:")]
        [StringLength(1, ErrorMessage = "Middle initial cannot exceed one character.")]
        public String MiddleInitial { get; set; }

        [Display(Name = "Customer Last Name:")]
        [Required(ErrorMessage = "Last name is required")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Customer last name may only contain letters")]
        public String LastName { get; set; }

        [Display(Name = "Customer Street Address:")]
        [Required(ErrorMessage = "Street Address is required")]
        public String StreetAddress { get; set; }

        [Display(Name = "Customer City:")]
        [Required(ErrorMessage = "City is required")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "City may only contain letters")]
        public String CityAddress { get; set; }

        [Display(Name = "Customer State:")]
        [Required(ErrorMessage = "State is required")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "State may only contain letters")]
        public String StateAddress { get; set; }

        [Display(Name = "Customer Zip Code:")]
        [Required(ErrorMessage = "Zip code is required")]
        [RegularExpression(@"^[0-9]{5}(?:-[0-9]{4})?$", ErrorMessage ="Please enter valid zip code")]
        public string ZipCodeAddress { get; set; }

        [Display(Name = "Customer Birthday:")]
        [Required(ErrorMessage = "Birthday is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: MM/dd/yyyy}")]
        public DateTime Birthday { get; set; }

        public List<Account> Accounts { get; set; }
        
    }
}
