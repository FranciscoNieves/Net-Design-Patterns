using System.Collections.Generic;

namespace TabularRenderKit
{
    public enum Alignment { LEFT, CENTER, RIGHT, TOP, MIDDLE, BOTTOM, HEADING, DATA };
    public enum Heading { H1, H2, H3, H4, H5, H6 };
    public enum ListType { UL, OL, MENU, DIR };

    public abstract class TDocumentElement
    {
        public List<TDocumentElement> DocumentElements { get; set; }
        public string BackgroundColor { get; set; }
        public Alignment Align { get; set; }
        public abstract void Accept(IDocumentVisitor doc_vis);

        public TDocumentElement()
        {
            DocumentElements = new List<TDocumentElement>();
            Align = Alignment.LEFT;
            BackgroundColor = "0xFF000000L";
        }

        public void AddObject(TDocumentElement value)
        {
            if (value != null)
            {
                DocumentElements.Add(value);
            }
        }

        public bool RemoveObject(TDocumentElement value)
        {
            if (value != null)
            {
                DocumentElements.Remove(value);

                return true;
            }

            return false;
        }
    }
}
