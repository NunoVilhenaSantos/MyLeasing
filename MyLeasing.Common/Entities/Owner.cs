using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MyLeasing.Common.Entities;

public class Owner : IEntity
{
    #region Properties

    [Key] public int Id { get; set; }


    [NotNull]
    [Required]
    [DisplayName("Document*")]
    public string Document { get; set; }


    [NotNull]
    [Required]
    [DisplayName("First Name*")]
    public string FirstName { get; set; }


    [NotNull]
    [Required]
    [DisplayName("Last Name*")]
    public string LastName { get; set; }


    [NotNull]
    [DisplayName("Owner Name")]
    public string FullName => $"{FirstName} {LastName}";


    [DisplayName("Fixed Phone")]
    [DisplayFormat(DataFormatString = "{0:(00) 0000-0000}")]
    public string? FixedPhone { get; set; }


    [DisplayName("Cell Phone")]
    [DisplayFormat(DataFormatString = "{0:(00) 0000-0000}")]
    public string? CellPhone { get; set; }


    public string? Address { get; set; }


    public User User { get; set; }

    #endregion
}