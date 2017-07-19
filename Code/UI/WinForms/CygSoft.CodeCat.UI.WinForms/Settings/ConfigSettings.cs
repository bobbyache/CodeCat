using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.UI.WinForms
{
    public static class ConfigSettings
    {

        public static Version AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version;
            }
        }

        public static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public static string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public static string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public static string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public static string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

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

        private static string msPaintEditorPath;
        public static string MsPaintEditorPath
        {
            get
            {
                if (string.IsNullOrEmpty(msPaintEditorPath))
                    msPaintEditorPath = ConfigurationManager.AppSettings["MsPaintEditorPath"];

                return msPaintEditorPath;
            }
            set
            {
                ConfigurationManager.AppSettings["RegistryPath"] = value;
            }
        }

        private static string applicationTitle;
        public static string ApplicationTitle
        {
            get { return ConfigSettings.AssemblyDescription; }
        }

        public static Version ProjectFileVersion
        {
            get
            {
                Version version = AssemblyVersion;
                return new Version(version.Major, version.Minor);
            }
        }

        private static string helpPageUrl = "";
        public static string HelpPageUrl
        {
            get
            {
                if (string.IsNullOrEmpty(helpPageUrl))
                    helpPageUrl = ConfigurationManager.AppSettings["HelpPageUrl"];

                return helpPageUrl;
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

        private static string qikScriptSyntaxFile;
        public static string QikScriptSyntaxFile
        {
            get
            {
                if (string.IsNullOrEmpty(qikScriptSyntaxFile))
                    qikScriptSyntaxFile = ConfigurationManager.AppSettings["QikScriptSyntaxFile"];

                return qikScriptSyntaxFile;
            }
            set
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configuration.AppSettings.Settings["QikScriptSyntaxFile"].Value = value;
                configuration.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        private static string qikTemplateSyntaxFile;
        public static string QikTemplateSyntaxFile
        {
            get
            {
                if (string.IsNullOrEmpty(qikTemplateSyntaxFile))
                    qikTemplateSyntaxFile = ConfigurationManager.AppSettings["QikTemplateSyntaxFile"];

                return qikTemplateSyntaxFile;
            }
            set
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configuration.AppSettings.Settings["QikTemplateSyntaxFile"].Value = value;
                configuration.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        private static int? defaultFontSize;
        public static int DefaultFontSize
        {
            get
            {
                if (!defaultFontSize.HasValue)
                {
                    int fontSize;
                    bool success = int.TryParse(ConfigurationManager.AppSettings["DefaultFontSize"], out fontSize);
                    defaultFontSize = (success && fontSize >= 0) ? fontSize : 10;
                }

                return defaultFontSize.Value;
            }
            set
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configuration.AppSettings.Settings["DefaultFontSize"].Value = value.ToString();
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
            qikScriptSyntaxFile = ConfigurationManager.AppSettings["QikScriptSyntaxFile"];
            qikTemplateSyntaxFile = ConfigurationManager.AppSettings["QikTemplateSyntaxFile"];
            helpPageUrl = ConfigurationManager.AppSettings["HelpPageUrl"];
            defaultFontSize = Int32.Parse(ConfigurationManager.AppSettings["DefaultFontSize"]);
            msPaintEditorPath = ConfigurationManager.AppSettings["MsPaintEditorPath"];
        }

    }
}
