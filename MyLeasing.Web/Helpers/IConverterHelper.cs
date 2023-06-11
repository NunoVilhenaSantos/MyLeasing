using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Models;

namespace MyLeasing.Web.Helpers;

public interface IConverterHelper
{
    Owner ToOwner(OwnerViewModel ownerViewModel,
        string? filePath, Guid fileStorageId, bool isNew);

    OwnerViewModel ToOwnerViewModel(Owner owner);

    Lessee ToLessee(LesseeViewModel lesseeViewModel,
        string? filePath, Guid fileStorageId, bool isNew);

    LesseeViewModel ToLesseeViewModel(Lessee lessee);
}