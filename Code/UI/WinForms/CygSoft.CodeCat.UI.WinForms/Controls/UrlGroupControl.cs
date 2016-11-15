using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.CodeGroup;
using CygSoft.CodeCat.DocumentManager.Infrastructure;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class UrlGroupControl : UserControl, IDocumentItemControl
    {
        public UrlGroupControl(AppFacade application, ICodeGroupDocumentGroup codeGroupFile, IUrlGroupDocument urlDocument)
        {
            InitializeComponent();
        }

        public event EventHandler Modified;

        public string Id { get; private set; }

        public string Title
        {
            get { return this.txtTitle.Text; }
        }

        public int ImageKey { get { return 0; } }

        public Icon ImageIcon { get { return IconRepository.GetIcon("TEXT"); } }

        public Image IconImage { get { return IconRepository.GetImage("TEXT"); } }

        public bool IsModified { get; private set; }

        public bool FileExists { get { return false; } }

        public void Revert()
        {
        }
    }
}
