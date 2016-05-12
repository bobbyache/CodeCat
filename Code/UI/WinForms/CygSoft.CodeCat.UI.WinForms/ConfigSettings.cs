using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.UI.WinForms
{
    public static class ConfigSettings
    {
        private static string registryFolder;
        public static string RegistryPath
        {
            get
            {
                if (string.IsNullOrEmpty(registryFolder))
                    registryFolder = ConfigurationManager.AppSettings["RegistryPath"];

                return registryFolder;
            }
            set
            {
                ConfigurationManager.AppSettings["RegistryPath"] = value;
            }
        }

        private static string applicationTitle;
        public static string ApplicationTitle
        {
            get
            {
                if (string.IsNullOrEmpty(applicationTitle))
                    applicationTitle = ConfigurationManager.AppSettings["ApplicationTitle"];

                return applicationTitle;
            }
        }

        private static int codeLibraryIndexFileVersion = -1;
        public static int CodeLibraryIndexFileVersion
        {
            get
            {
                if (codeLibraryIndexFileVersion < 0)
                    codeLibraryIndexFileVersion = Int32.Parse(ConfigurationManager.AppSettings["CodeLibraryIndexFileVersion"]);

                return codeLibraryIndexFileVersion;
            }
        }

        private static string lastProject;
        public static string LastProject
        {
            get
            {
                if (string.IsNullOrEmpty(lastProject))
                    lastProject = ConfigurationManager.AppSettings["LastProject"];

                return lastProject;
            }
            set
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configuration.AppSettings.Settings["LastProject"].Value = value;
                configuration.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        private static string syntaxFilePath;
        public static string SyntaxFilePath
        {
            get
            {
                if (string.IsNullOrEmpty(syntaxFilePath))
                    syntaxFilePath = ConfigurationManager.AppSettings["SyntaxFilePath"];

                return syntaxFilePath;
            }
            set
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configuration.AppSettings.Settings["SyntaxFilePath"].Value = value;
                configuration.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        private static string defaultSyntax;
        public static string DefaultSyntax
        {
            get
            {
                if (string.IsNullOrEmpty(defaultSyntax))
                    defaultSyntax = ConfigurationManager.AppSettings["DefaultSyntax"];

                return defaultSyntax;
            }
            set
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configuration.AppSettings.Settings["DefaultSyntax"].Value = value;
                configuration.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public static void Refresh()
        {
            registryFolder = ConfigurationManager.AppSettings["RegistryPath"];
            applicationTitle = ConfigurationManager.AppSettings["ApplicationTitle"];
            lastProject = ConfigurationManager.AppSettings["LastProject"];
            syntaxFilePath = ConfigurationManager.AppSettings["SyntaxFilePath"];
            defaultSyntax = ConfigurationManager.AppSettings["DefaultSyntax"];
            codeLibraryIndexFileVersion = Int32.Parse(ConfigurationManager.AppSettings["CodeLibraryIndexFileVersion"]);
        }

    }
}
