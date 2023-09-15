
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
        private const string LAPORAN_PERSENDIRIAN_PDF = "Laporan_Persendirian.pdf";
        private const string LAPORAN_PERSENDIRIAN_A3_PDF = "Laporan_Persendirian_A3.pdf";
        private const string LAPORAN_PERSENDIRIAN_XLSX = "Laporan_Persendirian.xlsx";
        private const string LAPORAN_RASMI_PDF = "Laporan_Rasmi.pdf";
        private const string LAPORAN_RASMI_A3_PDF = "Laporan_Rasmi_A3.pdf";
        private const string LAPORAN_RASMI_XLSX = "Laporan_Rasmi.xlsx";
        private const string CARIAN_MENGIKUT_SYARIKAT_PDF = "Carian_Mengikut_Syarikat.pdf";
        private const string CARIAN_MENGIKUT_SYARIKAT_XLSX = "Carian_Mengikut_Syarikat.xlsx";
        private const string LAPORAN_ELAUN_PDF = "Laporan_Elaun_Baju_Panas.pdf";
        private const string LAPORAN_ELAUN_XLSX = "Laporan_Elaun_Baju_Panas.xlsx";

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

            CellRange titleCellRange = workSheet.Cells.GetSubrangeAbsolute(0, 0, 0, dataTable.Columns.Count - 1);
            titleCellRange.Merged = true;
            titleCellRange.Style.WrapText = true;
            titleCellRange.Value = title;
            titleCellRange.Style.Indent = 13;
            titleCellRange.Style.Font.Size = FONT_SIZE_14;
            titleCellRange.Style.Font.Weight = ExcelFont.BoldWeight;
            titleCellRange.Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;
            titleCellRange.Style.VerticalAlignment = VerticalAlignmentStyle.Center;

            workSheet.Pictures.Add(Constants.LOGO_PATH, 0, 8, 116, 93, GemBox.Spreadsheet.LengthUnit.Pixel);

            workSheet.Rows[0].SetHeight(110, GemBox.Spreadsheet.LengthUnit.Pixel);

            #endregion

            #region Table Header

            int headerRow = 1;
            int columnIndex = 0;

            foreach (DataColumn column in dataTable.Columns)
            {
                workSheet.Cells[headerRow, columnIndex].Value = column.ColumnName;
                workSheet.Cells[headerRow, columnIndex].Style.Font.Size = FONT_SIZE_12;
                workSheet.Cells[headerRow, columnIndex].Style.Font.Weight = ExcelFont.BoldWeight;
                workSheet.Cells[headerRow, columnIndex].Style.FillPattern.SetSolid(GetColorLightGrey());

                columnIndex++;
            }

            #endregion

            #region Data

            workSheet.InsertDataTable(dataTable, new InsertDataTableOptions { StartRow = 2 });

            CellRange dataCells = workSheet.Cells.GetSubrangeAbsolute(headerRow, 0, headerRow + dataTable.Rows.Count, workSheet.CalculateMaxUsedColumns() - 1);
            dataCells.Style.Borders.SetBorders(GemBox.Spreadsheet.MultipleBorders.All, GetColorBlack(), LineStyle.Thin);
            dataCells.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
           
            if (!String.IsNullOrWhiteSpace(totalAmount)) workSheet.Cells[dataTable.Rows.Count + 3, 1].Value = "Jumlah Peruntukan : " + totalAmount;
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
                    GetParagraphText((dataTable.Rows[_row][_column]).ToString(), document, ref paragraph);
                    paragraph.ParagraphFormat.Alignment = HorizontalAlignment.Center;

                    cell.Blocks.Add(paragraph);
                    row.Cells.Add(cell);
                }
                contentTable.Rows.Add(row);
            }

            Paragraph summaryTitle = new Paragraph(document);
            summaryTitle.Inlines.Add(new SpecialCharacter(document, SpecialCharacterType.LineBreak));

            if (!String.IsNullOrWhiteSpace(totalAmount)) GetTitleText("Jumlah Peruntukan : " + totalAmount, document, ref summaryTitle);

            #endregion

            #region Section

            GemBox.Document.HeaderFooter footer = new GemBox.Document.HeaderFooter(document, HeaderFooterType.FooterDefault,
                 new Paragraph(document, new Run(document, "Page "), new Field(document, FieldType.Page), new Run(document, " of "), new Field(document, FieldType.NumPages)) { ParagraphFormat = new ParagraphFormat() { Alignment = HorizontalAlignment.Right } });

            Section section = new Section(document, headerTable, contentTable, summaryTitle);
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

        private void GetTitleText(string text, DocumentModel document, ref Paragraph paragraph, int fontSize = 14)
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

                        _ctr++;
                    }
                }
                else
                {
                    Run title = new Run(document, _text);
                    title.CharacterFormat.Style = (CharacterStyle)document.Styles.GetOrAdd(StyleTemplateType.Strong);
                    title.CharacterFormat.Size = fontSize;

                    paragraph.Inlines.Add(title);
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


        //#region Cuti agama

        //public void GenerateCutiAgamaForm(DataTable dataTable, ref string fileName, ref string filePath, int statusLevel = 0)
        //{
           

        //    fileName = "Maklumat-Permohonan-Cuti.pdf";
        //    filePath = Path.Combine(Constants.TEMP_PATH,fileName);

        //    DocumentModel document = new DocumentModel();
        //    document.DefaultCharacterFormat.FontName = CALIBRI;

        //    #region Header Table

        //    GemBox.Document.Tables.Table headerTable = new GemBox.Document.Tables.Table(document);
        //    headerTable.TableFormat.PreferredWidth = new TableWidth(100, TableWidthUnit.Percentage);
        //    headerTable.TableFormat.AutomaticallyResizeToFitContents = false;
        //    headerTable.TableFormat.Alignment = HorizontalAlignment.Left;
        //    headerTable.TableFormat.DefaultCellPadding = new Padding(8, 0, 8, 0, GemBox.Document.LengthUnit.Pixel);

        //    TableRow titleRow = new TableRow(document);

        //    Paragraph logoParagraph = new Paragraph(document);
        //    Picture logo = new Picture(document, Constants.LOGO_PATH, 70, 56, GemBox.Document.LengthUnit.Pixel);
        //    logoParagraph.Inlines.Add(logo);

        //    TableCell logoCell = new TableCell(document, logoParagraph);
        //    logoCell.CellFormat.PreferredWidth = new TableWidth(13, TableWidthUnit.Percentage);
        //    logoCell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single, Color.White, 1);
        //    titleRow.Cells.Add(logoCell);

        //    Paragraph upperTitle = new Paragraph(document);
        //    GetTitleText("Kementerian Sumber Asli, Alam Sekitar dan Perubahan Iklim", document, ref upperTitle);
        //    Paragraph lowerTitle = new Paragraph(document);
        //    GetParagraphText("PERMOHONAN CUTI HAJI/ UMRAH/ KEAGAMAAN BERSERTA KELULUSAN KE LUAR NEGARA", document, ref lowerTitle);

        //    TableCell titleCell = new TableCell(document, upperTitle, lowerTitle);
        //    titleCell.CellFormat.VerticalAlignment = VerticalAlignment.Center;
        //    titleCell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single, Color.White, 1);
        //    titleRow.Cells.Add(titleCell);

        //    headerTable.Rows.Add(titleRow);

        //    #endregion

        //    #region Table content

        //    GemBox.Document.Tables.Table table1 = new GemBox.Document.Tables.Table(document);
        //    table1.TableFormat.PreferredWidth = new TableWidth(100, TableWidthUnit.Percentage);
        //    table1.TableFormat.AutomaticallyResizeToFitContents = true;
        //    table1.TableFormat.Alignment = HorizontalAlignment.Center;
        //    table1.TableFormat.DefaultCellPadding = new Padding(8, 4, GemBox.Document.LengthUnit.Pixel);

        //    TableRow rowA1 = new TableRow(document);
        //    rowA1.RowFormat.AllowBreakAcrossPages = false;
        //    rowA1.Cells.Add(GetTitleTableCell(document, "MAKLUMAT PEMOHON", HorizontalAlignment.Left, Color.White));
        //    rowA1.Cells.Add(GetTableCell(document, "NO PERMOHONAN : " + dataTable.Rows[0]["Id"].ToString(), HorizontalAlignment.Right, Color.White));
        //    table1.Rows.Add(rowA1);

        //    //TableRow rowA2 = new TableRow(document);
        //    //rowA2.RowFormat.AllowBreakAcrossPages = false;
        //    //rowA2.Cells.Add(GetTableCell(document, "Nama Pegawai : ", dataTable.Rows[0]["Nama"].ToString(), HorizontalAlignment.Left, Color.Black));
        //    //rowA2.Cells.Add(GetTableCell(document, "Bahagian : ", dataTable.Rows[0]["Jabatan"].ToString(), HorizontalAlignment.Left, Color.Black));
        //    //table1.Rows.Add(rowA2);

        //    //TableRow rowA3 = new TableRow(document);
        //    //rowA3.RowFormat.AllowBreakAcrossPages = false;
        //    //rowA3.Cells.Add(GetTableCell(document, "No IC : ", dataTable.Rows[0]["NoIC"].ToString(), HorizontalAlignment.Left, Color.Black));
        //    //rowA3.Cells.Add(GetTableCell(document, "No Tel (Pej) : ", dataTable.Rows[0]["NoTelefonPej"].ToString(), HorizontalAlignment.Left, Color.Black));
        //    //table1.Rows.Add(rowA3);

        //    //TableRow rowA4 = new TableRow(document);
        //    //rowA4.RowFormat.AllowBreakAcrossPages = false;
        //    //rowA4.Cells.Add(GetTableCell(document, "Jawatan : ", dataTable.Rows[0]["Jawatan"].ToString(), HorizontalAlignment.Left, Color.Black));
        //    //rowA4.Cells.Add(GetTableCell(document, "No Tel (HP) : ", dataTable.Rows[0]["No_HP"].ToString(), HorizontalAlignment.Left, Color.Black));
        //    //table1.Rows.Add(rowA4);

        //    //TableRow rowA5 = new TableRow(document);
        //    //rowA5.RowFormat.AllowBreakAcrossPages = false;
        //    //rowA5.Cells.Add(GetTableCell(document, "Gred : ", dataTable.Rows[0]["GredHakiki"].ToString(), HorizontalAlignment.Left, Color.Black));
        //    //rowA5.Cells.Add(GetTableCell(document, "Alamat Emel : ", dataTable.Rows[0]["Emel_Rasmi"].ToString(), HorizontalAlignment.Left, Color.Black));
        //    //table1.Rows.Add(rowA5);

        //    //TableRow rowA6 = new TableRow(document);
        //    //rowA6.RowFormat.AllowBreakAcrossPages = false;
        //    //rowA6.Cells.Add(GetTableCell(document, "Nama Waris : ", dataTable.Rows[0]["NamaWaris"].ToString(), HorizontalAlignment.Left, Color.Black));
        //    //rowA6.Cells.Add(GetTableCell(document, "Alamat Emel Waris : ", dataTable.Rows[0]["EmelWaris"].ToString(), HorizontalAlignment.Left, Color.Black));
        //    //table1.Rows.Add(rowA6);

        //    //TableRow rowA7 = new TableRow(document);
        //    //rowA7.RowFormat.AllowBreakAcrossPages = false;
        //    //rowA7.Cells.Add(GetTableCell(document, "Hubungan Waris : ", dataTable.Rows[0]["HubunganWaris"].ToString(), HorizontalAlignment.Left, Color.Black));
        //    //rowA7.Cells.Add(GetTableCell(document, "No Telefon Waris : ", dataTable.Rows[0]["NoTelefonWaris"].ToString(), HorizontalAlignment.Left, Color.Black));
        //    //table1.Rows.Add(rowA7);

        //    //TableRow rowA8 = new TableRow(document);
        //    //rowA8.RowFormat.AllowBreakAcrossPages = false;
        //    //rowA8.Cells.Add(GetTableCell(document, "Alamat Keluarga : ", dataTable.Rows[0]["AlamatKeluarga"].ToString(), HorizontalAlignment.Left, Color.Black));
        //    //rowA8.Cells.Add(GetTableCell(document, String.Empty, HorizontalAlignment.Left, Color.Black));
        //    //table1.Rows.Add(rowA8);

        //    //TableRow rowB1 = new TableRow(document);
        //    //rowB1.RowFormat.AllowBreakAcrossPages = false;
        //    //rowB1.Cells.Add(GetTitleTableCell(document, "MAKLUMAT PERMOHONAN", HorizontalAlignment.Left, Color.White));
        //    //table1.Rows.Add(rowB1);

        //    //TableRow rowB2 = new TableRow(document);
        //    //rowB2.RowFormat.AllowBreakAcrossPages = false;
        //    //rowB2.Cells.Add(GetTableCell(document, "Tarikh Mula Cuti : ", ((DateTime)dataTable.Rows[0]["TkhMulaCuti"]).ToString(FORMAT_DATE), HorizontalAlignment.Left, Color.Black));
        //    //rowB2.Cells.Add(GetTableCell(document, "Tarikh Akhir Cuti : ", ((DateTime)dataTable.Rows[0]["TkhAkhirCuti"]).ToString(FORMAT_DATE), HorizontalAlignment.Left, Color.Black));
        //    //table1.Rows.Add(rowB2);

        //    //TableRow rowB3 = new TableRow(document);
        //    //rowB3.RowFormat.AllowBreakAcrossPages = false;
        //    //rowB3.Cells.Add(GetTableCell(document, "Tarikh Mula Cuti Keagamaan : ", ((DateTime)dataTable.Rows[0]["TarikhMulaCutiKeagamaan"]).ToString(FORMAT_DATE), HorizontalAlignment.Left, Color.Black));
        //    //rowB3.Cells.Add(GetTableCell(document, "Tarikh Akhir Cuti Keagamaan : ", ((DateTime)dataTable.Rows[0]["TarikhAkhirCutiKeagamaan"]).ToString(FORMAT_DATE), HorizontalAlignment.Left, Color.Black));
        //    //table1.Rows.Add(rowB3);

        //    //TableRow rowB4 = new TableRow(document);
        //    //rowB4.RowFormat.AllowBreakAcrossPages = false;
        //    //rowB4.Cells.Add(GetTableCell(document, "Jenis Permohonan : ", dataTable.Rows[0]["JenisPermohonan"].ToString(), HorizontalAlignment.Left, Color.Black));
        //    //rowB4.Cells.Add(GetTableCell(document, "Tarikh Kembali Bertugas : ", ((DateTime)dataTable.Rows[0]["TarikhMula_Tugas"]).ToString(FORMAT_DATE), HorizontalAlignment.Left, Color.Black));
        //    //table1.Rows.Add(rowB4);

        //    //TableRow rowB5 = new TableRow(document);
        //    //rowB5.RowFormat.AllowBreakAcrossPages = false;
        //    //rowB5.Cells.Add(GetTableCell(document, "Negara Dilawati : ", dataTable.Rows[0]["Negara_Dilawati"].ToString(), HorizontalAlignment.Left, Color.Black));
        //    //rowB5.Cells.Add(GetTableCell(document, String.Empty, HorizontalAlignment.Left, Color.Black));
        //    //table1.Rows.Add(rowB5);

        //    //TableRow rowB6 = new TableRow(document);
        //    //rowB6.RowFormat.AllowBreakAcrossPages = false;
        //    //rowB6.Cells.Add(GetTableCell(document, "Catatan : ", dataTable.Rows[0]["TujuanLawatan"].ToString(), HorizontalAlignment.Left, Color.Black));
        //    //rowB6.Cells.Add(GetTableCell(document, String.Empty, HorizontalAlignment.Left, Color.Black));
        //    //table1.Rows.Add(rowB6);

        //    GemBox.Document.Tables.Table table2 = new GemBox.Document.Tables.Table(document);
        //    table2.TableFormat.PreferredWidth = new TableWidth(100, TableWidthUnit.Percentage);
        //    table2.TableFormat.AutomaticallyResizeToFitContents = true;
        //    table2.TableFormat.Alignment = HorizontalAlignment.Center;
        //    table2.TableFormat.DefaultCellPadding = new Padding(8, 4, GemBox.Document.LengthUnit.Pixel);

        //    //TableRow rowC1 = new TableRow(document);
        //    //rowC1.RowFormat.AllowBreakAcrossPages = false;
        //    //rowC1.Cells.Add(GetTitleTableCell(document, "STATUS PERMOHONAN : " + dataTable.Rows[0]["StatusMohonCuti"].ToString().ToUpper(), HorizontalAlignment.Left, Color.White));
        //    //table2.Rows.Add(rowC1);

        //    //if (!String.IsNullOrWhiteSpace(dataTable.Rows[0]["CatatanSmkBPSM_Jab"].ToString()) || dataTable.Rows[0]["Organisasi_Grp_ID"].ToString() != (1).ToString())
        //    //{
        //    //    TableRow rowC2 = new TableRow(document);
        //    //    rowC2.RowFormat.AllowBreakAcrossPages = false;
        //    //    rowC2.Cells.Add(GetTableCell(document, "Catatan Semakan BPSM Jabatan : ", dataTable.Rows[0]["CatatanSmkBPSM_Jab"].ToString(), HorizontalAlignment.Left, Color.Black, 5));
        //    //    rowC2.Cells.Add(GetTableCell(document, "Tarikh Semakan BPSM Jabatan : ", String.IsNullOrWhiteSpace(dataTable.Rows[0]["TarikhSmkBPSM_Jab"].ToString()) ? String.Empty : ((DateTime)dataTable.Rows[0]["TarikhSmkBPSM_Jab"]).ToString("dd-MMM-yyyy"), HorizontalAlignment.Left, Color.Black, 5));
        //    //    table2.Rows.Add(rowC2);
        //    //}

        //    //if (!String.IsNullOrWhiteSpace(dataTable.Rows[0]["CatatanSmkBPSM"].ToString()) || dataTable.Rows[0]["Organisasi_Grp_ID"].ToString() == (1).ToString())
        //    //{
        //    //    TableRow rowC3 = new TableRow(document);
        //    //    rowC3.RowFormat.AllowBreakAcrossPages = false;
        //    //    rowC3.Cells.Add(GetTableCell(document, "Catatan Semakan BPSM Kementerian : ", dataTable.Rows[0]["CatatanSmkBPSM"].ToString(), HorizontalAlignment.Left, Color.Black, 5));
        //    //    rowC3.Cells.Add(GetTableCell(document, "Tarikh Semakan BPSM Kementerian : ", String.IsNullOrWhiteSpace(dataTable.Rows[0]["TarikhSmkBPSM"].ToString()) ? String.Empty : ((DateTime)dataTable.Rows[0]["TarikhSmkBPSM"]).ToString("dd-MMM-yyyy"), HorizontalAlignment.Left, Color.Black, 5));
        //    //    table2.Rows.Add(rowC3);
        //    //}

        //    //if (!String.IsNullOrWhiteSpace(dataTable.Rows[0]["CatatanPeraku_KPSU_PSM"].ToString()))
        //    //{
        //    //    TableRow rowC4 = new TableRow(document);
        //    //    rowC4.RowFormat.AllowBreakAcrossPages = false;
        //    //    rowC4.Cells.Add(GetTableCell(document, "Catatan Perakuan KPSU (BPSM) : ", dataTable.Rows[0]["CatatanPeraku_KPSU_PSM"].ToString(), HorizontalAlignment.Left, Color.Black, 5));
        //    //    rowC4.Cells.Add(GetTableCell(document, "Tarikh Perakuan KPSU (BPSM) : ", String.IsNullOrWhiteSpace(dataTable.Rows[0]["TarikhPeraku_KPSU_PSM"].ToString()) ? String.Empty : ((DateTime)dataTable.Rows[0]["TarikhPeraku_KPSU_PSM"]).ToString("dd-MMM-yyyy"), HorizontalAlignment.Left, Color.Black, 5));
        //    //    table2.Rows.Add(rowC4);
        //    //}

        //    //if (!String.IsNullOrWhiteSpace(dataTable.Rows[0]["CatatanPengesah_SUB_PSM"].ToString()))
        //    //{
        //    //    TableRow rowC5 = new TableRow(document);
        //    //    rowC5.RowFormat.AllowBreakAcrossPages = false;
        //    //    rowC5.Cells.Add(GetTableCell(document, "Catatan Pengesahan SUB (PSM) : ", dataTable.Rows[0]["CatatanPengesah_SUB_PSM"].ToString(), HorizontalAlignment.Left, Color.Black, 5));
        //    //    rowC5.Cells.Add(GetTableCell(document, "Tarikh Pengesahan SUB (PSM) : ", String.IsNullOrWhiteSpace(dataTable.Rows[0]["TarikhPengesah_SUB_PSM"].ToString()) ? String.Empty : ((DateTime)dataTable.Rows[0]["TarikhPengesah_SUB_PSM"]).ToString("dd-MMM-yyyy"), HorizontalAlignment.Left, Color.Black, 5));
        //    //    table2.Rows.Add(rowC5);
        //    //}

        //    //if (!String.IsNullOrWhiteSpace(dataTable.Rows[0]["CatatanPengesah_SUBK"].ToString()))
        //    //{
        //    //    TableRow rowC6 = new TableRow(document);
        //    //    rowC6.RowFormat.AllowBreakAcrossPages = false;
        //    //    rowC6.Cells.Add(GetTableCell(document, "Catatan Pengesahan SUBK : ", dataTable.Rows[0]["CatatanPengesah_SUBK"].ToString(), HorizontalAlignment.Left, Color.Black, 5));
        //    //    rowC6.Cells.Add(GetTableCell(document, "Tarikh Pengesahan SUBK : ", String.IsNullOrWhiteSpace(dataTable.Rows[0]["TarikhPengesah_SUBK"].ToString()) ? String.Empty : ((DateTime)dataTable.Rows[0]["TarikhPengesah_SUBK"]).ToString("dd-MMM-yyyy"), HorizontalAlignment.Left, Color.Black, 5));
        //    //    table2.Rows.Add(rowC6);
        //    //}

        //    //if (!String.IsNullOrWhiteSpace(dataTable.Rows[0]["CatatanKetua_Jabatan"].ToString()))
        //    //{
        //    //    TableRow rowC8 = new TableRow(document);
        //    //    rowC8.RowFormat.AllowBreakAcrossPages = false;
        //    //    rowC8.Cells.Add(GetTableCell(document, "Catatan Kelulusan Ketua Jabatan : ", dataTable.Rows[0]["CatatanKetua_Jabatan"].ToString(), HorizontalAlignment.Left, Color.Black, 5));
        //    //    rowC8.Cells.Add(GetTableCell(document, "Tarikh Kelulusan Ketua Jabatan : ", String.IsNullOrWhiteSpace(dataTable.Rows[0]["TarikhKetua_Jabatan"].ToString()) ? String.Empty : ((DateTime)dataTable.Rows[0]["TarikhKetua_Jabatan"]).ToString("dd-MMM-yyyy"), HorizontalAlignment.Left, Color.Black, 5));
        //    //    table2.Rows.Add(rowC8);
        //    //}

        //    //if (!String.IsNullOrWhiteSpace(dataTable.Rows[0]["CatatanKelulusan_KSU"].ToString()))
        //    //{
        //    //    TableRow rowC7 = new TableRow(document);
        //    //    rowC7.RowFormat.AllowBreakAcrossPages = false;
        //    //    rowC7.Cells.Add(GetTableCell(document, "Catatan Kelulusan KSU : ", dataTable.Rows[0]["CatatanKelulusan_KSU"].ToString(), HorizontalAlignment.Left, Color.Black, 5));
        //    //    rowC7.Cells.Add(GetTableCell(document, "Tarikh Kelulusan KSU : ", !String.IsNullOrWhiteSpace(dataTable.Rows[0]["CatatanKelulusan_KSU"].ToString()) ? ((DateTime)dataTable.Rows[0]["TarikhKelulusanKSU_KetuaJab"]).ToString(FORMAT_DATE) : String.Empty, HorizontalAlignment.Left, Color.Black, 5));
        //    //    table2.Rows.Add(rowC7);
        //    //}

        //    #endregion

        //    #region Section

        //    Section section = new Section(document, headerTable, table1, table2);

        //    double defaultMargin = GemBox.Document.LengthUnitConverter.Convert(0.45, GemBox.Document.LengthUnit.Inch, GemBox.Document.LengthUnit.Point);
        //    double bottomMargin = GemBox.Document.LengthUnitConverter.Convert(0.25, GemBox.Document.LengthUnit.Inch, GemBox.Document.LengthUnit.Point);

        //    section.PageSetup.PaperType = GemBox.Document.PaperType.A4;

        //    section.PageSetup.Orientation = Orientation.Portrait;
        //    section.PageSetup.PageMargins = new PageMargins() { Bottom = bottomMargin, Left = defaultMargin, Right = defaultMargin, Top = defaultMargin };
        //    section.PageSetup.PageMargins.Footer = GemBox.Document.LengthUnitConverter.Convert(0.3, GemBox.Document.LengthUnit.Centimeter, GemBox.Document.LengthUnit.Point);

        //    document.Sections.Add(section);

        //    #endregion

        //    document.Save(filePath, GemBox.Document.SaveOptions.PdfDefault);
        //}

        //#endregion

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