using System.ComponentModel;

namespace MyLeasing.Web.Models;

public class UserViewModel
{
    [DisplayName("Image")] public IFormFile ImageFile { get; set; }
}