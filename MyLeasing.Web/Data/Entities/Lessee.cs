using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MyLeasing.Web.Data.Entities;

public partial class Lessee : IEntity, IPerson
{
    [Display(Name = "Full Name with Document")]
    public string FullNameWithDocument =>
        $"{FirstName} {LastName} - {Document}";

    [Key] public int Id { get; set; }
    public bool WasDeleted { get; set; }

    [DisplayName("Document*")]
    [MaxLength(20,
        ErrorMessage =
            "The {0} field can not have more than {1} characters.")]
    [Required(ErrorMessage = "The field {0} is mandatory.")]
    public string Document { get; set; }


    [DisplayName("First Name*")]
    [MaxLength(50,
        ErrorMessage =
            "The {0} field can not have more than {1} characters.")]
    [Required(ErrorMessage = "The field {0} is mandatory.")]
    public string FirstName { get; set; }


    [DisplayName("Last Name*")]
    [MaxLength(50,
        ErrorMessage =
            "The {0} field can not have more than {1} characters.")]
    [Required(ErrorMessage = "The field {0} is mandatory.")]
    public string LastName { get; set; }


    [DisplayName("Profile Photo")] public string? ProfilePhotoUrl { get; set; }

    public string? ProfilePhotoFullUrl =>
        string.IsNullOrEmpty(ProfilePhotoUrl)
            ? "~/images/PlaceHolders/legacy.png"
            : MyRegex().Replace(ProfilePhotoUrl,
                "https://storage.googleapis.com/" +
                "supershoptpsicet77-nuno/lessees/");


    public Guid ProfilePhotoId { get; set; }

    public string ProfilePhotoIdUrl => ProfilePhotoId == Guid.Empty
        ? "https://supershopweb.blob.core.windows.net/noimage/noimage.png"
        : "https://storage.googleapis.com/supershoptpsicet77-nuno/lessees/" +
          ProfilePhotoId;

    [DisplayName("Fixed Phone")] public string? FixedPhone { get; set; }


    [DisplayName("Cell Phone")] public string? CellPhone { get; set; }


    [MaxLength(100,
        ErrorMessage =
            "The {0} field can not have more than {1} characters.")]
    public string? Address { get; set; }


    [DisplayName("Owner Name")]
    public string FullName => $"{FirstName} {LastName}";


    [Required] public User User { get; set; }

    [GeneratedRegex("^~/lessees/images/")]
    private static partial Regex MyRegex();
}