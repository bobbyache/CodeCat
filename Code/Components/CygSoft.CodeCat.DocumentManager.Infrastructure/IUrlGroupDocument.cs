using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IUrlItem
    {
        string Id { get; set; }
        string Title { get; set; }
        string Category { get; set; }
        string Url { get; set; }
        string HostName { get; }
        string Description { get; set; }
        DateTime DateModified { get; set; }
        DateTime DateCreated { get; set; }
    }

    public interface IUrlGroupDocument : IVersionableFile, IPositionedItem
    {
        string Title { get; set; }
        string DocumentType { get; set; }
        string Description { get; set; }
        IUrlItem[] Items { get; }
        string[] Categories { get; }
        void Add(IUrlItem urlItem);
        void Remove(IUrlItem urlItem);
    }
}
