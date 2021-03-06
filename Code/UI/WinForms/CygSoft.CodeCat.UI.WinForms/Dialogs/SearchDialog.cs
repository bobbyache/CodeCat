﻿using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.Dialogs
{
    public partial class SearchDialog : Form
    {
        private AppFacade application;

        public event EventHandler<SearchKeywordsModifiedEventArgs> KeywordsAdded;
        public event EventHandler<SearchKeywordsModifiedEventArgs> KeywordsRemoved;

        public event EventHandler<SearchDelimitedKeywordEventArgs> SearchExecuted;
        public event EventHandler<TopicIndexEventArgs> OpenSnippet;
        public event EventHandler<TopicIndexEventArgs> SelectSnippet;

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

        public bool SingleSnippetSelected { get { return codeSearchResultsControl1.SingleTopicSelected; } }
        public bool MultipleSnippetsSelected { get { return codeSearchResultsControl1.MultipleTopicsSelected; } }
        public IKeywordIndexItem SelectedSnippet { get { return codeSearchResultsControl1.SelectedTopic; } }
        public IKeywordIndexItem[] SelectedSnippets { get { return codeSearchResultsControl1.SelectedTopics; } }

        public SearchDialog(AppFacade application)
        {
            InitializeComponent();

            if (application == null)
                throw new ArgumentNullException("Application is a required constructor parameter and cannot be null");
            this.application = application;
            this.SearchEnabled = true;

            codeSearchResultsControl1.Application = application;

            btnFind.Image = Gui.Resources.GetImage(Constants.ImageKeys.FindSnippets);
            btnFind.Click += (s, e) => ExecuteSearch();

            keywordsTextBox.TextChanged += (s, e) => ExecuteSearch();

            codeSearchResultsControl1.OpenTopic += (s, e) => OpenSnippet?.Invoke(s, e);
            codeSearchResultsControl1.SelectTopic += (s, e) => SelectSnippet?.Invoke(s, e);
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

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
