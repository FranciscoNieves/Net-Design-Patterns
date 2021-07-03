namespace TabularRenderKit
{
    public class TDocumentTableCell : TDocumentElement
    {
        public int ColumnSpan { get; set; }
        public Alignment Horizontal { get; set; }
        public Alignment Vertical { get; set; }
        public Alignment Type { get; set; }

        public TDocumentTableCell()
        {
            ColumnSpan = 1;
            Horizontal = Alignment.LEFT;
            Vertical = Alignment.MIDDLE;
            Type = Alignment.DATA;
        }

        public TDocumentTableCell(Alignment type)
        {
            Type = type;
        }

        public override void Accept(IDocumentVisitor doc_vis)
        {
            doc_vis.Visit(this);
        }
    }
}
