using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Common.Data.Entities;

public interface IEntity
{
    [Key] int Id { get; set; }


    // string Name { get; set; }


    // bool WasDeleted { get; set; }
}