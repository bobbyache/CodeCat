﻿using System;
using System.Drawing;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public interface ITopicSectionBaseControl
    {
        event EventHandler Modified;

        string Id { get; }
        string Title { get; }
        string Text { get; }
        int ImageKey { get; }
        Icon ImageIcon { get; }
        Image IconImage { get; }
        bool IsModified { get; }
        bool FileExists { get; }

        void Revert();
    }
}
