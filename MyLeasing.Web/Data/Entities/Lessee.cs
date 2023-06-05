using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace MyLeasing.Web.Data.Entities
{
    public class Lessee : User, IEntity
    {

        #region Properties

        [Key] public int Id { get; set; }



        [DisplayName("Document*")]
        [MaxLength(20,
            ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Document { get; set; }



        [DisplayName("First Name*")]
        [MaxLength(50,
            ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string FirstName { get; set; }


        [DisplayName("Last Name*")]
        [MaxLength(50,
            ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string LastName { get; set; }



        
        [DisplayName("Profile Photo")]
        public string ProfilePhotoURL { get; set; }


        [DisplayName("Fixed Phone")] public string? FixedPhone { get; set; }


        [DisplayName("Cell Phone")] public string? CellPhone { get; set; }


        [MaxLength(100,
            ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string? Address { get; set; }


        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";


        [Display(Name = "Full Name with Document")]
        public string FullNameWithDocument =>
            $"{FirstName} {LastName} - {Document}";


        public User User { get; set; }

        #endregion  
    }
}
