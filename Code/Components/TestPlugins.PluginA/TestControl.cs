using CygSoft.CodeCat.Plugin.Infrastructure;
using System;
using System.Windows.Forms;

namespace TestPlugins.PluginA
{
    public partial class TestControl : UserControl, IPluginControl
    {
        public TestControl()
        {
            InitializeComponent();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Revert()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
