using Cellenza.Service.Model;

namespace Cellenza.Service.Data.DataExtention
{
    internal static class HolidayDataExtention
    {
        internal static Holiday CreateHoliday(this VacanceTable vacanceTable)
        {
            return new Holiday
                       {
                           Commentaire = vacanceTable.Commentaire,
                           ConsultantId = vacanceTable.ConsultantTableID,
                           DateDebut = vacanceTable.DateDebut,
                           DateFin = vacanceTable.DateFin,
                           Id = vacanceTable.ID,
                           IsActive = vacanceTable.Actif,
                           Motif = vacanceTable.Motif,
                           Status = (ItemStatus)vacanceTable.Status,
                           Type = (HolidayType)vacanceTable.Type
                       };
        }

        internal static void Update(this VacanceTable vacanceTable, Holiday holiday)
        {
            vacanceTable.Actif = holiday.IsActive;
            vacanceTable.Commentaire = holiday.Commentaire;
            vacanceTable.ConsultantTableID = holiday.ConsultantId;
            vacanceTable.DateDebut = holiday.DateDebut;
            vacanceTable.DateFin = holiday.DateFin;
            vacanceTable.ID = holiday.Id;
            vacanceTable.Motif = holiday.Motif;
            vacanceTable.Status = (int)holiday.Status;
            vacanceTable.Type = (int)holiday.Type;
        }

        internal static VacanceTable CreateDatabaseHoliday(this Holiday holiday)
        {
            return new VacanceTable
                       {
                           Commentaire = holiday.Commentaire,
                           ConsultantTableID = holiday.ConsultantId,
                           DateDebut = holiday.DateDebut,
                           DateFin = holiday.DateFin,
                           Motif = holiday.Motif,
                           Status = (int)holiday.Status,
                           Type = (int)holiday.Type
                       };
        }
    }
}