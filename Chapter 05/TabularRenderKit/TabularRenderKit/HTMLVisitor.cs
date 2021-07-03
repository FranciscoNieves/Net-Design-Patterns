using System;
using System.IO;
using System.Text;

namespace TabularRenderKit
{
    public class HTMLVisitor : IDocumentVisitor
    {
        private string file_name = null;
        private StringBuilder document = null;

        public HTMLVisitor(string filename)
        {
            file_name = filename;
        }

        public void Visit(TDocument doc)
        {
            document = new StringBuilder("<HTML><head>\n");
            document.Append("<title>");

            if (doc.Title != null)
            {
                document.Append(doc.Title);
            }

            document.Append("</title></head>\n\n<body ");

            string color = doc.BackgroundColor;

            if (color != null)
            {
                document.Append("BGCOLOR =\"" + color + "\"");
            }

            string textColor = doc.TextColor;

            if (textColor != null)
            {
                document.Append("TEXT =\"" + textColor + "\"");
            }

            string vlink = doc.Vlink;

            if (vlink != null)
            {
                document.Append("VLINK =\"" + vlink + "\"");
            }

            string linkColor = doc.LinkColor;

            if (linkColor != null)
            {
                document.Append("VLINK =\"" + vlink + "\"");
            }

            string alink = doc.Alink;

            if (alink != null)
            {
                document.Append("ALINK =\"" + alink + "\"");
            }

            document.Append(">\n");

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

            document.Append("\n</body>\n</HTML>\n");

            string html_content = document.ToString();

            FileStream fs = new FileStream(file_name, FileMode.Create);
            StreamWriter st = new StreamWriter(fs);
            st.Write(html_content);
            st.Close();
        }

        public void Visit(TDocumentTable table)
        {
            StringBuilder CHTML = new StringBuilder("<TABLE ");

            string bgColor = table.BackgroundColor;

            if (bgColor != null)
            {
                CHTML.Append(" bgcolor=" + bgColor + " ");
            }

            int _width = table.Width;

            if (_width > 0)
            {
                CHTML.Append(" WIDTH=" + _width);

                if (table.PercentageWidth)
                {
                    CHTML.Append("% ");
                }
                else
                {
                    CHTML.Append(" ");
                }
            }

            if (table.Border > -1)
            {
                CHTML.Append(" BORDER=" + table.Border);
            }

            if (table.CellPadding > -1)
            {
                CHTML.Append(" CELLSPADING=" + table.CellPadding);
            }

            string color = table.BackgroundColor;

            if (color != null)
            {
                CHTML.Append(" BGCOLOR=\"" + color + "\"");
            }

            CHTML.Append(">\n");

            string _caption = table.Caption;

            if (_caption != null)
            {
                CHTML.Append("\n<CAPTION>" + _caption + "</CAPTION>\n");
            }

            document.Append(CHTML.ToString());

            for (int i = 0; i < table.DocumentElements.Count; i++)
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

            document.Append("\n</TABLE>");
        }

        public void Visit(TDocumentTableRow row)
        {
            StringBuilder CHTML = new StringBuilder("<TR>");
            document.Append("<TR>");

            for (int i = 0; i < row.DocumentElements.Count; i++)
            {
                try
                {
                    row.DocumentElements[i].Accept(this);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                }
            }

            document.Append("</TR>\n");
        }

        public void Visit(TDocumentTableCell cell)
        {
            StringBuilder CHTML = new StringBuilder("");
            string tag = null;
            string valign = null;
            string align = null;

            switch (cell.Vertical)
            {
            case Alignment.TOP:
                {
                    valign = "TOP";

                    break;
                }
            case Alignment.MIDDLE:
                {
                    valign = "MIDDLE";

                    break;
                }
            default:
                {
                    valign = "BOTTOM";

                    break;
                }
            }

            switch (cell.Horizontal)
            {
            case Alignment.LEFT:
                {
                    align = "LEFT";

                    break;
                }
            case Alignment.CENTER:
                {
                    align = "CENTER";

                    break;
                }
            default:
                {
                    align = "RIGHT";

                    break;
                }
            }

            if (cell.Type == Alignment.DATA)
            {
                tag = "ID";
            }
            else
            {
                tag = "TH";
            }

            CHTML.Append("<" + tag + " VALIGN=" + valign + " ALIGN=" + align);
            CHTML.Append(" COLSPAN=" + cell.ColumnSpan + ">");

            document.Append(CHTML.ToString());

            for (int i = 0; i < cell.DocumentElements.Count; i++)
            {
                try
                {
                    cell.DocumentElements[i].Accept(this);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                }
            }

            document.Append("</" + tag + ">");
        }

        public void Visit(TDocumentText txt)
        {
            StringBuilder CHTML = new StringBuilder(txt.Text);

            if (txt.Bold)
            {
                CHTML.Insert(0, "<B>");
                CHTML.Append("</B>");
            }

            if (txt.Italic)
            {
                CHTML.Insert(0, "<I>");
                CHTML.Append("</I>");
            }

            if (txt.Italic)
            {
                CHTML.Insert(0, "<U>");
                CHTML.Append("</U>");
            }

            if (txt.Preformatted)
            {
                CHTML.Insert(0, "<PRE>");
                CHTML.Append("</PRE>");
            }

            if (txt.Center)
            {
                CHTML.Insert(0, "<CENTER>");
                CHTML.Append("</CENTER>");
            }

            if (txt.Font)
            {
                if (txt.Color != null)
                {
                    string str = "<font color=";
                    str += txt.Color;
                    str += ">";

                    CHTML.Insert(0, str);
                    CHTML.Append("</font>");
                }
            }

            document.Append(CHTML.ToString());
        }
    }
}
