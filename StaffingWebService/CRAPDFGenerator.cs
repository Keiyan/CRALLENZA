using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using StaffingWebService.Data;
using StaffingWebService.Data.DataExtention;
using StaffingWebService.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace StaffingWebService
{
    public class CRAPDFGenerator : PDFGenerator
    {
        public static void GeneratePDF(CompteRenduActivite cra)
        {
            var activities = ActivityDAL.GetActivities(cra.Id);

            var days = new List<List<string>>();
            for (var i = 0; i < 6; i++)
            {
                var week = new List<string>();
                for (int j = 0; j < 14; j++)
                {
                    week.Add(string.Empty);
                }

                days.Add(week);
            }

            var weeks = 0;
            foreach (var activityWeek in activities)
            {
                foreach (var day in activityWeek.Activities)
                {
                    var dayPosition = 0;
                    switch (day.Date.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            break;
                        case DayOfWeek.Tuesday:
                            dayPosition = 1;
                            break;
                        case DayOfWeek.Wednesday:
                            dayPosition = 2;
                            break;
                        case DayOfWeek.Thursday:
                            dayPosition = 3;
                            break;
                        case DayOfWeek.Friday:
                            dayPosition = 4;
                            break;
                        case DayOfWeek.Saturday:
                            dayPosition = 5;
                            break;
                        case DayOfWeek.Sunday:
                            dayPosition = 6;
                            break;
                    }

                    days[weeks][2 * dayPosition] = string.Format("{0} : {1}", day.Date.ToString("dd"),
                                                               day.MorningActivity.ConvertToFile());
                    days[weeks][2 * dayPosition + 1] = string.Format("{0} : {1}", day.Date.ToString("dd"),
                                                               day.AfternoonActivity.ConvertToFile());
                }

                weeks++;
            }

            Document doc = null;
            string path = string.Empty;
            try
            {
                doc = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);

                var consultant = ConsultantDAL.GetConsultants().Single(o => o.Id == cra.ConsultantId);
                path = string.Format("{0}\\Files\\{1}{2}_{3}_{4}.pdf", HttpContext.Current.Server.MapPath("."),
                                         cra.Year, cra.Month.ToString("00"), consultant.Nom, consultant.Prenom);

                PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
                doc.Open();

                var page = doc.PageSize;

                var table = new PdfPTable(6) { TotalWidth = page.Width };
                CreateImageCell(string.Format("{0}\\Files\\Image\\Logo_Blanc_312_114.jpg", HttpContext.Current.Server.MapPath(".")), table);
                CreateSpaceCell(4, 5, table);
                CreateCRAInformationCell(string.Format("{0} {1}", consultant.Prenom, consultant.Nom), 4, table);
                
                var culture = new CultureInfo("fr-Fr");
                CreateCRAInformationCell(string.Format("CRA {0} {1}", culture.DateTimeFormat.GetMonthName(cra.Month), cra.Year), 4, table);
                CreateCRAInformationCell(string.Format("{0}", consultant.Email), 4, table);
                CreateSpaceCell(6, 10, table);

                for (var i = 0; i < 7; i++)
                {
                    foreach (var day in days)
                    {
                        CreateDayCell(day[2 * i], table);
                    }

                    foreach (var day in days)
                    {
                        CreateDayCell(day[2 * i + 1], table);
                    }

                    CreateSpaceCell(6, 10, table);
                }

                CreateSpaceCell(6, 10, table);
                CreateCommentCell(table);
                CreateSpaceCell(6, 10, table);
                CreateSignaturesCells(table);
                doc.Add(table);
            }
            finally
            {
                if (doc != null)
                {
                    doc.Close();

                }
            }
        }
    }
}