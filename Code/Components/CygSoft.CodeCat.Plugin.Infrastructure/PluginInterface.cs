using System;
using System.Windows.Forms;

namespace CygSoft.CodeCat.Plugin.Infrastructure
{
    public interface IPluginControl
    {
        void Save();
        void Revert();
        void Close();
        void Delete();
    }

	public interface IPlugin
	{
		string Name {get;}
        string Title { get; }
		string Description {get;}
		string Author {get;}
		string Version {get;}

        IPluginControl CreateItem(string id, string directoryPath);
        IPluginControl GetItem(string id, string directoryPath);
        void DeleteItem(string id, string directoryPath);

        void Load();
        void Unload();
	}
	
	public interface IPluginHost
	{
		string PluginDetail(string Feedback, IPlugin Plugin);	
	}
}
