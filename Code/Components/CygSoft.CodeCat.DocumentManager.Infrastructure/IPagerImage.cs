﻿using System.Drawing;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IPagerImage : ITopicSection, IPositionedItem
    {
        bool IsModified { get; }
        string ModifyFilePath { get; }
        string DisplayFilePath { get; }

        Bitmap GetDisplayImage();
        void SetImage(string fromFile);
        void SetImage(Image image);
    }
}