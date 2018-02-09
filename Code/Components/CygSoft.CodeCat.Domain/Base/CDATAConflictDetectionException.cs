using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.Base
{
    public class CDATAConflictDetectionException : Exception
    {
        public CDATAConflictDetectionException()
        {
        }

        public override string Message
        {
            get
            {
                return "CodeCat has detected a conflicting CDATA tag fragment embedded in the text that will corrupt the file. The file cannot be saved unless the text is modified to remove the problem.";
            }
        }
    }
}
