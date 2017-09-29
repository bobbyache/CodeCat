using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Plugins.ManualXess
{
    public class Variable
    {
        public string Placeholder { get; private set; }
        public string Name { get; private set; }

        public Variable(string placeholder)
        {
            this.Placeholder = placeholder;
            this.Name = GetNameFromPlaceholder(placeholder);
        }

        private string GetNameFromPlaceholder(string placeholder)
        {
            if (placeholder.Length > 3)
            {
                return placeholder.Substring(2, placeholder.Length - 3);
            }
            return string.Empty;
        }
    }
}
