﻿using CygSoft.CodeCat.Plugin.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Plugin.Service
{
    /// <summary>
    /// Data Class for Available Plugin.  Holds and instance of the loaded Plugin, as well as the Plugin's Assembly Path
    /// </summary>
    public class AvailablePlugin
    {
        //This is the actual AvailablePlugin object.. 
        //Holds an instance of the plugin to access
        //ALso holds assembly path... not really necessary
        private IPlugin myInstance = null;
        private string myAssemblyPath = "";

        public IPlugin Instance
        {
            get { return myInstance; }
            set { myInstance = value; }
        }
        public string AssemblyPath
        {
            get { return myAssemblyPath; }
            set { myAssemblyPath = value; }
        }
    }
}
