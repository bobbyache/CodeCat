using CygSoft.CodeCat.DocumentManager.PathGenerators;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

/*
 * FileSet only deals with how indexed items are added or removed from the index.
 * Careful of adding the following:
 * 
 *  - It should not be used to handle the files referenced within the index.
 *  - this version should not even deal with order and placement in the order
 *  ONE POSSIBILITY
 *  - Later, PositionableList should be modified to take in an existing collection !!!
 *  One can add positional stuff via inheritance or via decoration! Look into advantages of
 *  decoration.
 *  
 *  However, for many items you may wish to use a dictionary to reference lots of files,
 *  although you can also just do this through LINQ.
 *  
 *  You might also want to reference a file within a sub-folder... think about what types
 *  of file list you'll have. You may have file lists within file lists ie...
 *  Event Diary with sub-folders for: Images, Attachments, Snippets, etc...
 * */

namespace Domain.TopicSections.CodeTemplate.UnitTests
{
    [TestFixture]
    [Category("TopicSection")]
    [Category("TopicSection.CodeTemplate")]
    public class FileSetTests
    {
        [Test]
        public void FileSet_WhenInstantiated_Sets_FilePath_As_Expected()
        {
            var documentIndexRepository = new Mock<IFileIndexRepository>();
            var repository = documentIndexRepository.Object;
            var pathGenerator = new DocumentIndexPathGenerator(@"C:\\CodeCat\Project\CodeGroup\", 
                "grp", "a2e0b048-8697-4958-b38d-eed4bdecc000");

            FileSet fileSet = new FileSet(repository, pathGenerator);

            Assert.AreEqual(@"C:\\CodeCat\Project\CodeGroup\a2e0b048-8697-4958-b38d-eed4bdecc000\a2e0b048-8697-4958-b38d-eed4bdecc000.grp", 
                fileSet.IndexPath);
        }

        [Test]
        public void FileSet_WhenLoaded_Returns_Correct_FileCount()
        {
            var pathGenerator = new DocumentIndexPathGenerator(@"C:\\CodeCat\Project\CodeGroup\",
                "grp", "a2e0b048-8697-4958-b38d-eed4bdecc000");

            FileSet fileSet = new FileSet(HelperMethods.GetRepositoryWithTwoItems(), pathGenerator);
            fileSet.Open();
            Assert.AreEqual(2, fileSet.FileCount);
        }

        [Test]
        public void FileSet_Add_Successfully_Increments_Item_Count_By_1()
        {
            var pathGenerator = new DocumentIndexPathGenerator(@"C:\\CodeCat\Project\CodeGroup\",
                "grp", "a2e0b048-8697-4958-b38d-eed4bdecc000");

            FileSet fileSet = new FileSet(HelperMethods.GetRepositoryWithTwoItems(), pathGenerator);
            fileSet.Open();
            fileSet.Add("Another File", "AnotherFile.txt");
            Assert.AreEqual(3, fileSet.FileCount);
        }

