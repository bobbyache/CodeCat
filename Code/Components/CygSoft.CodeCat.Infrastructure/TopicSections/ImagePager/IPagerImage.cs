using System.Drawing;

namespace CygSoft.CodeCat.Infrastructure.TopicSections
{
    public interface IPagerImage : ITopicSection
    {
        bool IsModified { get; }
        string ModifyFilePath { get; }
        string DisplayFilePath { get; }

        Bitmap GetDisplayImage();
        void SetImage(string fromFile);
        void SetImage(Image image);
    }
}
