using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.UI.WinForms
{
    public class Resources
    {
        public static string Namespace { get; set; }
        public static Assembly ExecutingAssembly { get; set; }

        public static Image GetImage(string key)
        {
            ResourceManager resManager = new ResourceManager(Namespace, ExecutingAssembly);
            return (Image)resManager.GetObject(key);
        }
    }
}
