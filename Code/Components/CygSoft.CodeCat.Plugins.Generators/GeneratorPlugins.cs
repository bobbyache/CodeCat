using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Plugins.Generators
{
    public class GeneratorPlugins
    {
        public class PluginListItem
        {
            public readonly string Title;
            public readonly string Id;

            public PluginListItem(string Id, string Title)
            {
                this.Id = Id;
                this.Title = Title;
            }
        }

        public PluginListItem[] PluginItems
        {
            get
            {
                return pluginsDictionary.Values.Select(p => new PluginListItem(p.Id, p.Title)).ToArray();
            }
        }

        public IGeneratorPlugin GetPlugin(string id)
        {
            return pluginsDictionary[id];
        }

        private readonly string pluginFolderPath;
        private Dictionary<string, IGeneratorPlugin> pluginsDictionary = new Dictionary<string, IGeneratorPlugin>();

        public GeneratorPlugins(string pluginFolderPath)
        {
            if (string.IsNullOrEmpty(pluginFolderPath) || !Directory.Exists(pluginFolderPath))
                throw new DirectoryNotFoundException("Plugin directory couldn not be found.");

            this.pluginFolderPath = pluginFolderPath;
        }

        public void LoadPlugins()
        {
            string[] pluginAssemblies = Directory.GetFiles(pluginFolderPath, "*.dll");
            foreach (string assembly in pluginAssemblies)
            {
                IGeneratorPlugin[] plugins = LoadGeneratorPlugins(assembly);

                foreach (IGeneratorPlugin plugin in plugins)
                {
                    if (!pluginsDictionary.ContainsKey(plugin.Id))
                        pluginsDictionary.Add(plugin.Id, plugin);
                }
            }
        }

        private IGeneratorPlugin[] LoadGeneratorPlugins(string assemblyFilePath)
        {
            if (File.Exists(assemblyFilePath))
            {
                Type[] types = (from t in Assembly.LoadFrom(assemblyFilePath).GetExportedTypes()
                                      where !t.IsInterface && !t.IsAbstract
                                      where typeof(IGeneratorPlugin).IsAssignableFrom(t)
                                      select t).ToArray();

                IGeneratorPlugin[] plugins = types.Select(t => (IGeneratorPlugin)Activator.CreateInstance(t)).ToArray();
                return plugins;
            }
            else
                return new IGeneratorPlugin[0];
        }
    }
}
