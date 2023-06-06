using System.ComponentModel;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Models;

public class OwnerViewModel : Owner
{
    [DisplayName("Image")] public IFormFile? ImageFile { get; set; }
}