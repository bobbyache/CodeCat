using System;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class AboutBoxDialog : Form
    {
        public AboutBoxDialog()
        {
            InitializeComponent();
            Text = String.Format("About {0}", AssemblyTitle);
            labelProductName.Text = AssemblyProduct;
            labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            labelCopyright.Text = AssemblyCopyright;

            rtfCredits.LoadFile("credits.rtf");
            txtLicense.TextAlign = HorizontalAlignment.Center;
            txtLicense.Text = File.ReadAllText("LICENSE");
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get { return ConfigSettings.AssemblyTitle; }
        }

        public string AssemblyVersion
        {
            get { return ConfigSettings.AssemblyVersion.ToString(); }
        }

        public string AssemblyDescription
        {
            get { return ConfigSettings.AssemblyDescription; }
        }

        public string AssemblyProduct
        {
            get { return ConfigSettings.AssemblyProduct; }
        }

        public string AssemblyCopyright
        {
            get { return ConfigSettings.AssemblyCopyright; }
        }

        public string AssemblyCompany
        {
            get { return ConfigSettings.AssemblyCompany; }
        }

        #endregion
    }
}
