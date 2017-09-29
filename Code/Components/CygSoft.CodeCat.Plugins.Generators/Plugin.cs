using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Plugins.Generators
{
    public class Plugin
    {
        public string PluginName { get; private set; }
        public Plugin(string fileName)
        {
            try
            {
                PluginName = Path.GetFileName(fileName);
                Assembly assembly = Assembly.LoadFrom(fileName);
                Type type = assembly.GetType("ImageEditor.Plugin");

            }
            catch (Exception ex)
            {

            }
        }
    }
}
