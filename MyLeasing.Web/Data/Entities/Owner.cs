using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Web.Data.Entities;

public class Owner : IEntity
{
    public Owner()
    {
    }

    public Owner(int id, string firstName, string lastName, string document,
        string? fixedPhone, string? cellPhone, string? address, User? user)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Document = document;
        FixedPhone = fixedPhone;
        CellPhone = cellPhone;
        Address = address;
        User = user;
    }

    [Required] [DisplayName("Document*")] public string Document { get; set; }


    [Required]
    [DisplayName("First Name*")]
    public string FirstName { get; set; }


    [Required] [DisplayName("Last Name*")] public string LastName { get; set; }


    [DisplayName("Profile Photo")] public string? ProfilePhotoUrl { get; set; }


    public string? ProfilePhotoFullUrl =>
        string.IsNullOrEmpty(ProfilePhotoUrl)
            ? null
            : $"https://supermarketapi.azurewebsites.net{ProfilePhotoUrl[1..]}";


    // [Display(Name = "Thumbnail")]
    // public string ImageThumbnailUrl { get; set; }
    //
    // public string ImageThumbnailFullUrl =>
    //     string.IsNullOrEmpty(ImageThumbnailUrl)
    //         ? null
    //         : $"https://supermarketapi.azurewebsites.net{ImageThumbnailUrl[1..]}";


    [DisplayName("Fixed Phone")] public string? FixedPhone { get; set; }


    [DisplayName("Cell Phone")] public string? CellPhone { get; set; }


    public string? Address { get; set; }


    [DisplayName("Owner Name")]
    public string FullName => $"{FirstName} {LastName}";


    [Required] public User? User { get; set; }
    [Key] public int Id { get; set; }
}