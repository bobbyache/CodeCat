namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IImageDocument :  IVersionableFile, IPositionedItem
    {
        string Title { get; set; }
        string DocumentType { get; set; }
        string Description { get; set; }
    }
}
