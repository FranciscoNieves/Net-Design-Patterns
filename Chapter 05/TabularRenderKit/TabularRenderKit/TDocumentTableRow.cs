namespace TabularRenderKit
{
    public class TDocumentTableRow : TDocumentElement
    {
        public TDocumentTableRow()
        {
        }

        public override void Accept(IDocumentVisitor doc_vis)
        {
            doc_vis.Visit(this);
        }
    }
}
