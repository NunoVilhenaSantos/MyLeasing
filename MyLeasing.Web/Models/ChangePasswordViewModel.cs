using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Web.Models;

public class ChangePasswordViewModel
{
    [Required]
    [MinLength(6)]
    [DataType(DataType.Password)]
    [DisplayName("Current Password")]
    public string OldPassword { get; set; }


    [Required]
    [MinLength(6)]
    [DataType(DataType.Password)]
    [DisplayName("New Password")]
    public string NewPassword { get; set; }


    [Required]
    [MinLength(6)]
    [DataType(DataType.Password)]
    [DisplayName("Confirm Password")]
    [Compare("NewPassword")]
    public string ConfirmPassword { get; set; }
}