using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.UI.WinForms.Controls.AutoCompleteSearch
{
    public class CurrentSearchTerm
    {
        public event EventHandler MoveNext;
        public event EventHandler MovePrevious;
        public event EventHandler TermCommitted;

        private StringBuilder builder = new StringBuilder();
        public bool IsEmpty { get { return builder.Length == 0; } }

        public void Set(string term)
        {
            builder.Clear();
            builder.Append(term.ToUpper());
        }

        public void FetchPreviousTerm(string text, int position)
        {
            StringBuilder bldr = new StringBuilder();
            builder.Clear();

            while (position >= 0 && text[position] != ',')
            {
                bldr.Append(text[position]);
                position--;
            }

            if (bldr.Length > 0)
            {
                char[] termArray = bldr.ToString().ToCharArray();
                Array.Reverse(termArray);

                builder.Append(new string(termArray));
            }
        }

        public void TypeChar(char character)
        {
            if (IgnoreCharacter(character))
                return;
            else if (character == ',')
            {
                this.Comma();
                builder.Append(character);
            }
            else if (character == '\b')
            {
                this.Backspace();
            }
            else if (character == '\r')
            {
                Enter();
            }
            else
            {
                builder.Append(character);
                //Debug.WriteLine(string.Format("Recorded: '{0}' - Search Term: '{1}'", this.builder.ToString(), this.ToString()));
            }
        }

        private void Enter()
        {
            if (TermCommitted != null)
                TermCommitted(this, new EventArgs());
            builder.Clear();
        }

        private void Comma()
        {
            this.Clear();
            if (MoveNext != null)
                MoveNext(this, new EventArgs());
        }

        private void Backspace()
        {
            if (builder.Length > 0)
                builder.Remove(builder.Length - 1, 1);

            if (this.IsEmpty)
            {
                if (MovePrevious != null)
                    MovePrevious(this, new EventArgs());
            }
        }

        private bool IgnoreCharacter(char character)
        {
            // don't record any of these characters...
            char[] ignoreCharacters = new char[] { '\n', '\t', '\v', '\0', '\a', '\f' };
            if (ignoreCharacters.Contains(character))
                return true;

            //// don't worry about backspace if term is empty.
            //if (character == '\b' && IsEmpty)
            //    return true;

            return false;
        }

        private void Clear()
        {
            builder.Clear();
        }
        public override string ToString()
        {
            string str = builder.ToString();
            str = str.Replace(",", "");
            return str.Trim();
        }
    }

}
