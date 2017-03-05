namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IPdfDocument : IVersionableFile, IPositionedItem
    {
        string Title { get; set; }
        string DocumentType { get; set; }
        string Description { get; set; }

        //void Import(string filePath);
    }
}
