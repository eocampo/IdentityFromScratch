using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityFromScratchWebApp02.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        [EmailAddress]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}