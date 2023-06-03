using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.Win32;
using SS23_SWEN2_TourPlanner_WPF.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
//using System.Windows.Documents;

namespace SS23_SWEN2_TourPlanner_WPF.BL
{
    public class Report
    {
        private void AddHeader(Document document, string headerText, int fontSize = 32, bool isBold = true, iText.Kernel.Colors.Color fontColor = null)
        {
            var header = new Paragraph(headerText);
            header.SetFontSize(fontSize);
            header.SetBold();
            header.SetFontColor(fontColor ?? iText.Kernel.Colors.ColorConstants.DARK_GRAY);
            document.Add(header);
        }

        private void AddBasicInfoRow(Document document, Tour tour)
        {
            string fmt;
            if (tour.Time.Days > 0)
                fmt = "%d' days 'h' hours 'm' minutes'";
            else
                fmt = "h' hours 'm' minutes'";
            var basicInfo = new Paragraph($"{tour.From} - {tour.To},\n {Math.Round(tour.Distance, 2)}km, {tour.Time.ToString(fmt)}");
            basicInfo.SetFontSize(18);
            basicInfo.SetFontColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY);
            document.Add(basicInfo);
        }

        private void AddDescription(Document document, string description)
        {
            var para = new Paragraph(description);
            para.SetFontSize(16);
            para.SetFontColor(iText.Kernel.Colors.ColorConstants.DARK_GRAY);
            document.Add(para);
        }

        private void AddTourLogs(Document document, Tour tour)
        {
            if (tour.TourLogs.Count != 0)
            {
                var header = new Paragraph("Logs");
                header.SetFontSize(16);
                header.SetBold();
                header.SetFontColor(iText.Kernel.Colors.ColorConstants.DARK_GRAY);
                document.Add(header);

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
        }

        private void AddImage(Document document, string image)
        {
            if (File.Exists(image))
            {
                var img = new Image(iText.IO.Image.ImageDataFactory.Create(image));
                img.SetWidth(400);
                img.SetHeight(300);
                img.SetMarginTop(20);
                document.Add(img);
            }
        }

        private void CreateTourReport(Document document, Tour tour)
        {
            AddHeader(document, tour.Name, 42);
            AddBasicInfoRow(document, tour);
            AddDescription(document, tour.Description);
            AddStatistics(document, tour.TourLogs);
            AddTourLogs(document, tour);
            AddImage(document, tour.Image);
        }

        private void AddStatistics(Document document, List<TourLog> tourLogs)
        {
            if (tourLogs.Count == 0)
                return;

            var header = new Paragraph("Statistics");
            header.SetFontSize(16);
            header.SetBold();
            header.SetFontColor(iText.Kernel.Colors.ColorConstants.DARK_GRAY);
            document.Add(header);

            var totalRating = 0;
            var totalTime = new TimeSpan();
            var totalDifficulty = 0;

            foreach (TourLog tourLog in tourLogs)
            {
                totalRating += tourLog.Rating;
                totalDifficulty += (int)tourLog.Difficulty;
                totalTime = totalTime.Add(tourLog.TotalTime);
            }

            var averageRating = (double)totalRating / (double)tourLogs.Count;
            var averageDifficulty = (double)totalDifficulty / (double)tourLogs.Count;
            var averageTime = totalTime.Divide(tourLogs.Count);

            Table table = new Table(4);
            table.AddHeaderCell("Average Difficulty");
            table.AddHeaderCell("Average Time");
            table.AddHeaderCell("Average Rating");
            table.AddHeaderCell("Amount of Logs");

            table.AddCell(((Difficulty)averageDifficulty).ToString());
            table.AddCell(averageTime.ToString());
            table.AddCell(averageRating.ToString());
            table.AddCell(tourLogs.Count.ToString());

            document.Add(table);
        }

        public void CreateReport(IEnumerable<Tour> tours, string filepath)
        {
            // Create the PDF report
            PdfWriter writer = new PdfWriter(filepath);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            AddHeader(document, "Tour Report", 64);
            AddHeader(document, DateTime.Now.ToString("dd. MMM yyyy"), isBold: false, fontSize: 12, fontColor: iText.Kernel.Colors.ColorConstants.LIGHT_GRAY);

            foreach (Tour tour in tours)
            {
                document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
                CreateTourReport(document, tour);
            }

            document.Close();
        }

        public void CreateReport(Tour tour, string filepath)
        {
            // Create the PDF report
            var writer = new PdfWriter(filepath);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            AddHeader(document, "Tour Report", 64);
            AddHeader(document, DateTime.Now.ToString("dd. MMM yyyy"), isBold:false, fontSize:12, fontColor: iText.Kernel.Colors.ColorConstants.LIGHT_GRAY);
            document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
          
            CreateTourReport(document, tour);
          
            document.Close();
        }
    }
}