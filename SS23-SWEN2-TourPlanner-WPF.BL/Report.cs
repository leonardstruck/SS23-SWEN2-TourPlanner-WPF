using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using SS23_SWEN2_TourPlanner_WPF.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
//using System.Windows.Documents;

namespace SS23_SWEN2_TourPlanner_WPF.BL
{
    public class Report
    {

        public void CreateReport(ObservableCollection<Tour> tours)
        {
            // reports werden mal am Desktop gespeichert. Downloads wär cleaner aber hab ich nicht gefunden grad :(
            var path = System.IO.Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "TourPlanner", "Reports");
            System.IO.Directory.CreateDirectory(path);
            PdfWriter writer = new PdfWriter(System.IO.Path.Join(path, ($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.pdf")));
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);
            document.Add(
                new Paragraph("Tour Report")
                    .SetFontSize(64)
                    .SetBold()
                    .SetFontColor(iText.Kernel.Colors.ColorConstants.DARK_GRAY)
                );
            document.Add(
                new Paragraph(DateTime.Now.ToString("dd.MMM yyyy"))
                    .SetFontColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY)
                );
            foreach (Tour tour in tours)
            {
                document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));

                //header
                document.Add(
                    new Paragraph(tour.Name)
                        .SetFontSize(32)
                        .SetBold()
                        .SetFontColor(iText.Kernel.Colors.ColorConstants.DARK_GRAY)
                    );
                // details
                document.Add(
                    new Paragraph($"{tour.From} - {tour.To}, {tour.Distance}km {Math.Floor(tour.Time)}:{(tour.Time%1)*60}")
                        .SetFontSize(18)
                        .SetFontColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY)
                    );
                //description
                document.Add(
                    new Paragraph(tour.Description)
                       .SetFontSize(16)
                       .SetFontColor(iText.Kernel.Colors.ColorConstants.DARK_GRAY)
                    );
                // Tourlogs
                if(tour.TourLogs.Count != 0)
                {
                    document.Add(
                        new Paragraph("Logs")
                           .SetFontSize(16)
                           .SetBold()
                           .SetFontColor(iText.Kernel.Colors.ColorConstants.DARK_GRAY)
                        );
                    Table table = new Table(5);
                    table.AddHeaderCell("Date");
                    table.AddHeaderCell("Comment");
                    table.AddHeaderCell("Difficulty");
                    table.AddHeaderCell("Total Time");
                    table.AddHeaderCell("Rating");
                    foreach (TourLog tourLog in tour.TourLogs)
                    {
                        table.AddCell(tourLog.DateTime.ToString("dd.MM.yyyy"));
                        table.AddCell(tourLog.Comment);
                        table.AddCell(tourLog.Difficulty.ToString());
                        table.AddCell(tourLog.TotalTime.ToString());
                        table.AddCell(tourLog.Rating.ToString());
                    }
                    document.Add(table);
                }
                // Image
                if (File.Exists(tour.Image))
                {
                    document.Add(
                        new Image(iText.IO.Image.ImageDataFactory.Create(tour.Image))
                            .SetWidth(300)
                            .SetHeight(300)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetMarginTop(20)
                        );
                }

            }
            
            document.Close();
        }
    }
}
