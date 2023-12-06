
using GemBox.Document;
using GemBox.Document.Tables;
using GemBox.Spreadsheet;
using System;
using System.Data;
using System.IO;

namespace EPBM
{
    public class ReportFunction
    {
        #region Shared Constants

        private const string ARIAL = "Arial";
        private const string CALIBRI = "Calibri";

        private const int DEFAULT_HEIGHT_PIXEL = 26;
        private const int DEFAULT_WIDTH_PIXEL = 180;
        private const int NUMBER_WIDTH_PIXEL = 64;

        //Excel file uses
        private const int FONT_SIZE_9 = 9 * 20;
        private const int FONT_SIZE_10 = 10 * 20;
        private const int FONT_SIZE_11 = 11 * 20;
        private const int FONT_SIZE_12 = 12 * 20;
        private const int FONT_SIZE_14 = 14 * 20;
        private const int FONT_SIZE_16 = 16 * 20;

        private const string CARRIAGE_RETURN = "\r";
        private const string DOUBLE_SPACE = "  ";
        private const string NEW_LINE = "\n";
        private const string TAB = "\t";

        private const string FORMAT_DATE = "dd MMM yyyy";

        private const string LAPORAN = "Laporan";     
        private const string CARIAN_MENGIKUT_SYARIKAT_PDF = "Carian_Mengikut_Syarikat.pdf";
        private const string CARIAN_MENGIKUT_SYARIKAT_XLSX = "Carian_Mengikut_Syarikat.xlsx";
        private const string SENARAI_PEROLEHAN_PDF = "Senarai_Perolehan.pdf";
        private const string SENARAI_PEROLEHAN_XLSX = "Senarai_Perolehan.xlsx";
        private const string LAPORAN_PDF = "Laporan.pdf";
        private const string LAPORAN_XLSX = "Laporan.xlsx";

        #endregion

        public ReportFunction()
        {
            string gemBoxSpreadSheet = ServiceUtility.GetgGemBoxExcelLicense();
            gemBoxSpreadSheet = !String.IsNullOrWhiteSpace(gemBoxSpreadSheet) ? gemBoxSpreadSheet : Constants.FREE_LIMITED_KEY;

            string gemBoxWord = ServiceUtility.GetgGemBoxWordLicense();
            gemBoxWord = !String.IsNullOrWhiteSpace(gemBoxWord) ? gemBoxWord : Constants.FREE_LIMITED_KEY;

            SpreadsheetInfo.SetLicense(gemBoxSpreadSheet);
            ComponentInfo.SetLicense(gemBoxWord);
        }

        #region Color

        private SpreadsheetColor GetColorBlack()
        {
            return SpreadsheetColor.FromArgb(0, 0, 0);
        }

        private SpreadsheetColor GetColorLightGrey()
        {
            return SpreadsheetColor.FromArgb(242, 242, 242);
        }

        #endregion

        #region Excel Reports _Carian 



        public void GenerateLaporanExcel(string title, DataTable dataTable, string totalAmount, ref string fileName, ref string filePath)
        {

            fileName = LAPORAN_XLSX;
            filePath = Path.Combine(Constants.TEMP_PATH, fileName);

            GenerateExcelLaporan(dataTable, title, totalAmount, filePath);
        }

        private void GenerateExcelLaporan(DataTable dataTable, string title, string totalAmount, string filePath)
        {
            ExcelFile excelFile = new ExcelFile();
            ExcelWorksheet workSheet = excelFile.Worksheets.Add(LAPORAN);
            workSheet.Cells.Style.Font.Name = CALIBRI;
            workSheet.Cells.Style.Font.Size = FONT_SIZE_12;
            workSheet.Cells.Style.VerticalAlignment = VerticalAlignmentStyle.Center;

            #region Logo & Title

            workSheet.Cells["A1"].Value = "SULIT";
            workSheet.Cells["A1"].Style.Font.Size = FONT_SIZE_12;
            workSheet.Cells["A1"].Style.Font.Weight = ExcelFont.BoldWeight;

            CellRange titleCellRange = workSheet.Cells.GetSubrangeAbsolute(1, 0, 1, dataTable.Columns.Count - 1);
            titleCellRange.Merged = true;
            titleCellRange.Style.WrapText = true;
            titleCellRange.Value = title;
            titleCellRange.Style.Indent = 13;
            titleCellRange.Style.Font.Size = FONT_SIZE_14;
            titleCellRange.Style.Font.Weight = ExcelFont.BoldWeight;
            titleCellRange.Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;
            titleCellRange.Style.VerticalAlignment = VerticalAlignmentStyle.Center;

            workSheet.Pictures.Add(Constants.LOGO_PATH, 0, 28, 116, 93, GemBox.Spreadsheet.LengthUnit.Pixel);
            workSheet.Rows[1].SetHeight(110, GemBox.Spreadsheet.LengthUnit.Pixel);

            #endregion

            #region Table Header

            int headerRow = 2;
            int columnIndex = 0;

            foreach (DataColumn column in dataTable.Columns)
            {
                workSheet.Cells[headerRow, columnIndex].Value = column.ColumnName;
                workSheet.Cells[headerRow, columnIndex].Style.Font.Size = FONT_SIZE_12;
                workSheet.Cells[headerRow, columnIndex].Style.Font.Weight = ExcelFont.BoldWeight;
                workSheet.Cells[headerRow, columnIndex].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                workSheet.Cells[headerRow, columnIndex].Style.Borders.SetBorders(GemBox.Spreadsheet.MultipleBorders.All, GetColorBlack(), LineStyle.Thin);
                workSheet.Cells[headerRow, columnIndex].Style.FillPattern.SetSolid(GetColorLightGrey());

                columnIndex++;
            }

            #endregion

            #region Data

            workSheet.InsertDataTable(dataTable, new InsertDataTableOptions { StartRow = 3 });

            CellRange dataCells = workSheet.Cells.GetSubrangeAbsolute(headerRow + 1, 0, headerRow + dataTable.Rows.Count, workSheet.CalculateMaxUsedColumns() - 1);
            dataCells.Style.Borders.SetBorders(GemBox.Spreadsheet.MultipleBorders.All, GetColorBlack(), LineStyle.Thin);
            dataCells.Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;

            if (!String.IsNullOrWhiteSpace(totalAmount)) workSheet.Cells[dataTable.Rows.Count + 5, 1].Value = "Jumlah Peruntukan : " + totalAmount;

            workSheet.Cells[dataTable.Rows.Count + 7, workSheet.CalculateMaxUsedColumns() - 1].Value = "SULIT";
            workSheet.Cells[dataTable.Rows.Count + 7, workSheet.CalculateMaxUsedColumns() - 1].Style.Font.Size = FONT_SIZE_12;
            workSheet.Cells[dataTable.Rows.Count + 7, workSheet.CalculateMaxUsedColumns() - 1].Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;
            workSheet.Cells[dataTable.Rows.Count + 7, workSheet.CalculateMaxUsedColumns() - 1].Style.Font.Weight = ExcelFont.BoldWeight;

            #endregion

            #region Autofit

            for (int _column = 0; _column < workSheet.CalculateMaxUsedColumns(); _column++)
            {
                workSheet.Columns[_column].AutoFit(1, workSheet.Rows[1], workSheet.Rows[workSheet.Rows.Count - 1]);
                workSheet.Columns[_column].SetWidth(workSheet.Columns[_column].GetWidth(GemBox.Spreadsheet.LengthUnit.Pixel) + 3, GemBox.Spreadsheet.LengthUnit.Pixel);
            }

            excelFile.Save(filePath, GemBox.Spreadsheet.SaveOptions.XlsxDefault);

            #endregion
        }

