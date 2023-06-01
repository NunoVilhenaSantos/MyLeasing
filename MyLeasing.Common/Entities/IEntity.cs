using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Common.Entities;

public interface IEntity
{
    [Key] int Id { get; set; }


    // bool WasDeleted { get; set; }


    // string Name { get; set; }
}