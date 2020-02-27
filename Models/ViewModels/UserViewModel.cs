using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;


namespace Team_4_Project.Models
{
    public class LoginViewModel
    {
       
        [Required]
        [Key]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

    }

    public class RegisterViewModel
    {
        [Key]
        public Int32 UserID { get; set; }

        //TODO:  Add any fields that you need for creating a new user
        //First name is provided as an example
        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name:")]
        public String FirstName { get; set; }

        [Display(Name = "Customer Middle Initial:")]
        public String MiddleInitial { get; set; }

        //TODO: Additional fields go here
        [Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = "Last Name:")]
        public String LastName { get; set; }

        //NOTE: Here is the property for email
        [Required]
        [EmailAddress]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        //NOTE: Here is the property for phone number
        [Required(ErrorMessage = "Phone number is required")]
        [Phone]
        [Display(Name = "Phone Number:")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Street Address:")]
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
        [RegularExpression(@"^[0-9]{5}(?:-[0-9]{4})?$", ErrorMessage = "Please enter valid zip code")]
        public string ZipCodeAddress { get; set; }

        [Display(Name = "Customer Birthday:")]
        [Required(ErrorMessage = "Birthday is required")]
        [DisplayFormat(DataFormatString = "{0: mm/dd/yyyy")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        //NOTE: Here is the logic for putting in a password
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel1
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [EmailAddress]
        [Display(Name = "Email:")]
        public string Email { get; set; }
    }

    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public String UserName { get; set; }
        public String Email { get; set; }
        public String UserID { get; set; }
    }
}