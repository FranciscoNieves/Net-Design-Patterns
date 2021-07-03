using System;

namespace TabularRenderKit
{
    public class TDocumentText : TDocumentElement
    {
        public string Text { get; set; }
        public bool Bold { get; set; }
        public bool Italic { get; set; }
        public bool Underline { get; set; }
        public bool Center { get; set; }
        public bool Preformatted { get; set; }
        public string Color { get; set; }
        public bool Font { get; set; }

        public TDocumentText()
        {

        }

        public TDocumentText(string value = null)
        {
            if (value != null)
            {
                Text = value;
            }
        }

        public override void Accept(IDocumentVisitor doc_vis)
        {
            throw new NotImplementedException();
        }
    }
}
