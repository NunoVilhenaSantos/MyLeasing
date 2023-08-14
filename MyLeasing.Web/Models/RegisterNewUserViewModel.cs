using MyLeasing.Web.Data.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Web.Models;

public class RegisterNewUserViewModel : User
{
    [DisplayName("Image")] public IFormFile? ImageFile { get; set; }

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