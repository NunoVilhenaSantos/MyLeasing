using MyLeasing.Web.Data.Entities;
using System.ComponentModel;

namespace MyLeasing.Web.Models;

public class UserViewModel : User
{
    [DisplayName("Image")] public IFormFile ImageFile { get; set; }
}