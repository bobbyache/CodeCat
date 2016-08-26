using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.UI.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.Documents
{
    public class DocumentTabManager
    {
        public event EventHandler<DocumentTabEventArgs> BeforeDeleteTab;
        public event EventHandler<DocumentTabEventArgs> AfterAddTab;
        
        private TabControl tabControl;
        private Dictionary<string, TabPage> tabPageDictionary = new Dictionary<string, TabPage>();

        public DocumentTabManager(TabControl tabControl)
        {
            this.tabControl = tabControl;
        }

        public TabPage AddTab(IDocument document, IDocumentItemControl tabUserControl, bool visible = true, bool select = false)
        {
            TabPage tabPage = new TabPage(document.Title);
            tabPage.Name = document.Id;
            tabPage.ImageIndex = tabUserControl.ImageKey;
            tabPage.ImageIndex = tabUserControl.ImageKey;

            tabUserControl.ParentTab = tabPage;
            ((UserControl)tabUserControl).Dock = DockStyle.Fill;
            tabPage.Controls.Add(tabUserControl as UserControl);

            tabPageDictionary.Add(document.Id, tabPage);

            if (visible)
            {
                tabControl.TabPages.Add(tabPage);
                if (select)
                    tabControl.SelectedTab = tabPage;
            }

            return tabPage;
        }

        public IDocumentItemControl FindDocumentControl(string Id)
        {
            return tabPageDictionary[Id].Controls[0] as IDocumentItemControl;
        }

        public void Display(string id, bool visible)
        {
            TabPage tabPage = tabPageDictionary[id];

            if (visible && !tabControl.Contains(tabPage))
            {
                tabControl.TabPages.Add(tabPage);
                tabControl.SelectedTab = tabPage;
            }
            else if (visible && tabControl.Contains(tabPage))
                tabControl.SelectedTab = tabPage;
            else
            {
                if (tabControl.Contains(tabPage))
                    tabControl.TabPages.Remove(tabPage);
            }
        }

        public void SendToBack(string id, bool visible)
        {
            TabPage tabPage = tabPageDictionary[id];

            if (tabControl.Contains(tabPage))
                tabControl.TabPages.Remove(tabPage);

            if (visible)
            {
                tabControl.TabPages.Add(tabPage);
            }
            //tabControl.SelectedTab = tabPage;
        }

        public string SelectedTabId
        {
            get
            {
                return tabControl.SelectedTab.Name;
            }
        }

        public TabPage SelectedTab { get { return tabControl.SelectedTab; } }

        public bool HasTabs { get { return this.tabControl.TabPages.Count > 0; } }

        public void Clear()
        {
            foreach (TabPage tabPage in tabControl.TabPages)
            {
                if (BeforeDeleteTab != null)
                    BeforeDeleteTab(this, new DocumentTabEventArgs(tabPage, tabPage.Controls[0] as UserControl));
            }

            tabPageDictionary.Clear();
            tabControl.TabPages.Clear();
        }

        public void Remove(string id)
        {
            TabPage tabPage = tabPageDictionary[id];

            if (BeforeDeleteTab != null)
                BeforeDeleteTab(this, new DocumentTabEventArgs(tabPage, tabPage.Controls[0] as UserControl));

            tabPageDictionary.Remove(id);
            tabControl.TabPages.Remove(tabPage);
        }
    }
}
