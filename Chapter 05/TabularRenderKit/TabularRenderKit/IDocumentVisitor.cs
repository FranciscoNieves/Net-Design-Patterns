namespace TabularRenderKit
{
    public interface IDocumentVisitor
    {
        void Visit(TDocument doc);
        void Visit(TDocumentTable table);
        void Visit(TDocumentTableRow row);
        void Visit(TDocumentTableCell cell);
        void Visit(TDocumentText txt);
    }
}
