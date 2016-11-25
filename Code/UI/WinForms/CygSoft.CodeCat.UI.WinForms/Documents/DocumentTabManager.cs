﻿using CygSoft.CodeCat.DocumentManager.Infrastructure;
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
        
        private TabControl tabControl;
        private ToolStripDropDownButton tabMenuButton;
        private Dictionary<string, TabPage> tabPageDictionary = new Dictionary<string, TabPage>();

        public string SelectedTabId { get { return tabControl.SelectedTab.Name; } }
        public TabPage SelectedTab { get { return tabControl.SelectedTab; } }
        public bool HasTabs { get { return this.tabControl.TabPages.Count > 0; } }

        public DocumentTabManager(TabControl tabControl, ToolStripDropDownButton tabMenuButton)
        {
            this.tabControl = tabControl;
            this.tabMenuButton = tabMenuButton;
        }

        public bool TabExists(string id)
        {
            return tabPageDictionary.ContainsKey(id);
        }

        public IDocumentItemControl FindDocumentControl(string Id)
        {
            return tabPageDictionary[Id].Controls[0] as IDocumentItemControl;
        }

        public void OrderTabs(IDocument[] documents)
        {
            tabControl.TabPages.Clear();
            foreach (IDocument document in documents)
            {
                TabPage tabPage = tabPageDictionary[document.Id];
                tabControl.TabPages.Add(tabPage);
            }
        }

        public TabPage AddTab(IDocument document, IDocumentItemControl tabUserControl, bool visible = true, bool select = false)
        {
            TabPage tabPage = new TabPage(document.Title);
            tabPage.Name = document.Id;
            tabPage.ImageIndex = tabUserControl.ImageKey;

            ((UserControl)tabUserControl).Dock = DockStyle.Fill;
            tabPage.Controls.Add(tabUserControl as UserControl);
            tabUserControl.Modified += tabUserControl_Modified;

            tabPageDictionary.Add(document.Id, tabPage);

            if (visible)
            {
                tabControl.TabPages.Add(tabPage);
                AddTabMenuItem(document);
                if (select)
                    tabControl.SelectedTab = tabPage;
            }

            return tabPage;
        }

        public void RemoveTab(string id)
        {
            TabPage tabPage = tabPageDictionary[id];
            IDocumentItemControl itemControl = FindDocumentControl(id);
            itemControl.Modified -= tabUserControl_Modified;

            if (BeforeDeleteTab != null)
                BeforeDeleteTab(this, new DocumentTabEventArgs(tabPage, tabPage.Controls[0] as UserControl));

            RemoveTabMenuItem(id);
            tabPageDictionary.Remove(id);
            tabControl.TabPages.Remove(tabPage);
        }

        public void DisplayTab(string id, bool visible)
        {
            TabPage tabPage = tabPageDictionary[id];

            if (visible && !tabControl.Contains(tabPage))
            {
                tabControl.TabPages.Add(tabPage);
                AddTabMenuItem(id);
                tabControl.SelectedTab = tabPage;
            }
            else if (visible && tabControl.Contains(tabPage))
                tabControl.SelectedTab = tabPage;
            else
            {
                if (tabControl.Contains(tabPage))
                {
                    tabControl.TabPages.Remove(tabPage);
                    RemoveTabMenuItem(id);
                }
            }
        }

        public void Clear()
        {
            foreach (TabPage tabPage in tabControl.TabPages)
            {
                ToolStripMenuItem item = tabMenuButton.DropDownItems[tabPage.Name] as ToolStripMenuItem;
                item.Click -= item_Click;

                IDocumentItemControl itemControl = FindDocumentControl(tabPage.Name);
                itemControl.Modified -= tabUserControl_Modified;

                if (BeforeDeleteTab != null)
                    BeforeDeleteTab(this, new DocumentTabEventArgs(tabPage, tabPage.Controls[0] as UserControl));
            }

            tabPageDictionary.Clear();
            tabControl.TabPages.Clear();
            tabMenuButton.DropDownItems.Clear();
        }

        private void AddTabMenuItem(string id)
        {
            IDocumentItemControl docControl = FindDocumentControl(id);
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Name = docControl.Id;
            item.Text = docControl.Title;
            item.Image = docControl.IconImage;
            item.Click += item_Click;
            this.tabMenuButton.DropDownItems.Add(item);
        }

        private void AddTabMenuItem(IDocument document)
        {
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Name = document.Id;
            item.Text = document.Title;

            if (document is ICodeDocument)
                item.Image = IconRepository.GetImage((document as ICodeDocument).Syntax);
            else if (document is IPdfDocument)
                item.Image = IconRepository.GetImage(IconRepository.PDF);
            else if (document is IImageDocument)
                item.Image = IconRepository.ImageByExtension(document.FileExtension);
            else if (document is IImageSetDocument)
                item.Image = IconRepository.ImageByExtension(IconRepository.IMG);
            else if (document is IUrlGroupDocument)
                item.Image = IconRepository.ImageByExtension(IconRepository.WEB);

            else
                item.Image = IconRepository.GetImage("TEXT");

            item.Click += item_Click;
            this.tabMenuButton.DropDownItems.Add(item);
        }

        private void RemoveTabMenuItem(string id)
        {
            ToolStripMenuItem item = this.tabMenuButton.DropDownItems[id] as ToolStripMenuItem;
            if (item != null)
            {
                item.Click -= item_Click;
                this.tabMenuButton.DropDownItems.Remove(item);
            }
        }

        private void tabUserControl_Modified(object sender, EventArgs e)
        {
            IDocumentItemControl control = sender as IDocumentItemControl;
            TabPage tabPage = tabPageDictionary[control.Id];
            ToolStripMenuItem item = this.tabMenuButton.DropDownItems[control.Id] as ToolStripMenuItem;

            tabPage.ImageIndex = control.ImageKey;
            tabPage.Text = control.Title;
            item.Text = control.Title;
            item.Image = control.IconImage;
        }

        private void item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            this.DisplayTab(item.Name, true);
        }
    }
}
