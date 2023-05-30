using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MyLeasing.Common.Data.Entities;

public class Owner : IEntity
{
    #region Attributes

    private readonly string _fullName;

    #endregion


    #region Properties

    [Key] public int Id { get; set; }


    [NotNull] [DisplayName("Document*")] public string Document { get; set; }


    [NotNull] [DisplayName("First Name*")] public string FirstName { get; set; }


    [NotNull] [DisplayName("Last Name*")] public string LastName { get; set; }


    [NotNull]
    [DisplayName("Owner Name*")]
    public string FullName
    {
        get => _fullName;
        init
        {
            _fullName = value;
            _fullName = $"{FirstName} {LastName}";
        }
    }


    [DisplayName("Fixed Phone")] public string FixedPhone { get; set; }


    [DisplayName("Cell Phone")] public string CellPhone { get; set; }


    public string Address { get; set; }

    
    public User User { get; set; }

    #endregion
}