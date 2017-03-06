using System;

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
