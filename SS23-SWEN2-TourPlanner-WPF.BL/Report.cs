using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.Win32;
using SS23_SWEN2_TourPlanner_WPF.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
//using System.Windows.Documents;

namespace SS23_SWEN2_TourPlanner_WPF.BL
{
    public class Report
    {
        
        public void CreateReport(ObservableCollection<Tour> tours)
        {
            // Show save file dialog
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                // Create the PDF report
                PdfWriter writer = new PdfWriter(filePath);
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
                    // string fmt = "%d' days 'hh' hours 'mm' minutes'";
                    string fmt;
                    
                    if (tour.Time.Days > 0)
                        fmt = "%d' days 'h' hours 'm' minutes'";
                    else
                        fmt = "h' hours 'm' minutes'";
                    

                    document.Add(
                        new Paragraph($"{tour.From} - {tour.To}, {Math.Round(tour.Distance, 2)}km \n{tour.Time.ToString(fmt)}")
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
                    if (tour.TourLogs.Count != 0)
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
                                .SetWidth(400)
                                .SetHeight(300)
                                .SetMarginTop(20)
                            );
                    }

                }

                document.Close();
            }
        }

        public void CreateReport(Tour tour)
        {
            // Show save file dialog
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                // Create the PDF report
                PdfWriter writer = new PdfWriter(filePath);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);
                document.Add(
                new Paragraph(tour.Name)
                    .SetFontSize(64)
                    .SetBold()
                    .SetFontColor(iText.Kernel.Colors.ColorConstants.DARK_GRAY)
                );
                document.Add(
                    new Paragraph("Tour Report")
                        .SetFontColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY)
                    );
                document.Add(
                    new Paragraph(DateTime.Now.ToString("dd.MMM yyyy"))
                        .SetFontColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY)
                    );
                
                document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));

                //header
                document.Add(
                    new Paragraph(tour.Name)
                        .SetFontSize(32)
                        .SetBold()
                        .SetFontColor(iText.Kernel.Colors.ColorConstants.DARK_GRAY)
                    );
                // details
                // string fmt = "%d' days 'hh' hours 'mm' minutes'";
                string fmt;

                if (tour.Time.Days > 0)
                    fmt = "%d' days 'h' hours 'm' minutes'";
                else
                    fmt = "h' hours 'm' minutes'";


                document.Add(
                    new Paragraph($"{tour.From} - {tour.To}, {Math.Round(tour.Distance, 2)}km \n{tour.Time.ToString(fmt)}")
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
                if (tour.TourLogs.Count != 0)
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
                            .SetWidth(400)
                            .SetHeight(300)
                            .SetMarginTop(20)
                        );
                }

                

                document.Close();
            }
        }
    }
}
