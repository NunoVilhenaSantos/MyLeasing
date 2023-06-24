using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Models;

public class RegisterNewUserViewModel : User
{
    [Required] [DisplayName("First Name")] public string FirstName { get; set; }


    [Required] [DisplayName("Last Name")] public string LastName { get; set; }


    [Required]
    [DataType(DataType.EmailAddress)]
    public string Username { get; set; }


    [Required]
    [DataType(DataType.Password)]
    [MinLength(6)]
    public string Password { get; set; }


    [Required]
    [DataType(DataType.Password)]
    [Compare("Password")]
    [MinLength(6)]
    public string ConfirmPassword { get; set; }
}