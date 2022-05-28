using System;
using System.Collections.Generic;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using WaterBalance.Models;

namespace WaterBalance.Services
{
    internal sealed class PrintDocument
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private FlowDocument CreateDocument(List<Consumer> consumersList)
        {
            FlowDocument flowDoc = new FlowDocument();

            Table table = new Table();
            table.CellSpacing = 0;

            flowDoc.Blocks.Add(table);            

            table.TextAlignment = TextAlignment.Center;

            table.Columns.Add(new TableColumn() { Width = new GridLength(30) });
            table.Columns.Add(new TableColumn());
            table.Columns.Add(new TableColumn() { Width = new GridLength(55) });
            table.Columns.Add(new TableColumn() { Width = new GridLength(65) });
            table.Columns.Add(new TableColumn() { Width = new GridLength(45) });
            table.Columns.Add(new TableColumn() { Width = new GridLength(45) });
            table.Columns.Add(new TableColumn() { Width = new GridLength(45) });
            table.Columns.Add(new TableColumn() { Width = new GridLength(45) });
            table.Columns.Add(new TableColumn() { Width = new GridLength(45) });
            table.Columns.Add(new TableColumn() { Width = new GridLength(45) });
            table.Columns.Add(new TableColumn() { Width = new GridLength(45) });
            table.Columns.Add(new TableColumn() { Width = new GridLength(45) });
            table.Columns.Add(new TableColumn() { Width = new GridLength(45) });
            table.Columns.Add(new TableColumn() { Width = new GridLength(45) });
            table.Columns.Add(new TableColumn() { Width = new GridLength(45) });
            table.Columns.Add(new TableColumn() { Width = new GridLength(45) });
            table.Columns.Add(new TableColumn());


            table.RowGroups.Add(new TableRowGroup());
            table.RowGroups[0].Rows.Add(new TableRow());

            TableRow currentRow = table.RowGroups[0].Rows[0];

            currentRow.FontSize = 16;
            currentRow.FontWeight = FontWeights.Bold; 
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Баланс водопотребления и водоотведения"))) { BorderThickness = new Thickness(1), BorderBrush = Brushes.Black });
            currentRow.Cells[0].ColumnSpan = 17;

            table.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table.RowGroups[0].Rows[1];

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(null))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(null))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(null))) { BorderThickness = new Thickness(0, 0, 0, 1), BorderBrush = Brushes.Black });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(null))) { BorderThickness = new Thickness(0, 0, 0, 1), BorderBrush = Brushes.Black });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Расход холодной воды"))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });            
            currentRow.Cells[4].ColumnSpan = 4;
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Расход горячей воды"))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });
            currentRow.Cells[5].ColumnSpan = 4;
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Водоотведение"))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });
            currentRow.Cells[6].ColumnSpan = 4;
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(null))) { BorderThickness = new Thickness(1, 0, 1, 1), BorderBrush = Brushes.Black });


            table.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table.RowGroups[0].Rows[2];

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("№"))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Водопотребитель"))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Кол-во в сутки"))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Ед. измерения"))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Норма"))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Q сут, м3/сут"))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Q ч, м3/ч"))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("q, л/с"))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Норма"))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Q сут, м3/сут"))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Q ч, м3/ч"))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("q, л/с"))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Норма"))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Q сут, м3/сут"))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Q ч, м3/ч"))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("q, л/с"))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Нормативный документ"))) { BorderThickness = new Thickness(1, 0, 1, 1), BorderBrush = Brushes.Black });


            int index = 3;

            foreach (Consumer consumer in consumersList)
            {
                table.RowGroups[0].Rows.Add(new TableRow());
                currentRow = table.RowGroups[0].Rows[index];

                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(consumer.LineNumber.ToString()))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(consumer.ConsumerName))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(consumer.Unit.ToString()))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(consumer.UnitName))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });

                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(consumer.ConsumptionRatePerDay.ToString()))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });

                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(String.Format("{0:N2}", consumer.DailyUseColdWater.ConsumptionPerDayColdWater)))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(String.Format("{0:N2}", consumer.DailyUseColdWater.ConsumptionPerHourColdWater)))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(String.Format("{0:N4}", consumer.DailyUseColdWater.ConsumptionPerSecondColdWater)))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });

                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(consumer.HotWaterConsumption.ToString()))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });

                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(String.Format("{0:N2}", consumer.DailyUseHotWater.ConsumptionPerDayHotWater)))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(String.Format("{0:N2}", consumer.DailyUseHotWater.ConsumptionPerHourHotWater)))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(String.Format("{0:N4}", consumer.DailyUseHotWater.ConsumptionPerSecondHotWater)))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });

                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(consumer.WastewaterCollection.ToString()))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });

                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(String.Format("{0:N2}", consumer.DailyWastewaterCollection.WastewaterCollectionPerDay)))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(String.Format("{0:N2}", consumer.DailyWastewaterCollection.WastewaterCollectionPerHour)))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(String.Format("{0:N4}", consumer.DailyWastewaterCollection.WastewaterCollectionPerSecond)))) { BorderThickness = new Thickness(1, 0, 0, 1), BorderBrush = Brushes.Black });

                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(consumer.NormativeDocument))) { BorderThickness = new Thickness(1, 0, 1, 1), BorderBrush = Brushes.Black });

                index++;
            }

            int lastIndex = index;

            table.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table.RowGroups[0].Rows[lastIndex];
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run())));
            table.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table.RowGroups[0].Rows[lastIndex + 1];
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run())));

            table.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table.RowGroups[0].Rows[lastIndex + 2];

            currentRow.FontSize = 14;
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Водопотребитель: ___________________________ ___________________________ ___________________________"))));
            currentRow.Cells[0].ColumnSpan = 17; 

            return flowDoc;            
        }

        public void PrintDoc(List<Consumer> items)
        {
            try
            {                
                FlowDocument flowDoc = CreateDocument(items);

                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
                    flowDoc.FontSize = 12;
                    flowDoc.PageHeight = printDialog.PrintableAreaHeight;
                    flowDoc.PageWidth = printDialog.PrintableAreaWidth;
                    flowDoc.PagePadding = new Thickness(25, 80, 25, 25);
                    flowDoc.ColumnGap = 0;
                    flowDoc.ColumnWidth = (flowDoc.PageWidth - flowDoc.PagePadding.Left - flowDoc.PagePadding.Right);

                    IDocumentPaginatorSource dps = flowDoc;
                    printDialog.PrintDocument(dps.DocumentPaginator, "Water balance");
                }      
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Method 'PrintDoc' / class 'PrintDocument'");
            }
        }
    }
}
