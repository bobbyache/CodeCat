﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CygSoft.CodeCat.Domain
{
    internal class Project
    {
        public string FilePath { get; private set; }
        public string FileTitle { get { return Path.GetFileName(this.FilePath); } }
        public string FolderPath { get { return Path.GetDirectoryName(this.FilePath); } }

        public int CurrentVersion { get; private set; }
        public string ProjectFileExtension { get { return "codecat"; } }

        public void Open(string filePath, int currentVersion)
        {
            this.FilePath = filePath;
            this.CurrentVersion = currentVersion;
        }


        public void Create(string filePath, int currentVersion)
        {
            this.FilePath = filePath;
            this.CurrentVersion = currentVersion;
            CreateNew(filePath);
        }

        private void CreateNew(string filePath)
        {
            XmlDocument document = new XmlDocument();

            XmlDeclaration xmlDeclaration = document.CreateXmlDeclaration("1.0", "utf-8", null);
            XmlElement root = document.CreateElement("CodeCat_Project");

            root.Attributes.Append(CreateVersionAttributes(document));

            document.InsertBefore(xmlDeclaration, document.DocumentElement);
            document.AppendChild(root);
            document.Save(filePath);
        }

        private XmlAttribute CreateVersionAttributes(XmlDocument document)
        {
            XmlAttribute version = document.CreateAttribute("Version");
            version.Value = this.CurrentVersion.ToString();
            return version;
        }
    }
}
