using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Web.Models;

public class ChangeUserViewModel
{
    [Required] [DisplayName("First Name")] public string FirstName { get; set; }


    [Required] [DisplayName("Last Name")] public string LastName { get; set; }
}