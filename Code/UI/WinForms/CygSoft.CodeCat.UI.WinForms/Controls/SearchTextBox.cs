using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.Controls.AutoCompleteSearch
{
    public partial class SearchTextBox : TextBox
    {
        public event EventHandler OpenDropDown;
        public event EventHandler CloseDropDown;
        public event EventHandler CurrentTermChanged;
        public event EventHandler CurrentTermCommitted;
        public event EventHandler PressUpKey;
        public event EventHandler PressDownKey;

        // http://www.codeproject.com/Tips/794589/NET-Get-correct-caret-position-from-TextBox-contro
        // http://stackoverflow.com/questions/1831219/knowing-the-point-location-of-the-caret-in-a-winforms-textbox
        [DllImport("user32.dll")]
        private static extern bool GetCaretPos(out Point lpPoint);

        private CurrentSearchTerm currentTerm = new CurrentSearchTerm();
        public string CurrentTerm { get { return currentTerm.ToString(); } }

        private ListBox dropDownList;
        public ListBox DropDownList 
        {
            set
            {
                if (dropDownList != null)
                {
                    dropDownList.DoubleClick -= dropDownList_DoubleClick;
                    dropDownList.KeyDown -= dropDownList_KeyDown;
                    dropDownList.SelectedIndexChanged -= dropDownList_SelectedIndexChanged;
                }

                this.dropDownList = value;
                this.dropDownList.Visible = false;

                dropDownList.DoubleClick += dropDownList_DoubleClick;
                dropDownList.KeyDown += dropDownList_KeyDown;
                dropDownList.SelectedIndexChanged += dropDownList_SelectedIndexChanged;
            }
        }

        #region Constructors

        public SearchTextBox()
        {
            CommonInitialize();
        }

        public SearchTextBox(IContainer container)
        {
            container.Add(this);
            CommonInitialize();
        }

        #endregion

        public void ResetList(object[] items)
        {
            if (this.dropDownList == null)
                return;

            dropDownList.Items.Clear();
            dropDownList.Items.AddRange(items);
        }

        #region Event Handlers

        private void dropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void dropDownList_KeyDown(object sender, KeyEventArgs e)
        {
            this.Focus();
        }

        private void dropDownList_DoubleClick(object sender, EventArgs e)
        {
            if (dropDownList.SelectedItem != null)
                this.CommitText(dropDownList.SelectedItem.ToString());
            this.Focus();
        }

        private void currentTerm_TermCommitted(object sender, EventArgs e)
        {
            if (dropDownList.SelectedItem != null)
                this.CommitText(dropDownList.SelectedItem.ToString());

            if (CurrentTermCommitted != null)
                CurrentTermCommitted(this, new EventArgs());
        }

        private void currentTerm_MoveNext(object sender, EventArgs e)
        {
            CloseOpenDropdown();
            //Debug.WriteLine(currentTerm.ToString());
            //Debug.WriteLine("Move Next");
        }

        private void currentTerm_MovePrevious(object sender, EventArgs e)
        {
            CloseOpenDropdown();
            currentTerm.FetchPreviousTerm(this.Text, this.SelectionStart - 2);
            //Debug.WriteLine(currentTerm.ToString());
            //Debug.WriteLine("Move Previous");
        }

        private void SearchTextBox_LostFocus(object sender, EventArgs e)
        {
            HideContextList();
            if (CloseDropDown != null)
                CloseDropDown(this, new EventArgs());
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                HideContextList();
                if (CloseDropDown != null)
                    CloseDropDown(this, new EventArgs());
            }
            else if (e.KeyCode == Keys.Up)
            {
                MoveUp();
                if (PressUpKey != null)
                    PressUpKey(this, new EventArgs());
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                MoveDown();
                if (PressDownKey != null)
                    PressDownKey(this, new EventArgs());
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                HideContextList();
                if (CloseDropDown != null)
                    CloseDropDown(this, new EventArgs());
            }
            else if (e.KeyCode == Keys.Enter && (e.Alt && e.Shift))
            {
                ShowContextList();
                if (OpenDropDown != null)
                    OpenDropDown(this, new EventArgs());
            }
        }

        private void SearchTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            currentTerm.TypeChar(e.KeyChar);
            if (this.Text.Length == 0)
                CloseOpenDropdown();

            dropDownList.SelectedIndex = dropDownList.FindString(this.CurrentTerm);
            if (CurrentTermChanged != null)
                CurrentTermChanged(this, new EventArgs());
        }

        #endregion

        #region Private Methods

        private void CommonInitialize()
        {
            InitializeComponent();
            this.KeyDown += SearchTextBox_KeyDown;
            this.LostFocus += SearchTextBox_LostFocus;
            this.currentTerm.MovePrevious += currentTerm_MovePrevious;
            this.currentTerm.MoveNext += currentTerm_MoveNext;
            this.currentTerm.TermCommitted += currentTerm_TermCommitted;
        }

        private void CommitText(string text)
        {
            int startIndex = 0;
            int index = this.SelectionStart - 1;

            if (index < 0)
                index = 0;

            while (index > 0 && this.Text[index] != ',')
            {
                index--;
            }
            startIndex = index;

            int length = this.SelectionStart - startIndex;


            this.SelectionStart = startIndex;
            this.SelectionLength = length;

            if (startIndex == 0)
                this.SelectedText = text;
            else
                this.SelectedText = ", " + text;

            HideContextList();
            if (CloseDropDown != null)
                CloseDropDown(this, new EventArgs());
        }

        private void ShowContextList()
        {
            if (!dropDownList.Visible && dropDownList.Items.Count > 0)
            {
                try
                {
                    Point point = new Point();
                    point.Y = (int)Math.Ceiling(this.Font.GetHeight()) + 2 + this.Top - 45;
                    point.X = this.Left + GetCorrectCaretPos().X + 2;

                    this.dropDownList.Location = point;
                    this.dropDownList.BringToFront();
                    this.dropDownList.Show();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private void HideContextList()
        {
            this.dropDownList.Hide();
        }

        private void MoveUp()
        {
            if (dropDownList.Visible)
            {
                if (this.dropDownList.SelectedIndex > 0)
                    this.dropDownList.SelectedIndex--;
            }
        }

        private void MoveDown()
        {
            if (this.dropDownList.Visible)
            {
                if (this.dropDownList.SelectedIndex < this.dropDownList.Items.Count - 1)
                    this.dropDownList.SelectedIndex++;
            }
        }

        private Point GetCorrectCaretPos()
        {
            Point pt = Point.Empty;
            if (GetCaretPos(out pt))
            {
                return pt;
            }
            else
            {
                return Point.Empty;
            }
        }

        private void TypeCharacter(Keys keyCode)
        {
            currentTerm.TypeChar(Convert.ToChar(keyCode));
            dropDownList.SelectedIndex = dropDownList.FindString(this.CurrentTerm);
            if (CurrentTermChanged != null)
                CurrentTermChanged(this, new EventArgs());
        }

        private void CloseOpenDropdown()
        {
            HideContextList();
            if (CloseDropDown != null)
                CloseDropDown(this, new EventArgs());

            ShowContextList();
            if (OpenDropDown != null)
                OpenDropDown(this, new EventArgs());
        }

        #endregion
    }
}