        #endregion

        #region PDF Report_Carian 

        public void GenerateLaporanPdf(string title, DataTable dataTable, string totalAmount, ref string fileName, ref string filePath)
        {

            fileName = LAPORAN_PDF;
            filePath = Path.Combine(Constants.TEMP_PATH, fileName);

            GeneratePdfLaporan(dataTable, title, totalAmount, filePath);
        }

        private void GeneratePdfLaporan(DataTable dataTable, string title, string totalAmount, string filePath, bool isA3 = false)
        {
            DocumentModel document = new DocumentModel();
            document.DefaultCharacterFormat.FontName = CALIBRI;

            #region Header Table

            GemBox.Document.Tables.Table headerTable = new GemBox.Document.Tables.Table(document);
            headerTable.TableFormat.PreferredWidth = new TableWidth(100, TableWidthUnit.Percentage);
            headerTable.TableFormat.AutomaticallyResizeToFitContents = false;
            headerTable.TableFormat.Alignment = HorizontalAlignment.Left;
            headerTable.TableFormat.DefaultCellPadding = new Padding(8, 0, 8, 20, GemBox.Document.LengthUnit.Pixel);

            TableRow titleRow = new TableRow(document);

            Paragraph logoParagraph = new Paragraph(document);
            Picture logo = new Picture(document, Constants.LOGO_PATH, 116, 93, GemBox.Document.LengthUnit.Pixel);
            logoParagraph.Inlines.Add(logo);

            TableCell logoCell = new TableCell(document, logoParagraph);
            logoCell.CellFormat.PreferredWidth = new TableWidth(isA3 ? 9 : 13, TableWidthUnit.Percentage);
            logoCell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single, Color.White, 1);
            titleRow.Cells.Add(logoCell);

            Paragraph paragraphTitle = new Paragraph(document);
            GetTitleText2(title, document, ref paragraphTitle);

            TableCell titleCell = new TableCell(document, paragraphTitle);
            titleCell.CellFormat.VerticalAlignment = VerticalAlignment.Center;
            titleCell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single, Color.White, 1);
            titleRow.Cells.Add(titleCell);

            headerTable.Rows.Add(titleRow);

            #endregion

            #region Data Table

            GemBox.Document.Tables.Table contentTable = new GemBox.Document.Tables.Table(document);
            contentTable.TableFormat.PreferredWidth = new TableWidth(100, TableWidthUnit.Percentage);
            contentTable.TableFormat.AutomaticallyResizeToFitContents = true;
            contentTable.TableFormat.Alignment = HorizontalAlignment.Center;
            contentTable.TableFormat.DefaultCellPadding = new Padding(8, 4, GemBox.Document.LengthUnit.Pixel);

            TableRow columnHeaderRow = new TableRow(document);
            columnHeaderRow.RowFormat.RepeatOnEachPage = true;

            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                TableCell columnCell = new TableCell(document);

                Run col = new Run(document, dataColumn.ColumnName);
                col.CharacterFormat.Style = (CharacterStyle)document.Styles.GetOrAdd(StyleTemplateType.Strong);

                Paragraph paragraph = new Paragraph(document, col);
                paragraph.ParagraphFormat.Alignment = HorizontalAlignment.Center;

