namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IPdfDocument : IFile, IPositionedItem
    {
        string Title { get; set; }
        string DocumentType { get; set; }
        string Description { get; set; }
    }
}
