using MyLeasing.Web.Data.Entities;
using System.ComponentModel;

namespace MyLeasing.Web.Models;

public class LesseeViewModel : Lessee
{
    [DisplayName("Image")] public IFormFile? ImageFile { get; set; }
}