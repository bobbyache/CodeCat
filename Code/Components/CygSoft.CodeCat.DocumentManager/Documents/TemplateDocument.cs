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
//    public class TemplateDocument : BaseDocumentFile
//    {
//        public string Text { get; set; }
//        public string Syntax { get; set; }

//        // Only create these documents internally.
//        internal TemplateDocument(string id, string title, string syntax, string description = null)
//            : base(id, "tpl", title, description)
//        {
//            this.Syntax = syntax;
//        }

//        internal TemplateDocument(string id, string title, string syntax, int ordinal, string description = null)
//            : base(id, "tpl", ordinal, title, description)
//        {
//            this.Syntax = syntax; 
//        }

//        protected override IFileVersion NewVersion(DateTime timeStamp, string description)
//        {
//            return new TemplateDocumentVersion(this.FilePath, timeStamp, description);
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
