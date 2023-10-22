using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MyLeasing.Web.Data.Entities;

public partial class Owner : IEntity, IPerson
{
    [Key] public int Id { get; set; }
    public bool WasDeleted { get; set; }


    [Required][DisplayName("Document*")] public string Document { get; set; }


    [Required]
    [DisplayName("First Name*")]
    public string FirstName { get; set; }


    [Required][DisplayName("Last Name*")] public string LastName { get; set; }


    [DisplayName("Profile Photo")] public string? ProfilePhotoUrl { get; set; }

    public string? ProfilePhotoFullUrl =>
        string.IsNullOrEmpty(ProfilePhotoUrl)
        ? "https://supershop.blob.core.windows.net/placeholders/noimage.png"
            : MyRegex().Replace(ProfilePhotoUrl,
                "https://supershop.blob.core.windows.net/" +
                "/owners/");


    [DisplayName("Profile Photo Id")] public Guid ProfilePhotoId { get; set; }

    public string ProfilePhotoIdUrl => ProfilePhotoId == Guid.Empty
        ? "https://supershop.blob.core.windows.net/placeholders/noimage.png"
        : "https://supershop.blob.core.windows.net/" +
        "owners" +
        "/" + ProfilePhotoId;


    [DisplayName("Fixed Phone")] public string? FixedPhone { get; set; }


    [DisplayName("Cell Phone")] public string? CellPhone { get; set; }


    public string? Address { get; set; }


    [DisplayName("Owner Name")]
    public string FullName => $"{FirstName} {LastName}";


    [Required] public User User { get; set; }

    [GeneratedRegex("^~/owners/images/")]
    private static partial Regex MyRegex();
}