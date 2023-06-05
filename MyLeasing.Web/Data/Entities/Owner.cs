using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace MyLeasing.Web.Data.Entities;

public class Owner : User, IEntity
{
    #region Properties

     [Key] public int Id { get; set; }


    
    [Required]
    [DisplayName("Document*")]
    public string Document { get; set; }


    
    [Required]
    [DisplayName("First Name*")]
    public string FirstName { get; set; }


    
    [Required]
    [DisplayName("Last Name*")]
    public string LastName { get; set; }


    
    [DisplayName("Profile Photo")]
    public string ProfilePhotoURL { get; set; }


    [DisplayName("Fixed Phone")] public string? FixedPhone { get; set; }


    [DisplayName("Cell Phone")] public string? CellPhone { get; set; }


    public string? Address { get; set; }


    [DisplayName("Owner Name")]
    public string FullName => $"{FirstName} {LastName}";


    public User User { get; set; }

    #endregion
}