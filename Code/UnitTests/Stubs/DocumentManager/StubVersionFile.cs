using CygSoft.CodeCat.DocumentManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Stubs.DocumentManager
{
    public class StubVersionFile : BaseVersionFile
    {
        public StubVersionFile(string filePath, DateTime timeStamp, string description)
            : base(filePath, timeStamp, description) 
        {

        }

        protected override void CreateFile()
        {
            throw new NotImplementedException();
        }

        protected override void OpenFile()
        {
            //throw new NotImplementedException();
        }

        protected override void SaveFile()
        {
            throw new NotImplementedException();
        }
    }
}
