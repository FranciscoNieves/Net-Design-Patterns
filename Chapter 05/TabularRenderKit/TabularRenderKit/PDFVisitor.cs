using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace TabularRenderKit
{
    public class PDFVisitor : IDocumentVisitor
    {
        private string file_name = null;
        private PdfWriter writer = null;
        private Document document = null;
        private FileStream fs = null;
        private PdfPTable table_temp;
        private TDocumentTableRow _rows = null;
        private int column_count;

        public PDFVisitor(string filename)
        {
            file_name = filename;
            fs = new FileStream(file_name, FileMode.Create);
            document = new Document(PageSize.A4, 25, 25, 30, 30);
            writer = PdfWriter.GetInstance(document, fs);
        }

        public void Visit(TDocument doc)
        {
            document.AddAuthor("Author");
            document.AddCreator("iTextSharp library");
            document.AddKeywords("Design Patterns Architecture");
            document.AddSubject("Book on .Net Design Patterns");
            document.Open();
            column_count = doc.ColumnCount;
            document.AddTitle(doc.Title);

            for (int i = 0; i < doc.DocumentElements.Count; i++)
            {
                try
                {
                    doc.DocumentElements[i].Accept(this);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                }
            }

            document.Add(table_temp);
            document.Close();
            writer.Close();
            fs.Close();
        }

        public void Visit(TDocumentTable table)
        {
            table_temp = new PdfPTable(column_count);

            PdfPCell cell = new PdfPCell(new Phrase("Header spanning 3 columns"));
            cell.Colspan = column_count;
            cell.HorizontalAlignment = 1; // 0 = Left, 1 = Center, 2 = Right
            table_temp.AddCell(cell);

            for (int i = 0; i < table.RowCount; i++)
            {
                try
                {
                    table.DocumentElements[i].Accept(this);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                }
            }
        }

        public void Visit(TDocumentTableRow row)
        {
            for (int i = 0; i < row.DocumentElements.Count; i++)
            {
                row.DocumentElements[i].Accept(this);
            }
        }

        public void Visit(TDocumentTableCell cell)
        {
            for (int i = 0; i < cell.DocumentElements.Count; i++)
            {
                cell.DocumentElements[i].Accept(this);
            }
        }

        public void Visit(TDocumentText txt)
        {
            table_temp.AddCell(txt.Text);
        }
    }
}