                columnCell.Blocks.Add(paragraph);
                columnCell.CellFormat.BackgroundColor = new Color(242, 242, 242);
                columnCell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single, Color.Black, 1);
                columnCell.CellFormat.VerticalAlignment = VerticalAlignment.Center;

                columnHeaderRow.Cells.Add(columnCell);
            }

            contentTable.Rows.Add(columnHeaderRow);

            for (int _row = 0; _row < dataTable.Rows.Count; _row++)
            {
                TableRow row = new TableRow(document);
                row.RowFormat.AllowBreakAcrossPages = false;

                for (int _column = 0; _column < dataTable.Columns.Count; _column++)
                {
                    TableCell cell = new TableCell(document);
                    cell.CellFormat.VerticalAlignment = VerticalAlignment.Center;

                    Paragraph paragraph = new Paragraph(document);
                    GetParagraphText1((dataTable.Rows[_row][_column]).ToString(), document, ref paragraph);
                    paragraph.ParagraphFormat.Alignment = HorizontalAlignment.Center;

                    cell.Blocks.Add(paragraph);
                    row.Cells.Add(cell);
                }
                contentTable.Rows.Add(row);
            }

            Paragraph summaryTitle = new Paragraph(document);
            summaryTitle.Inlines.Add(new SpecialCharacter(document, SpecialCharacterType.LineBreak));

            if (!String.IsNullOrWhiteSpace(totalAmount)) GetTitleText1("Jumlah Peruntukan : " + totalAmount, document, ref summaryTitle);

            #endregion

            #region Section

            Paragraph topParagraph = new Paragraph(document);
            GetTitleText1("SULIT", document, ref topParagraph);

            Paragraph bottomParagraph = new Paragraph(document);
            GetTitleText1("SULIT", document, ref bottomParagraph, 14, true);

            GemBox.Document.HeaderFooter footer = new GemBox.Document.HeaderFooter(document, HeaderFooterType.FooterDefault,
                 bottomParagraph, new Paragraph(document, new Run(document, "Page "), new Field(document, FieldType.Page), new Run(document, " of "), new Field(document, FieldType.NumPages)) { ParagraphFormat = new ParagraphFormat() { Alignment = HorizontalAlignment.Right } });

            Section section = new Section(document, topParagraph, headerTable, contentTable, summaryTitle);
            section.HeadersFooters.Add(footer);

            double defaultMargin = GemBox.Document.LengthUnitConverter.Convert(0.45, GemBox.Document.LengthUnit.Inch, GemBox.Document.LengthUnit.Point);
            double bottomMargin = GemBox.Document.LengthUnitConverter.Convert(0.25, GemBox.Document.LengthUnit.Inch, GemBox.Document.LengthUnit.Point);

            section.PageSetup.PaperType = isA3 ? GemBox.Document.PaperType.A3 : GemBox.Document.PaperType.A4;

            section.PageSetup.Orientation = Orientation.Landscape;
            section.PageSetup.PageMargins = new PageMargins() { Bottom = bottomMargin, Left = defaultMargin, Right = defaultMargin, Top = defaultMargin };
            section.PageSetup.PageMargins.Footer = GemBox.Document.LengthUnitConverter.Convert(0.3, GemBox.Document.LengthUnit.Centimeter, GemBox.Document.LengthUnit.Point);

            document.Sections.Add(section);

            #endregion

            document.Save(filePath, GemBox.Document.SaveOptions.PdfDefault);
        }

        private void GetParagraphText2(string text, DocumentModel document, ref Paragraph paragraph, int fontSize = 12)
        {
            text = text.Replace(CARRIAGE_RETURN, String.Empty).Replace(TAB, DOUBLE_SPACE);
            string[] paragraphs = text.Split(new string[] { NEW_LINE }, StringSplitOptions.RemoveEmptyEntries);

            int ctr = 0;
            foreach (string _text in paragraphs)
            {
                if (ctr > 0)
                    paragraph.Inlines.Add(new SpecialCharacter(document, SpecialCharacterType.LineBreak));

                paragraph.Inlines.Add(new Run(document, _text) { CharacterFormat = { Size = fontSize } });
                ctr++;
            }
        }

        private void GetTitleText2(string text, DocumentModel document, ref Paragraph paragraph, int fontSize = 14, bool isRight = false)
        {
            text = text.Replace(CARRIAGE_RETURN, String.Empty);
            string[] paragraphs = text.Split(new string[] { NEW_LINE }, StringSplitOptions.RemoveEmptyEntries);

            int ctr = 0;
            foreach (string _text in paragraphs)
            {
                if (ctr > 0)
                    paragraph.Inlines.Add(new SpecialCharacter(document, SpecialCharacterType.LineBreak));

                string[] tabLines = _text.Split(new string[] { TAB }, StringSplitOptions.None);

                if (tabLines.Length > 1)
                {
                    int _ctr = 0;
                    foreach (string _tabline in tabLines)
                    {
                        if (_ctr > 0)
                            paragraph.Inlines.Add(new SpecialCharacter(document, SpecialCharacterType.Tab));

                        Run subTitle = new Run(document, _tabline);
                        subTitle.CharacterFormat.Style = (CharacterStyle)document.Styles.GetOrAdd(StyleTemplateType.Strong);
                        subTitle.CharacterFormat.Size = fontSize;

                        paragraph.Inlines.Add(subTitle);

                        if (isRight) paragraph.ParagraphFormat.Alignment = HorizontalAlignment.Right;

                        _ctr++;
                    }
                }
                else
                {
                    Run title = new Run(document, _text);
                    title.CharacterFormat.Style = (CharacterStyle)document.Styles.GetOrAdd(StyleTemplateType.Strong);
                    title.CharacterFormat.Size = fontSize;

                    paragraph.Inlines.Add(title);

                    if (isRight) paragraph.ParagraphFormat.Alignment = HorizontalAlignment.Right;
                }

                ctr++;
            }
        }

        #endregion


        #region Excel Reports


        public void GenerateLaporanSyarikatExcel(string title, DataTable dataTable, string totalAmount, ref string fileName, ref string filePath)
        {
          
            fileName = CARIAN_MENGIKUT_SYARIKAT_XLSX;
            filePath = Path.Combine(Constants.TEMP_PATH,fileName);

            GenerateExcelReport(dataTable, title, totalAmount, filePath);
        }
        
        private void GenerateExcelReport(DataTable dataTable, string title, string totalAmount, string filePath)
        {
            ExcelFile excelFile = new ExcelFile();
            ExcelWorksheet workSheet = excelFile.Worksheets.Add(LAPORAN);
            workSheet.Cells.Style.Font.Name = CALIBRI;
            workSheet.Cells.Style.Font.Size = FONT_SIZE_12;
            workSheet.Cells.Style.VerticalAlignment = VerticalAlignmentStyle.Center;

            #region Logo & Title

            workSheet.Cells["A1"].Value = "SULIT";
            workSheet.Cells["A1"].Style.Font.Size = FONT_SIZE_12;
            workSheet.Cells["A1"].Style.Font.Weight = ExcelFont.BoldWeight;

            CellRange titleCellRange = workSheet.Cells.GetSubrangeAbsolute(1, 0, 1, dataTable.Columns.Count - 1);
            titleCellRange.Merged = true;
            titleCellRange.Style.WrapText = true;
            titleCellRange.Value = title;
            titleCellRange.Style.Indent = 13;
            titleCellRange.Style.Font.Size = FONT_SIZE_14;
            titleCellRange.Style.Font.Weight = ExcelFont.BoldWeight;
            titleCellRange.Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;
            titleCellRange.Style.VerticalAlignment = VerticalAlignmentStyle.Center;

            workSheet.Pictures.Add(Constants.LOGO_PATH, 0, 28, 116, 93, GemBox.Spreadsheet.LengthUnit.Pixel);
            workSheet.Rows[1].SetHeight(110, GemBox.Spreadsheet.LengthUnit.Pixel);

            #endregion

            #region Table Header

            int headerRow = 2;
            int columnIndex = 0;

            foreach (DataColumn column in dataTable.Columns)
            {
                workSheet.Cells[headerRow, columnIndex].Value = column.ColumnName;
                workSheet.Cells[headerRow, columnIndex].Style.Font.Size = FONT_SIZE_12;
                workSheet.Cells[headerRow, columnIndex].Style.Font.Weight = ExcelFont.BoldWeight;
                workSheet.Cells[headerRow, columnIndex].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                workSheet.Cells[headerRow, columnIndex].Style.Borders.SetBorders(GemBox.Spreadsheet.MultipleBorders.All, GetColorBlack(), LineStyle.Thin);
                workSheet.Cells[headerRow, columnIndex].Style.FillPattern.SetSolid(GetColorLightGrey());

                columnIndex++;
            }

            #endregion

            #region Data

            workSheet.InsertDataTable(dataTable, new InsertDataTableOptions { StartRow = 3 });

            CellRange dataCells = workSheet.Cells.GetSubrangeAbsolute(headerRow + 1, 0, headerRow + dataTable.Rows.Count, workSheet.CalculateMaxUsedColumns() - 1);
            dataCells.Style.Borders.SetBorders(GemBox.Spreadsheet.MultipleBorders.All, GetColorBlack(), LineStyle.Thin);
            dataCells.Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;

            if (!String.IsNullOrWhiteSpace(totalAmount)) workSheet.Cells[dataTable.Rows.Count + 5, 1].Value = "Jumlah Peruntukan : " + totalAmount;

            workSheet.Cells[dataTable.Rows.Count + 7, workSheet.CalculateMaxUsedColumns() - 1].Value = "SULIT";
            workSheet.Cells[dataTable.Rows.Count + 7, workSheet.CalculateMaxUsedColumns() - 1].Style.Font.Size = FONT_SIZE_12;
            workSheet.Cells[dataTable.Rows.Count + 7, workSheet.CalculateMaxUsedColumns() - 1].Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;
            workSheet.Cells[dataTable.Rows.Count + 7, workSheet.CalculateMaxUsedColumns() - 1].Style.Font.Weight = ExcelFont.BoldWeight;

            #endregion

            #region Autofit

            for (int _column = 0; _column < workSheet.CalculateMaxUsedColumns(); _column++)
            {
                workSheet.Columns[_column].AutoFit(1, workSheet.Rows[1], workSheet.Rows[workSheet.Rows.Count - 1]);
                workSheet.Columns[_column].SetWidth(workSheet.Columns[_column].GetWidth(GemBox.Spreadsheet.LengthUnit.Pixel) + 3, GemBox.Spreadsheet.LengthUnit.Pixel);
            }

            excelFile.Save(filePath, GemBox.Spreadsheet.SaveOptions.XlsxDefault);

            #endregion
        }

        #endregion

        #region PDF Report

        public void GenerateLaporanSyarikatPdf (string title, DataTable dataTable, string totalAmount, ref string fileName, ref string filePath)
        {
           

            fileName = CARIAN_MENGIKUT_SYARIKAT_PDF;
            filePath = Path.Combine(Constants.TEMP_PATH,fileName);

            GeneratePdfReport(dataTable, title, totalAmount, filePath);
        }

        private void GeneratePdfReport(DataTable dataTable, string title, string totalAmount, string filePath, bool isA3 = false)
        {
            DocumentModel document = new DocumentModel();
            document.DefaultCharacterFormat.FontName = CALIBRI;

            #region Header Table

            GemBox.Document.Tables.Table headerTable = new GemBox.Document.Tables.Table(document);
            headerTable.TableFormat.PreferredWidth = new TableWidth(100, TableWidthUnit.Percentage);
            headerTable.TableFormat.AutomaticallyResizeToFitContents = false;
            headerTable.TableFormat.Alignment = HorizontalAlignment.Left;
            headerTable.TableFormat.DefaultCellPadding = new Padding(8, 0, 8, 20, GemBox.Document.LengthUnit.Pixel);

            TableRow titleRow = new TableRow(document);

            Paragraph logoParagraph = new Paragraph(document);
            Picture logo = new Picture(document, Constants.LOGO_PATH, 116, 93, GemBox.Document.LengthUnit.Pixel);
            logoParagraph.Inlines.Add(logo);

            TableCell logoCell = new TableCell(document, logoParagraph);
            logoCell.CellFormat.PreferredWidth = new TableWidth(isA3 ? 9 : 13, TableWidthUnit.Percentage);
            logoCell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single, Color.White, 1);
            titleRow.Cells.Add(logoCell);

            Paragraph paragraphTitle = new Paragraph(document);
            GetTitleText(title, document, ref paragraphTitle);

            TableCell titleCell = new TableCell(document, paragraphTitle);
            titleCell.CellFormat.VerticalAlignment = VerticalAlignment.Center;
            titleCell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single, Color.White, 1);
            titleRow.Cells.Add(titleCell);

            headerTable.Rows.Add(titleRow);

            #endregion

            #region Data Table

            GemBox.Document.Tables.Table contentTable = new GemBox.Document.Tables.Table(document);
            contentTable.TableFormat.PreferredWidth = new TableWidth(100, TableWidthUnit.Percentage);
            contentTable.TableFormat.AutomaticallyResizeToFitContents = true;
            contentTable.TableFormat.Alignment = HorizontalAlignment.Center;
            contentTable.TableFormat.DefaultCellPadding = new Padding(8, 4, GemBox.Document.LengthUnit.Pixel);

            TableRow columnHeaderRow = new TableRow(document);
            columnHeaderRow.RowFormat.RepeatOnEachPage = true;

            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                TableCell columnCell = new TableCell(document);

                Run col = new Run(document, dataColumn.ColumnName);
                col.CharacterFormat.Style = (CharacterStyle)document.Styles.GetOrAdd(StyleTemplateType.Strong);

                Paragraph paragraph = new Paragraph(document, col);
                paragraph.ParagraphFormat.Alignment = HorizontalAlignment.Center;

                columnCell.Blocks.Add(paragraph);
                columnCell.CellFormat.BackgroundColor = new Color(242, 242, 242);
                columnCell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single, Color.Black, 1);
                columnCell.CellFormat.VerticalAlignment = VerticalAlignment.Center;

                columnHeaderRow.Cells.Add(columnCell);
            }

            contentTable.Rows.Add(columnHeaderRow);

            for (int _row = 0; _row < dataTable.Rows.Count; _row++)
            {
                TableRow row = new TableRow(document);
                row.RowFormat.AllowBreakAcrossPages = false;

                for (int _column = 0; _column < dataTable.Columns.Count; _column++)
                {
                    TableCell cell = new TableCell(document);
                    cell.CellFormat.VerticalAlignment = VerticalAlignment.Center;

                    Paragraph paragraph = new Paragraph(document);
                    GetParagraphText1((dataTable.Rows[_row][_column]).ToString(), document, ref paragraph);
                    paragraph.ParagraphFormat.Alignment = HorizontalAlignment.Center;

                    cell.Blocks.Add(paragraph);
                    row.Cells.Add(cell);
                }
                contentTable.Rows.Add(row);
            }

            Paragraph summaryTitle = new Paragraph(document);
            summaryTitle.Inlines.Add(new SpecialCharacter(document, SpecialCharacterType.LineBreak));

            if (!String.IsNullOrWhiteSpace(totalAmount)) GetTitleText1("Jumlah Peruntukan : " + totalAmount, document, ref summaryTitle);

            #endregion

            #region Section

            Paragraph topParagraph = new Paragraph(document);
            GetTitleText1("SULIT", document, ref topParagraph);

            Paragraph bottomParagraph = new Paragraph(document);
            GetTitleText1("SULIT", document, ref bottomParagraph, 14, true);

            GemBox.Document.HeaderFooter footer = new GemBox.Document.HeaderFooter(document, HeaderFooterType.FooterDefault,
                 bottomParagraph, new Paragraph(document, new Run(document, "Page "), new Field(document, FieldType.Page), new Run(document, " of "), new Field(document, FieldType.NumPages)) { ParagraphFormat = new ParagraphFormat() { Alignment = HorizontalAlignment.Right } });

            Section section = new Section(document, topParagraph, headerTable, contentTable, summaryTitle);
            section.HeadersFooters.Add(footer);

            double defaultMargin = GemBox.Document.LengthUnitConverter.Convert(0.45, GemBox.Document.LengthUnit.Inch, GemBox.Document.LengthUnit.Point);
            double bottomMargin = GemBox.Document.LengthUnitConverter.Convert(0.25, GemBox.Document.LengthUnit.Inch, GemBox.Document.LengthUnit.Point);

            section.PageSetup.PaperType = isA3 ? GemBox.Document.PaperType.A3 : GemBox.Document.PaperType.A4;

            section.PageSetup.Orientation = Orientation.Landscape;
            section.PageSetup.PageMargins = new PageMargins() { Bottom = bottomMargin, Left = defaultMargin, Right = defaultMargin, Top = defaultMargin };
            section.PageSetup.PageMargins.Footer = GemBox.Document.LengthUnitConverter.Convert(0.3, GemBox.Document.LengthUnit.Centimeter, GemBox.Document.LengthUnit.Point);

            document.Sections.Add(section);

            #endregion

            document.Save(filePath, GemBox.Document.SaveOptions.PdfDefault);
        }

        private void GetParagraphText(string text, DocumentModel document, ref Paragraph paragraph, int fontSize = 12)
        {
            text = text.Replace(CARRIAGE_RETURN, String.Empty).Replace(TAB, DOUBLE_SPACE);
            string[] paragraphs = text.Split(new string[] { NEW_LINE }, StringSplitOptions.RemoveEmptyEntries);

            int ctr = 0;
            foreach (string _text in paragraphs)
            {
                if (ctr > 0)
                    paragraph.Inlines.Add(new SpecialCharacter(document, SpecialCharacterType.LineBreak));

                paragraph.Inlines.Add(new Run(document, _text) { CharacterFormat = { Size = fontSize } });
                ctr++;
            }
        }

        private void GetTitleText(string text, DocumentModel document, ref Paragraph paragraph, int fontSize = 14, bool isRight = false)
        {
            text = text.Replace(CARRIAGE_RETURN, String.Empty);
            string[] paragraphs = text.Split(new string[] { NEW_LINE }, StringSplitOptions.RemoveEmptyEntries);

            int ctr = 0;
            foreach (string _text in paragraphs)
            {
                if (ctr > 0)
                    paragraph.Inlines.Add(new SpecialCharacter(document, SpecialCharacterType.LineBreak));

                string[] tabLines = _text.Split(new string[] { TAB }, StringSplitOptions.None);

                if (tabLines.Length > 1)
                {
                    int _ctr = 0;
                    foreach (string _tabline in tabLines)
                    {
                        if (_ctr > 0)
                            paragraph.Inlines.Add(new SpecialCharacter(document, SpecialCharacterType.Tab));

                        Run subTitle = new Run(document, _tabline);
                        subTitle.CharacterFormat.Style = (CharacterStyle)document.Styles.GetOrAdd(StyleTemplateType.Strong);
                        subTitle.CharacterFormat.Size = fontSize;

                        paragraph.Inlines.Add(subTitle);

                        if (isRight) paragraph.ParagraphFormat.Alignment = HorizontalAlignment.Right;

                        _ctr++;
                    }
                }
                else
                {
                    Run title = new Run(document, _text);
                    title.CharacterFormat.Style = (CharacterStyle)document.Styles.GetOrAdd(StyleTemplateType.Strong);
                    title.CharacterFormat.Size = fontSize;

                    paragraph.Inlines.Add(title);

                    if (isRight) paragraph.ParagraphFormat.Alignment = HorizontalAlignment.Right;
                }

                ctr++;
            }
        }

        #endregion

      
        #region Excel Reports_SenaraiPerolehan

        public void GenerateLaporanSenaraiPerolehanExcel(string title, DataTable dataTable, string totalAmount, ref string fileName, ref string filePath)
        {

            fileName = SENARAI_PEROLEHAN_XLSX;
            filePath = Path.Combine(Constants.TEMP_PATH, fileName);

            GenerateExcelReport1(dataTable, title, totalAmount, filePath);
        }

        private void GenerateExcelReport1(DataTable dataTable, string title, string totalAmount, string filePath)
        {
            ExcelFile excelFile = new ExcelFile();
            ExcelWorksheet workSheet = excelFile.Worksheets.Add(LAPORAN);
            workSheet.Cells.Style.Font.Name = CALIBRI;
            workSheet.Cells.Style.Font.Size = FONT_SIZE_12;
            workSheet.Cells.Style.VerticalAlignment = VerticalAlignmentStyle.Center;

            #region Logo & Title

            workSheet.Cells["A1"].Value = "SULIT";
            workSheet.Cells["A1"].Style.Font.Size = FONT_SIZE_12;
            workSheet.Cells["A1"].Style.Font.Weight = ExcelFont.BoldWeight;

            CellRange titleCellRange = workSheet.Cells.GetSubrangeAbsolute(1, 0, 1, dataTable.Columns.Count - 1);
            titleCellRange.Merged = true;
            titleCellRange.Style.WrapText = true;
            titleCellRange.Value = title;
            titleCellRange.Style.Indent = 13;
            titleCellRange.Style.Font.Size = FONT_SIZE_14;
            titleCellRange.Style.Font.Weight = ExcelFont.BoldWeight;
            titleCellRange.Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;
            titleCellRange.Style.VerticalAlignment = VerticalAlignmentStyle.Center;

            workSheet.Pictures.Add(Constants.LOGO_PATH, 0, 28, 116, 93, GemBox.Spreadsheet.LengthUnit.Pixel);

            workSheet.Rows[1].SetHeight(110, GemBox.Spreadsheet.LengthUnit.Pixel);

            #endregion

            #region Table Header

            int headerRow = 2;
            int columnIndex = 0;

            foreach (DataColumn column in dataTable.Columns)
            {
                workSheet.Cells[headerRow, columnIndex].Value = column.ColumnName;
                workSheet.Cells[headerRow, columnIndex].Style.Font.Size = FONT_SIZE_12;
                workSheet.Cells[headerRow, columnIndex].Style.Font.Weight = ExcelFont.BoldWeight;
                workSheet.Cells[headerRow, columnIndex].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                workSheet.Cells[headerRow, columnIndex].Style.Borders.SetBorders(GemBox.Spreadsheet.MultipleBorders.All, GetColorBlack(), LineStyle.Thin);
                workSheet.Cells[headerRow, columnIndex].Style.FillPattern.SetSolid(GetColorLightGrey());

                columnIndex++;
            }

            #endregion

            #region Data

            workSheet.InsertDataTable(dataTable, new InsertDataTableOptions { StartRow = 3 });

            CellRange dataCells = workSheet.Cells.GetSubrangeAbsolute(headerRow + 1, 0, headerRow + dataTable.Rows.Count, workSheet.CalculateMaxUsedColumns() - 1);
            dataCells.Style.Borders.SetBorders(GemBox.Spreadsheet.MultipleBorders.All, GetColorBlack(), LineStyle.Thin);
            dataCells.Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;

            if (!String.IsNullOrWhiteSpace(totalAmount)) workSheet.Cells[dataTable.Rows.Count + 5, 1].Value = "Jumlah Peruntukan : " + totalAmount;

            workSheet.Cells[dataTable.Rows.Count + 7, workSheet.CalculateMaxUsedColumns() - 1].Value = "SULIT";
            workSheet.Cells[dataTable.Rows.Count + 7, workSheet.CalculateMaxUsedColumns() - 1].Style.Font.Size = FONT_SIZE_12;
            workSheet.Cells[dataTable.Rows.Count + 7, workSheet.CalculateMaxUsedColumns() - 1].Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;
            workSheet.Cells[dataTable.Rows.Count + 7, workSheet.CalculateMaxUsedColumns() - 1].Style.Font.Weight = ExcelFont.BoldWeight;

            #endregion

            #region Autofit

            for (int _column = 0; _column < workSheet.CalculateMaxUsedColumns(); _column++)
            {
                workSheet.Columns[_column].AutoFit(1, workSheet.Rows[1], workSheet.Rows[workSheet.Rows.Count - 1]);
                workSheet.Columns[_column].SetWidth(workSheet.Columns[_column].GetWidth(GemBox.Spreadsheet.LengthUnit.Pixel) + 3, GemBox.Spreadsheet.LengthUnit.Pixel);
            }

            excelFile.Save(filePath, GemBox.Spreadsheet.SaveOptions.XlsxDefault);

            #endregion
        }

        #endregion

        #region PDF Report_SenaraiPerolehan

        public void GenerateLaporanSenaraiPerolehanPdf(string title, DataTable dataTable, string totalAmount, ref string fileName, ref string filePath)
        {


            fileName = SENARAI_PEROLEHAN_PDF;
            filePath = Path.Combine(Constants.TEMP_PATH, fileName);

            GeneratePdfReportSenaraiPerolehan(dataTable, title, totalAmount, filePath);
        }

        private void GeneratePdfReportSenaraiPerolehan(DataTable dataTable, string title,  string totalAmount, string filePath, bool isA3 = false)
        {
            DocumentModel document = new DocumentModel();
            document.DefaultCharacterFormat.FontName = CALIBRI;

            #region Header Table

            GemBox.Document.Tables.Table headerTable = new GemBox.Document.Tables.Table(document);
            headerTable.TableFormat.PreferredWidth = new TableWidth(100, TableWidthUnit.Percentage);
            headerTable.TableFormat.AutomaticallyResizeToFitContents = false;
            headerTable.TableFormat.Alignment = HorizontalAlignment.Left;
            headerTable.TableFormat.DefaultCellPadding = new Padding(8, 0, 8, 20, GemBox.Document.LengthUnit.Pixel);

            TableRow titleRow = new TableRow(document);

            Paragraph logoParagraph = new Paragraph(document);
            Picture logo = new Picture(document, Constants.LOGO_PATH, 116, 93, GemBox.Document.LengthUnit.Pixel);
            logoParagraph.Inlines.Add(logo);

            TableCell logoCell = new TableCell(document, logoParagraph);
            logoCell.CellFormat.PreferredWidth = new TableWidth(isA3 ? 9 : 13, TableWidthUnit.Percentage);
            logoCell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single, Color.White, 1);
            titleRow.Cells.Add(logoCell);

            Paragraph paragraphTitle = new Paragraph(document);
            GetTitleText1(title, document, ref paragraphTitle);

            TableCell titleCell = new TableCell(document, paragraphTitle);
            titleCell.CellFormat.VerticalAlignment = VerticalAlignment.Center;
            titleCell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single, Color.White, 1);
            titleRow.Cells.Add(titleCell);

            headerTable.Rows.Add(titleRow);

            #endregion

            #region Data Table

            GemBox.Document.Tables.Table contentTable = new GemBox.Document.Tables.Table(document);
            contentTable.TableFormat.PreferredWidth = new TableWidth(100, TableWidthUnit.Percentage);
            contentTable.TableFormat.AutomaticallyResizeToFitContents = true;
            contentTable.TableFormat.Alignment = HorizontalAlignment.Center;
            contentTable.TableFormat.DefaultCellPadding = new Padding(8, 4, GemBox.Document.LengthUnit.Pixel);

            TableRow columnHeaderRow = new TableRow(document);
            columnHeaderRow.RowFormat.RepeatOnEachPage = true;
           
            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                TableCell columnCell = new TableCell(document);

                Run col = new Run(document, dataColumn.ColumnName);
                col.CharacterFormat.Style = (CharacterStyle)document.Styles.GetOrAdd(StyleTemplateType.Strong);

                Paragraph paragraph = new Paragraph(document, col);
                paragraph.ParagraphFormat.Alignment = HorizontalAlignment.Center;

                columnCell.Blocks.Add(paragraph);
                columnCell.CellFormat.BackgroundColor = new Color(242, 242, 242);
                columnCell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single, Color.Black, 1);
                columnCell.CellFormat.VerticalAlignment = VerticalAlignment.Center;

                columnHeaderRow.Cells.Add(columnCell);
            }

            contentTable.Rows.Add(columnHeaderRow);

            for (int _row = 0; _row < dataTable.Rows.Count; _row++)
            {
                TableRow row = new TableRow(document);
                row.RowFormat.AllowBreakAcrossPages = false;

                for (int _column = 0; _column < dataTable.Columns.Count; _column++)
                {
                    TableCell cell = new TableCell(document);
                    cell.CellFormat.VerticalAlignment = VerticalAlignment.Center;

                    Paragraph paragraph = new Paragraph(document);
                    GetParagraphText1((dataTable.Rows[_row][_column]).ToString(), document, ref paragraph);
                    paragraph.ParagraphFormat.Alignment = HorizontalAlignment.Center;

                    cell.Blocks.Add(paragraph);
                    row.Cells.Add(cell);
                }
                contentTable.Rows.Add(row);
            }

            Paragraph summaryTitle = new Paragraph(document);
            summaryTitle.Inlines.Add(new SpecialCharacter(document, SpecialCharacterType.LineBreak));

            if (!String.IsNullOrWhiteSpace(totalAmount)) GetTitleText1("Jumlah Peruntukan : " + totalAmount, document, ref summaryTitle);

            #endregion

            #region Section

            Paragraph topParagraph = new Paragraph(document);
            GetTitleText1("SULIT", document, ref topParagraph);

            Paragraph bottomParagraph = new Paragraph(document);
            GetTitleText1("SULIT", document, ref bottomParagraph, 14, true);

            GemBox.Document.HeaderFooter footer = new GemBox.Document.HeaderFooter(document, HeaderFooterType.FooterDefault,
                bottomParagraph, new Paragraph(document, new Run(document, "Page "), new Field(document, FieldType.Page), new Run(document, " of "), new Field(document, FieldType.NumPages)) { ParagraphFormat = new ParagraphFormat() { Alignment = HorizontalAlignment.Right } });

            Section section = new Section(document, topParagraph, headerTable, contentTable, summaryTitle);
            section.HeadersFooters.Add(footer);
         
            double defaultMargin = GemBox.Document.LengthUnitConverter.Convert(0.45, GemBox.Document.LengthUnit.Inch, GemBox.Document.LengthUnit.Point);
            double bottomMargin = GemBox.Document.LengthUnitConverter.Convert(0.25, GemBox.Document.LengthUnit.Inch, GemBox.Document.LengthUnit.Point);

            section.PageSetup.PaperType = isA3 ? GemBox.Document.PaperType.A3 : GemBox.Document.PaperType.A4;

            section.PageSetup.Orientation = Orientation.Landscape;
            section.PageSetup.PageMargins = new PageMargins() { Bottom = bottomMargin, Left = defaultMargin, Right = defaultMargin, Top = defaultMargin };
            section.PageSetup.PageMargins.Footer = GemBox.Document.LengthUnitConverter.Convert(0.3, GemBox.Document.LengthUnit.Centimeter, GemBox.Document.LengthUnit.Point);

            document.Sections.Add(section);

            #endregion

            document.Save(filePath, GemBox.Document.SaveOptions.PdfDefault);
        }

        private void GetParagraphText1(string text, DocumentModel document, ref Paragraph paragraph, int fontSize = 12)
        {
            text = text.Replace(CARRIAGE_RETURN, String.Empty).Replace(TAB, DOUBLE_SPACE);
            string[] paragraphs = text.Split(new string[] { NEW_LINE }, StringSplitOptions.RemoveEmptyEntries);

            int ctr = 0;
            foreach (string _text in paragraphs)
            {
                if (ctr > 0)
                    paragraph.Inlines.Add(new SpecialCharacter(document, SpecialCharacterType.LineBreak));

                paragraph.Inlines.Add(new Run(document, _text) { CharacterFormat = { Size = fontSize } });
                ctr++;
            }
        }

        private void GetTitleText1(string text, DocumentModel document, ref Paragraph paragraph, int fontSize = 14, bool isRight = false)
        {
            text = text.Replace(CARRIAGE_RETURN, String.Empty);
            string[] paragraphs = text.Split(new string[] { NEW_LINE }, StringSplitOptions.RemoveEmptyEntries);

            int ctr = 0;
            foreach (string _text in paragraphs)
            {
                if (ctr > 0)
                    paragraph.Inlines.Add(new SpecialCharacter(document, SpecialCharacterType.LineBreak));

                string[] tabLines = _text.Split(new string[] { TAB }, StringSplitOptions.None);

                if (tabLines.Length > 1)
                {
                    int _ctr = 0;
                    foreach (string _tabline in tabLines)
                    {
                        if (_ctr > 0)
                            paragraph.Inlines.Add(new SpecialCharacter(document, SpecialCharacterType.Tab));

                        Run subTitle = new Run(document, _tabline);
                        subTitle.CharacterFormat.Style = (CharacterStyle)document.Styles.GetOrAdd(StyleTemplateType.Strong);
                        subTitle.CharacterFormat.Size = fontSize;

                        paragraph.Inlines.Add(subTitle);

                        if (isRight) paragraph.ParagraphFormat.Alignment = HorizontalAlignment.Right;

                        _ctr++;
                    }
                }
                else
                {
                    Run title = new Run(document, _text);
                    title.CharacterFormat.Style = (CharacterStyle)document.Styles.GetOrAdd(StyleTemplateType.Strong);
                    title.CharacterFormat.Size = fontSize;

                    paragraph.Inlines.Add(title);

                    if (isRight) paragraph.ParagraphFormat.Alignment = HorizontalAlignment.Right;
                }

                ctr++;
            }
        }

        #endregion

        #region Clean Up

        public void ReportFileCleanUp(string nokp)
        {
            ValidateReportFolder(nokp);

            foreach (string filePath in Directory.GetFiles(Path.Combine(Constants.TEMP_PATH, nokp.ToString())))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch (Exception exception)
                {

                }
            }
        }

        public void ValidateReportFolder(string nokp)
        {
            try
            {
                string pathName = Path.Combine(Constants.TEMP_PATH, nokp.ToString());

                if (!Directory.Exists(pathName)) Directory.CreateDirectory(pathName);
            }
            catch (Exception exception)
            {

            }
        }

        #endregion

        #region Share functions

        private TableCell GetTitleTableCell(DocumentModel documentModel, string text, HorizontalAlignment horizontalAlignment, Color borderColor)
        {
            TableCell tableCell = new TableCell(documentModel);
            tableCell.CellFormat.VerticalAlignment = VerticalAlignment.Center;
            tableCell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single, borderColor, 1);

            Paragraph paragraph = new Paragraph(documentModel);
            paragraph.ParagraphFormat.Alignment = horizontalAlignment;
            paragraph.ParagraphFormat = new ParagraphFormat { SpaceAfter = 5, SpaceBefore = 10 };
            GetTitleText(text, documentModel, ref paragraph, 13);
            tableCell.Blocks.Add(paragraph);

            return tableCell;
        }

        private TableCell GetTableCell(DocumentModel documentModel, string text, HorizontalAlignment horizontalAlignment, Color borderColor)
        {
            TableCell tableCell = new TableCell(documentModel);
            tableCell.CellFormat.VerticalAlignment = VerticalAlignment.Center;
            tableCell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single, borderColor, 1);

            Paragraph paragraph1 = new Paragraph(documentModel);
            paragraph1.ParagraphFormat.Alignment = horizontalAlignment;
            GetTitleText(text, documentModel, ref paragraph1, 11);
            tableCell.Blocks.Add(paragraph1);

            return tableCell;
        }

        private TableCell GetTableCell(DocumentModel documentModel, string text1, string text2, HorizontalAlignment horizontalAlignment, Color borderColor, int spaceAfter = 0)
        {
            TableCell tableCell = new TableCell(documentModel);
            tableCell.CellFormat.PreferredWidth = new TableWidth(50, TableWidthUnit.Percentage);
            tableCell.CellFormat.VerticalAlignment = VerticalAlignment.Center;
            tableCell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single, borderColor, 1);

            Paragraph paragraph1 = new Paragraph(documentModel);
            paragraph1.ParagraphFormat.Alignment = horizontalAlignment;
            GetTitleText(text1, documentModel, ref paragraph1, 11);
            tableCell.Blocks.Add(paragraph1);

            Paragraph paragraph2 = new Paragraph(documentModel);
            paragraph2.ParagraphFormat.Alignment = horizontalAlignment;
            GetParagraphText(text2, documentModel, ref paragraph2, 10);
            paragraph2.ParagraphFormat = new ParagraphFormat { SpaceAfter = spaceAfter };
            tableCell.Blocks.Add(paragraph2);

            return tableCell;
        }

        #endregion
    }
}