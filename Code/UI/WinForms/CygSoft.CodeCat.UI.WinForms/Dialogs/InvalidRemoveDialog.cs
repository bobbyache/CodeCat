﻿using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class InvalidRemoveDialog : Form
    {
        public InvalidRemoveDialog(IKeywordIndexItem[] invalidItems)
        {
            InitializeComponent();
            lstInvalidItems.Items.AddRange(invalidItems.Select(doc => doc.Title).ToArray());
        }
    }
}
