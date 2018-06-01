using CygSoft.CodeCat.Infrastructure;
using System;

namespace CygSoft.CodeCat.Domain.Code
{
    public interface ICodeFile
    {
        string CommaDelimitedKeywords { get; set; }
        bool HasSnapshots { get; }
        string Id { get; }
        IKeywordIndexItem IndexItem { get; }
        CodeSnapshot[] Snapshots { get; }
        string Syntax { get; set; }
        string Text { get; set; }
        string Title { get; set; }

        event EventHandler SnapshotDeleted;
        event EventHandler SnapshotTaken;

        void DeleteSnapshot(string snapshotId);
        void TakeSnapshot(string description = "");

        void Save();
    }
}