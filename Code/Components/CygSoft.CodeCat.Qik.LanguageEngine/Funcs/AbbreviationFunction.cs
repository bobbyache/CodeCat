using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using CygSoft.CodeCat.Qik.LanguageEngine.Scope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CygSoft.CodeCat.Qik.LanguageEngine.Funcs
{
    internal class AbbreviateFunction : BaseFunction
    {

        public AbbreviateFunction(FuncInfo funcInfo, GlobalTable scopeTable, List<BaseFunction> functionArguments)
            : base(funcInfo, scopeTable, functionArguments)
        {

        }

        public override string Execute(IErrorReport errorReport)
        {
            if (functionArguments.Count() != 1)
                errorReport.AddError(new CustomError(this.Line, this.Column, "Too many arguments", this.Name));

            string result = null;

            try
            {
                string txt = functionArguments[0].Execute(errorReport);

                if (!string.IsNullOrWhiteSpace(txt))
                {
                    var builder = new StringBuilder();

                    foreach (var chr in txt.ToCharArray())
                    {
                        if (char.IsUpper(chr))
                        {
                            builder.Append(' ');
                            builder.Append(chr);

                        }
                        else if (chr == '_')
                        {
                            builder.Append(' ');
                        }
                        else
                        {
                            builder.Append(chr);
                        }
                    }

                    var results = builder.ToString()
                        .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(str => str[0]).ToArray();

                    result = new string(results);
                    return result;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                errorReport.AddError(new CustomError(this.Line, this.Column, "Bad function call.", this.Name));
            }
            return result;
        }
    }
}
