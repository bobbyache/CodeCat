﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IRichTextDocument : IVersionableFile, IPositionedItem
    {
        event EventHandler RequestSaveRtf;

        string Title { get; set; }
        string DocumentType { get; set; }
        string Description { get; set; }
    }
}