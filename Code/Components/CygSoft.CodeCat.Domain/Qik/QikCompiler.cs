using CygSoft.Qik.LanguageEngine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.Qik
{
    public class QikCompiler
    {
        CygSoft.Qik.LanguageEngine.Qik qik;

        public IQikExpression[] Expressions { get { return qik.Expressions; } }

        public IQikControl[] Controls { get { return qik.Controls; } }

        public string[] Placeholders { get { return qik.Placeholders; } }

        public string GetOutput(string placeholder)
        {
            return qik.FindOutput(placeholder);
        }

        public string GetTitle(string placeholder)
        {
            return qik.FindTitle(placeholder);
        }

        public void Compile(string script)
        {
            qik = new CygSoft.Qik.LanguageEngine.Qik();
            qik.ExecuteScript(script);
        }
    }
}
