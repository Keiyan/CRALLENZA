using iTextSharp.text;
using iTextSharp.text.pdf;

namespace StaffingWebService
{
    public class PDFGenerator
    {
        private static readonly Font Verdana = FontFactory.GetFont("Verdana", 8F, Font.NORMAL, BaseColor.BLACK);

        protected static void CreateSignaturesCells(PdfPTable table)
        {
            var signatureCell = new PdfPCell(new Phrase("Signature du client :", Verdana)) { Colspan = 2, FixedHeight = 60 };
            AddtextCell(table, signatureCell);

            signatureCell = new PdfPCell(new Phrase("Signature du consultant :", Verdana)) { Colspan = 2, FixedHeight = 60 };
            AddtextCell(table, signatureCell);

            signatureCell = new PdfPCell(new Phrase("Signature du manager :", Verdana)) { Colspan = 2, FixedHeight = 60 };
            AddtextCell(table, signatureCell);
        }

        protected static void CreateCommentCell(PdfPTable table)
        {
            var commentCell = new PdfPCell(new Phrase("Commentaires :", Verdana)) { Colspan = 6, FixedHeight = 50 };
            AddtextCell(table, commentCell);
        }

        protected static void CreateDayCell(string str, PdfPTable table)
        {
            var cell = new PdfPCell(new Phrase(str, Verdana)) { Colspan = 0, Padding = 5 };
            AddtextCell(table, cell);
        }

        protected static void CreateCRAInformationCell(string text, int colspan, PdfPTable table)
        {
            var cell = new PdfPCell(new Phrase(text, Verdana)) { Colspan = colspan, HorizontalAlignment = 0, Border = 0 };
            AddtextCell(table, cell);
        }

        protected static void CreateSpaceCell(int colspan, int fixedHeight, PdfPTable table)
        {
            var spaceCell = new PdfPCell { Colspan = colspan, FixedHeight = fixedHeight, HorizontalAlignment = 0, Border = 0 };
            AddtextCell(table, spaceCell);
        }

        protected static void CreateImageCell(string strLogoPath, PdfPTable table)
        {
            var imgCell = new PdfPCell { Colspan = 2, Rowspan = 4, HorizontalAlignment = 1 };
            var image = Image.GetInstance(strLogoPath);
            AddImageInCell(imgCell, image, 200f, 200f, 1);
            imgCell.Border = 0;
            AddtextCell(table, imgCell);
        }

        protected static void AddImageInCell(PdfPCell cell, Image image, float fitWidth, float fitHight, int alignment)
        {
            image.ScaleToFit(fitWidth, fitHight);
            image.Alignment = alignment;
            cell.AddElement(image);
        }

        protected static void AddtextCell(PdfPTable table, PdfPCell cell)
        {
            table.AddCell(cell);
        }
    }
}