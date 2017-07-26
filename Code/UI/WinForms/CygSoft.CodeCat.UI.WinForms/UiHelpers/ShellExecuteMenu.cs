using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CygSoft.CodeCat.UI.WinForms.UiHelpers
{
    public static partial class Gui
    {
        public static class ShellExecuteMenu
        {
            public static void LoadMenu(ToolStripMenuItem rootMenu)
            {
                ShellExecuteFile shellExecuteFile = new ShellExecuteFile(ConfigSettings.ShellExecuteMenu);

                foreach (ShellFile file in shellExecuteFile.GetRootCategories())
                {
                    ToolStripMenuItem item = new ToolStripMenuItem();
                    item.Tag = file;
                    item.Text = file.Title;
                    item.Click += (s, e) => {
                        ShellFile f = (s as ToolStripMenuItem).Tag as ShellFile;

                        if (f != null)
                        {
                            if (File.Exists(f.Path))
                            {
                                Process.Start(f.Path);
                            }
                        }
                    };
                    rootMenu.DropDownItems.Add(item);
                }
            }

            private static void Open(ToolStripMenuItem application)
            {
                ShellFile file = application.Tag as ShellFile;

                if (file != null)
                {
                    if (File.Exists(file.Path))
                    {
                        Process.Start(file.Path);
                    }
                }
            }
        }
    }

    public class ShellFile
    {
        public ShellFile(string id, string title, string path)
        {
            this.Id = id;
            this.Title = title;
            this.Path = path;
        }

        public string Id { get; }
        public string Title { get; }
        public string Path { get; }
    }

    public class ShellExecuteFile
    {
        public string FileName { get; set; }

        public ShellExecuteFile(string fileName)
        {
            this.FileName = fileName;
        }

        public List<ShellFile> GetRootCategories()
        {
            List<ShellFile>  shellFiles = new List<ShellFile>();

            XElement rootElement;
            if (FetchRootElement(out rootElement))
            {
                if (rootElement.Elements("Application").Any())
                {
                    shellFiles = (from el in rootElement.Elements("Application")
                                 select new ShellFile((string)el.Attribute("ID"), (string)el.Attribute("Title"), (string)el.Attribute("Path"))
                                 {
                                  }).ToList();
                }
            }

            return shellFiles;
        }

        private bool FetchRootElement(out XElement rootElement)
        {
            rootElement = null;

            if (File.Exists(this.FileName))
            {
                XDocument doc = XDocument.Load(this.FileName);
                rootElement = doc.Root;
                return true;
            }
            return false;
        }
    }

}
