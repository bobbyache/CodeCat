using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public enum ControlTypeEnum
    {
        TextBox,
        //CheckBox,
        OptionBox,
        ExpressionBox
    }

    public class PropertyControlAttribute : Attribute
    {
        public ControlTypeEnum ControlType { get; private set; }
        public PropertyControlAttribute(ControlTypeEnum controlType)
        {
            this.ControlType = controlType;
        }
    }
}
