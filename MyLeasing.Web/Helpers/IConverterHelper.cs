using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Models;

namespace MyLeasing.Web.Helpers;

public interface IConverterHelper
{
    Owner ToOwner(OwnerViewModel ownerViewModel,
        string? filePath, bool isNew);

    OwnerViewModel ToOwnerViewModel(Owner owner);

    Lessee ToLessee(LesseeViewModel lesseeViewModel,
        string? filePath, bool isNew);

    LesseeViewModel ToLesseeViewModel(Lessee lessee);
}