using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MyLeasing.Web.Data.Entities;

public class User : IdentityUser
{
    [DisplayName("Document")]
    [MaxLength(20,
        ErrorMessage = "The {0} field can not have more than {1} characters.")]
    [Required(ErrorMessage = "The field {0} is mandatory.")]
    public string Document { get; set; }


    [DisplayName("First Name")]
    [MaxLength(50,
        ErrorMessage = "The {0} field can not have more than {1} characters.")]
    [Required(ErrorMessage = "The field {0} is mandatory.")]
    public string FirstName { get; set; }


    [DisplayName("Last Name")]
    [MaxLength(50,
        ErrorMessage = "The {0} field can not have more than {1} characters.")]
    [Required(ErrorMessage = "The field {0} is mandatory.")]
    public string LastName { get; set; }


    [MaxLength(100,
        ErrorMessage = "The {0} field can not have more than {1} characters.")]
    public string? Address { get; set; }


    [Display(Name = "Full Name")]
    public string FullName => $"{FirstName} {LastName}";


    [Display(Name = "Full Name with Document")]
    public string FullNameWithDocument =>
        $"{FirstName} {LastName} - {Document}";


    [DisplayName("Profile Photo Id")] public Guid ProfilePhotoId { get; set; }

    public string ProfilePhotoIdUrl => ProfilePhotoId == Guid.Empty
        ? "https://supershopweb.blob.core.windows.net/noimage/noimage.png"
        : "https://storage.googleapis.com/supershoptpsicet77-nuno/users/" +
          ProfilePhotoId;
    //  : "https://supershopnunostorage.blob.core.windows.net/"+
    //    GetType().Name.ToLower()+"s/"+ImageId;
    //  : "https://supershopnunostorage.blob.core.windows.net/products/"+
    //    ProfilePhotoId;


    // [Display(Name = "Thumbnail")] public string ImageThumbnailUrl { get; set; }
    //
    // public string ImageThumbnailFullUrl =>
    //     string.IsNullOrEmpty(ImageThumbnailUrl)
    //         ? "https://supershopweb.blob.core.windows.net/noimage/noimage.png"
    //         : "https://storage.googleapis.com/supershoptpsicet77-nuno/users/" +
    //           ImageThumbnailUrl;
}