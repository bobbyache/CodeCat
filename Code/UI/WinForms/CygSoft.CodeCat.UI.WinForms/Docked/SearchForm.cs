using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;
using WeifenLuo.WinFormsUI.Docking;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class SearchForm : DockContent
    {
        private AppFacade application;

        public event EventHandler<SearchKeywordsModifiedEventArgs> KeywordsAdded;
        public event EventHandler<SearchKeywordsModifiedEventArgs> KeywordsRemoved;

        public event EventHandler<SearchDelimitedKeywordEventArgs> SearchExecuted;
        public event EventHandler<OpenSnippetEventArgs> OpenSnippet;
        public event EventHandler<SelectSnippetEventArgs> SelectSnippet;

        private bool searchEnabled;
        public bool SearchEnabled
        {
            get { return searchEnabled; }
            set
            {
                searchEnabled = value;
                btnFind.Enabled = value;
            }
        }

        public string KeywordSearchText
        {
            get { return keywordsTextBox.Text; }
            set { keywordsTextBox.Text = value; }
        }

        public bool SingleSnippetSelected { get { return codeSearchResultsControl1.SingleSnippetSelected; } }
        public bool MultipleSnippetsSelected { get { return codeSearchResultsControl1.MultipleSnippetsSelected; } }
        public IKeywordIndexItem SelectedSnippet { get { return codeSearchResultsControl1.SelectedSnippet; } }
        public IKeywordIndexItem[] SelectedSnippets { get { return codeSearchResultsControl1.SelectedSnippets; } }

        public SearchForm(AppFacade application)
        {
            InitializeComponent();

            codeSearchResultsControl1.Application = application;
            btnFind.Image = Resources.GetImage(Constants.ImageKeys.FindSnippets);

            this.application = application;
            HideOnClose = true;
            DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight;
            btnFind.Click += (s, e) => ExecuteSearch();
            keywordsTextBox.TextChanged += (s, e) => ExecuteSearch();

            codeSearchResultsControl1.OpenSnippet += (s, e) => OpenSnippet?.Invoke(s, e);
            codeSearchResultsControl1.SelectSnippet += (s, e) => SelectSnippet?.Invoke(s, e);
            codeSearchResultsControl1.KeywordsAdded += (s, e) => KeywordsAdded?.Invoke(s, e);
            codeSearchResultsControl1.KeywordsRemoved += (s, e) => KeywordsRemoved?.Invoke(s, e);
            codeSearchResultsControl1.SearchExecuted += (s, e) => SearchExecuted?.Invoke(s, e);
        }

        public void ExecuteSearch()
        {
            if (searchEnabled)
                codeSearchResultsControl1.ExecuteSearch(keywordsTextBox.Text);
        }

        public void ExecuteSearch(string selectedId)
        {
            codeSearchResultsControl1.ExecuteSearch(keywordsTextBox.Text);
        }
    }
}
