﻿using CygSoft.CodeCat.DocumentManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Stubs.DocumentManager
{
    public class StubSnapshotFile : BaseSnapshotFile
    {
        public StubSnapshotFile(string filePath, DateTime timeStamp, string description, string text)
            : base(filePath, timeStamp, description, text) 
        {

        }
    }
}