        [Test]
        public void FileSet_When_Instantiated_With_Null_Repository_Throws_NullArgumentException()
        {
            var pathGenerator = new DocumentIndexPathGenerator(@"C:\\CodeCat\Project\CodeGroup\",
                "grp", "a2e0b048-8697-4958-b38d-eed4bdecc000");

            ActualValueDelegate<object> del = () => new FileSet(null, pathGenerator);

            Assert.That(del, Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public void FileSet_When_Instantiated_With_Null_PathGenerator_Throws_NullArgumentException()
        {
            var pathGenerator = new DocumentIndexPathGenerator(@"C:\\CodeCat\Project\CodeGroup\",
                "grp", "a2e0b048-8697-4958-b38d-eed4bdecc000");

            ActualValueDelegate<object> del = () => new FileSet(new Mock<IFileIndexRepository>().Object, null);

            Assert.That(del, Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public void IndexFile_EnsureThat_It_Realises_The_IIndexedFileHeader_Interface()
        {
            IndexFile file = new IndexFile();
            Assert.That(file is IIndexFile);
        }

        [Test]
        public void IndexFile_EnsureThat_It_Realises_The_IIndexedFile_Interface()
        {
            IndexFile file = new IndexFile();
            Assert.That(file is IIndexFile);
        }

        [Test]
        public void FileSet_Open_Calls_Repository_LoadIndeces()
        {
            var fileIndexRepository = new Mock<IFileIndexRepository>();

            FileSet fileSet = new FileSet(fileIndexRepository.Object,
                new DocumentIndexPathGenerator(@"C:\\CodeCat\Project\CodeGroup\",
                "grp", "a2e0b048-8697-4958-b38d-eed4bdecc000"));

            fileSet.Open();

            fileIndexRepository.Verify(f1 => f1.LoadIndeces(), Times.Once());
        }


        [Test]
        public void FileSet_Save_Calls_Repository_WriteIndeces()
        {
            var fileIndexRepository = new Mock<IFileIndexRepository>();

            FileSet fileSet = new FileSet(fileIndexRepository.Object,
                new DocumentIndexPathGenerator(@"C:\\CodeCat\Project\CodeGroup\",
                "grp", "a2e0b048-8697-4958-b38d-eed4bdecc000"));

            fileSet.Open();
            fileSet.Save();

            fileIndexRepository.Verify(f1 => f1.WriteIndeces(null), Times.Once());
        }

        [Test]
        public void FileSet_Save_WhenCalled_Without_Opening_Throws_Exception()
        {
            var fileIndexRepository = new Mock<IFileIndexRepository>();

            FileSet fileSet = new FileSet(fileIndexRepository.Object,
                new DocumentIndexPathGenerator(@"C:\\CodeCat\Project\CodeGroup\",
                "grp", "a2e0b048-8697-4958-b38d-eed4bdecc000"));

            TestDelegate proc = () => fileSet.Save();
            Assert.That(proc, Throws.TypeOf<FileNotOpenException>());
        }

        [Test]
        public void FileSet_Add_WhenCalled_Without_Opening_Throws_Exception()
        {
            var fileIndexRepository = new Mock<IFileIndexRepository>();

            FileSet fileSet = new FileSet(fileIndexRepository.Object,
                new DocumentIndexPathGenerator(@"C:\\CodeCat\Project\CodeGroup\",
                "grp", "a2e0b048-8697-4958-b38d-eed4bdecc000"));

            TestDelegate proc = () => fileSet.Add("My File", "MyFile.txt");
            Assert.That(proc, Throws.TypeOf<FileNotOpenException>());
        }
    }

    [Serializable()]
    public class FileNotOpenException : Exception, ISerializable
    {
        public FileNotOpenException() : base() { }
        public FileNotOpenException(string message) : base(message) { }
        public FileNotOpenException(string message, System.Exception inner) : base(message, inner) { }
        public FileNotOpenException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    public class IndexFile : IIndexFile
    {
        public string FilePath { get; set; }
    }

    public interface IIndexFile
    {
        string FilePath { get; set; }
    }

    public interface IFileIndexRepository
    {
        List<IIndexFile> LoadIndeces();
        void WriteIndeces(List<IIndexFile> documents);
    }

    public class FileSet
    {
        private IFileIndexRepository repository;
        private DocumentIndexPathGenerator indexPathGenerator;
        private List<IIndexFile> files;

        public string IndexPath => indexPathGenerator.FilePath;

        public double FileCount { get { return files.Count; } }

        public bool IsOpen { get; private set; }

        public FileSet(IFileIndexRepository repository, DocumentIndexPathGenerator indexPathGenerator)
        {
            if (repository == null)
                throw new NullReferenceException("Mandatory FileSet repository was not provided.");

            if (indexPathGenerator == null)
                throw new NullReferenceException("Mandatory PathGenerator was not provided.");

            this.repository = repository;
            this.indexPathGenerator = indexPathGenerator;
        }

        public void Add(string title, string filePath)
        {
            if (!IsOpen)
                throw new FileNotOpenException("The index has not been loaded and can therefore not be added to.");
            IndexFile indexFile = new IndexFile();
            indexFile.FilePath = filePath;
            this.files.Add(indexFile);
        }

        public void Open()
        {
            this.files = this.repository.LoadIndeces();
            this.IsOpen = true;
        }

        public void Save()
        {
            if (!IsOpen)
                throw new FileNotOpenException("The index has not been loaded and can therefore not be saved.");

            this.repository.WriteIndeces(files);
        }
    }
}
