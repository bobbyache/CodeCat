//using CygSoft.CodeCat.DocumentManager.Base;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CygSoft.CodeCat.DocumentManager.PathGenerators
//{
//    public class FileGroupPathGenerator : BaseFilePathGenerator
//    {
//        private string folder;
//        private string extension;
//        private string fileName;

//        public FileGroupPathGenerator(string folder, string fileName)
//        {
//            this.fileName = fileName;
//            this.folder = folder;
//            this.extension = Path.GetExtension(fileName);
//        }

//        public override string FileExtension
//        {
//            get { return base.CleanExtension(this.extension); }
//        }

//        public override string FileName
//        {
//            get { return fileName; }
//        }

//        public override string FilePath
//        {
//            get { return Path.Combine(folder, fileName); }
//        }

//        public override string Id
//        {
//            get { return fileName; }
//        }
//    }
//}
