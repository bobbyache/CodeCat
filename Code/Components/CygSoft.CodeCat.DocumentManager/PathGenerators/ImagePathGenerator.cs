﻿using CygSoft.CodeCat.DocumentManager.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.PathGenerators
{
    public class ImagePathGenerator : BaseFilePathGenerator
    {
        private string folder;
        private string extension;
        private string id;

        public ImagePathGenerator(string folder, string extension)
        {
            this.id = Guid.NewGuid().ToString();
            this.folder = folder;
            this.extension = extension;
        }

        public ImagePathGenerator(string folder, string extension, string id)
        {
            this.id = id;
            this.folder = folder;
            this.extension = extension;
        }

        public override string FileExtension
        {
            get { return base.CleanExtension(this.extension); }
        }

        public override string FileName
        {
            get { return id + "." + base.CleanExtension(extension); }
        }

        public override string FilePath
        {
            get { return Path.Combine(folder, id + "." + base.CleanExtension(extension)); }
        }

        public override string Id
        {
            get { return id; }
        }

        public string ModifiedFileName { get { return "TEMP_" + this.Id + "." + this.FileExtension; } }
        public string ModifiedFilePath { get { return Path.Combine(this.FolderPath, this.ModifiedFileName); } }
    }
}
