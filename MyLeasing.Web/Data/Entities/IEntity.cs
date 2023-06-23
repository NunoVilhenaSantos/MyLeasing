using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Web.Data.Entities;

public interface IEntity
{
    [Key] int Id { get; set; }


    [Required] bool WasDeleted { get; set; }


    // string Name { get; set; }
}