using System.ComponentModel;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Models;

public class LesseeViewModel : Lessee
{
    [DisplayName("Image")] public IFormFile? ImageFile { get; set; }
}