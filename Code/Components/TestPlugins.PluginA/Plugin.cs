using CygSoft.CodeCat.Plugin.Infrastructure;
using System;

namespace TestPlugins.PluginA
{
    public class Plugin : IPlugin
    {
        public string Author
        {
            get
            {
                return "Rob Blake";
            }
        }

        public string Description
        {
            get
            {
                return "This is just a test plugin.";
            }
        }

        public string Name
        {
            get
            {
                return "TestPluginA";
            }
        }

        public string Title
        {
            get
            {
                return "Test Plugin A";
            }
        }

        public string Version
        {
            get
            {
                return "1.0.0.0";
            }
        }

        public IPluginControl CreateItem(string id, string directoryPath)
        {
            return new TestControl();
        }

        public void DeleteItem(string id, string directoryPath)
        {
            throw new NotImplementedException();
        }

        public IPluginControl GetItem(string id, string directoryPath)
        {
            return new TestControl();
        }

        public void Load()
        {
            //throw new NotImplementedException();
        }

        public void Unload()
        {
            //throw new NotImplementedException();
        }
    }
}
