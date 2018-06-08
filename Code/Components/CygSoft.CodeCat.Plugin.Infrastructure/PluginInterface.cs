using System;
using System.Windows.Forms;

namespace CygSoft.CodeCat.Plugin.Infrastructure
{
    public interface IPluginControl: IPositionedItem
    {
        event EventHandler<FileEventArgs> BeforeDelete;
        event EventHandler<FileEventArgs> AfterDelete;

        event EventHandler<FileEventArgs> BeforeOpen;
        event EventHandler<FileEventArgs> AfterOpen;
        event EventHandler<FileEventArgs> BeforeSave;
        event EventHandler<FileEventArgs> AfterSave;
        event EventHandler<FileEventArgs> BeforeClose;
        event EventHandler<FileEventArgs> AfterClose;
        event EventHandler<FileEventArgs> BeforeRevert;
        event EventHandler<FileEventArgs> AfterRevert;

        string FilePath { get; }
        string FileName { get; }
        string FileExtension { get; }
        string Folder { get; }
        bool FolderExists { get; }
        bool Exists { get; }
        bool Loaded { get; }

        void Open();
        void Delete();
        void Save();
        void Close();
        void Revert();

        string Id { get; }

        string Title { get; set; }
        string DocumentType { get; set; }
        string Description { get; set; }
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
