namespace TabularRenderKit
{
    public class TDocument : TDocumentElement
    {
        public string Title { get; set; }
        public string BackgroundImage { get; set; }
        public string TextColor { get; set; }
        public string LinkColor { get; set; }
        public string Vlink { get; set; }
        public string Alink { get; set; }
        public int ColumnCount { get; set; }

        public TDocument(int count = 1)
        {
            ColumnCount = count;
            Title = "Default Title";
        }

        public TDocument(string value)
        {
            if (value != null)
            {
                Title = value;
            }
        }

        public override void Accept(IDocumentVisitor doc_vis)
        {
            doc_vis.Visit(this);
        }
    }
}
