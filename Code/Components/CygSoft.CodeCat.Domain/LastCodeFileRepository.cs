﻿using System.Collections.Generic;
using System.IO;

namespace CygSoft.CodeCat.Domain
{
    internal class LastCodeFileRepository
    {
        private string filePath = "";

        public LastCodeFileRepository(string filePath)
        {
            this.filePath = filePath;
        }

        public string[] Load()
        {
            List<string> idList = new List<string>();

            if (File.Exists(filePath))
            {
                using (StreamReader streamReader = File.OpenText(filePath))
                {
                    string input = null;
                    while ((input = streamReader.ReadLine()) != null)
                    {
                        idList.Add(input.Trim());
                    }
                }
            }
            return idList.ToArray();
        }

        public void Save(string[] ids)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                foreach (string id in ids)
                {
                    streamWriter.WriteLine(id);
                    streamWriter.Flush();
                }
            }
        }
    }
}
