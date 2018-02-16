using System.ComponentModel.DataAnnotations;
namespace bank_accounts.Models{
    public class RegisterViews{
        [Required]
        [MinLength(2)]
        public string FirstName{get;set;}
        [Required]
        [MinLength(2)]
        public string LastName{get;set;}
        [Required]
        [EmailAddress]
        public string Email{get;set;}

        [Required]
        [DataType(DataType.Password)]
        public string Password{get;set;} 
        [Compare ("Password",ErrorMessage="Password and PAssword confirmation do not match")]
        public string PasswordConfirmation{get;set;}
    }
    
}