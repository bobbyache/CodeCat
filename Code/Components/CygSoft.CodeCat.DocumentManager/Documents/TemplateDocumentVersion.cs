//using CygSoft.CodeCat.DocumentManager.Base;
//using CygSoft.CodeCat.DocumentManager.Infrastructure;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CygSoft.CodeCat.DocumentManager.Documents
//{
//    public class TemplateDocumentVersion : BaseVersionFile
//    {
//        public string Text { get; set; }

//        public TemplateDocumentVersion(string filePath, DateTime timeStamp, string description)
//            : base(filePath, timeStamp, description) 
//        {

//        }

//        protected override void CreateFile()
//        {
//            File.WriteAllText(this.FileName, this.Text);
//        }

//        protected override void OpenFile()
//        {
//            this.Text = File.ReadAllText(this.FilePath);
//        }

//        protected override void SaveFile()
//        {
//            File.WriteAllText(this.FilePath, this.Text);
//        }
//    }
//}
