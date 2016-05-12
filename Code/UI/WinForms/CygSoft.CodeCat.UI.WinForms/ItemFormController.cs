using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms
{
    public interface IItemForm
    {
        string Id { get; }
        void Initialize(IPersistableFile persistableFile, AppFacade application);
    }
    public class ItemFormEventArgs : EventArgs
    {
        public string Id { get; private set; }
        public ItemFormEventArgs(string id)
        {
            this.Id = id;
        }
    }

    public class ItemFormController<T> where T : Form, IItemForm, new()
    {
        public event EventHandler<ItemFormEventArgs> ItemFormClosed;

        private IWin32Window mainForm;
        private Dictionary<string, T> openForms = new Dictionary<string, T>();

        public ItemFormController(IWin32Window mainForm)
        {
            this.mainForm = mainForm;
        }

        public bool OpenSnippetsExist { get { return openForms.Count > 0; } }

        public bool ItemFormIsOpen(string id)
        {
            return openForms.ContainsKey(id);
        }

        public bool CloseitemForm(string id)
        {
            if (openForms.ContainsKey(id))
            {
                T itemForm = openForms[id];
                itemForm.Close();
                return true;
            }
            return false;
        }

        public void OpenCodeWindow(IPersistableFile persistableFile, AppFacade application)
        {
            if (this.openForms.ContainsKey(persistableFile.Id))
            {
                this.openForms[persistableFile.Id].Activate();
            }
            else
            {
                T itemForm = new T();
                itemForm.Initialize(persistableFile, application);
                itemForm.FormClosed += itemForm_FormClosed;

                this.openForms.Add(itemForm.Id, itemForm);
                itemForm.Show();
            }
        }

        private void itemForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            T frm = (sender as T);
            if (this.openForms.ContainsKey(frm.Id))
            {
                frm.FormClosed -= itemForm_FormClosed;
                this.openForms.Remove(frm.Id);

                if (this.ItemFormClosed != null)
                    this.ItemFormClosed(this, new ItemFormEventArgs(frm.Id));
            }
        }
    }
}
