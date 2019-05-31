using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManager.UnitTests
{
    public class HelperFuncs
    {
        public static string GetFileFolder(string filePath)
        {
            return Path.GetDirectoryName(filePath);
        }

        public static string GetIdFromFileName(string fileName)
        {
            Guid guid;
            string fileTitle = Path.GetFileNameWithoutExtension(fileName);
            if (Guid.TryParse(fileTitle, out guid))
            {
                return guid.ToString();
            }
            else
                return string.Empty;
        }

        public static bool FileNameIsGuid(string fileName)
        {
            Guid guid;
            string fileTitle = Path.GetFileNameWithoutExtension(fileName);
            return Guid.TryParse(fileTitle, out guid);
        }

        public static bool IdIsGuid(string id)
        {
            Guid guid;
            return Guid.TryParse(id, out guid);
        }
    }
}
