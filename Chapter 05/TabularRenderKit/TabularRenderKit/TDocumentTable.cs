namespace TabularRenderKit
{
    public class TDocumentTable : TDocumentElement
    {
        public string Caption { get; set; }
        public int Width { get; set; }
        public int Border { get; set; }
        public int CellSpacing { get; set; }
        public int CellPadding { get; set; }
        public bool PercentageWidth { get; set; }
        public string BgColor { get; set; }

        public int RowCount
        {
            get
            {
                return this.DocumentElements.Count;
            }
        }

        public TDocumentTable()
        {

        }

        public override void Accept(IDocumentVisitor doc_vis)
        {
            doc_vis.Visit(this);
        }
    }
}
