using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DynamicTypeDescriptor;

using Dyn = DynamicTypeDescriptor;
using Scm = System.ComponentModel;
using System.Drawing.Design;
using CygSoft.Qik.LanguageEngine.Infrastructure;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    // http://www.codeproject.com/Articles/415070/Dynamic-Type-Description-Framework-for-PropertyGri

    public partial class InputPropertyGrid : UserControl
    {
        public event EventHandler InputChanged;

        private Dictionary<string, IQikControl> optionsDictionary = null;
        private Dictionary<string, IQikExpression> expressionDictionary = null;

        private const string CATEGORY_USER_INPUT = "1. User Input";
        private const string CATEGORY_EXPRESSION = "2. Expressions";

        public InputPropertyGrid()
        {
            InitializeComponent();
        }

        public void Reset(IQikControl[] controlList, IQikExpression[] expressionList)
        {
            optionsDictionary = new Dictionary<string, IQikControl>();
            expressionDictionary = new Dictionary<string, IQikExpression>();

            UserInputProperties properties = new UserInputProperties();
            Dyn.TypeDescriptor.IntallTypeDescriptor(properties);
            propertyGrid.SelectedObject = properties;

            foreach (IQikControl ctrl in controlList)
            {
                if (ctrl is IQikOptionBoxControl)
                {
                    CreateOptionsBox(ctrl as IQikOptionBoxControl);   
                }
                //else if (ctrl is QikCheckBoxControl)
                //{
                //    CreateCheckBox(ctrl as QikCheckBoxControl);
                //}
                else if (ctrl is IQikTextBoxControl)
                {
                    CreateTextBox(ctrl as IQikTextBoxControl);
                }
                optionsDictionary.Add(ctrl.Symbol, ctrl);
            }


            foreach (IQikExpression expression in expressionList)
            {
                CreateExpression(expression);
                expressionDictionary.Add(expression.Symbol, expression);
            }

            propertyGrid.Refresh();
        }

        private void CreateTextBox(IQikTextBoxControl textBox)
        {
            Dyn.TypeDescriptor typeDescriptor = Dyn.TypeDescriptor.GetTypeDescriptor(propertyGrid.SelectedObject);

            Dyn.PropertyDescriptor propertyDescriptor = new Dyn.PropertyDescriptor(propertyGrid.SelectedObject.GetType(),
                                                        textBox.Symbol,
                                                        typeof(string), textBox.DefaultValue,
                                                        new Scm.BrowsableAttribute(true),
                                                        new Scm.DisplayNameAttribute(textBox.Title),
                                                        new Scm.DescriptionAttribute("Insert Text"),
                                                        new Scm.DefaultValueAttribute(textBox.DefaultValue)
                                                        );
            propertyDescriptor.Attributes.Add(new Scm.CategoryAttribute(CATEGORY_USER_INPUT), true);
            propertyDescriptor.Attributes.Add(new PropertyControlAttribute(ControlTypeEnum.TextBox), true);
            propertyDescriptor.AddValueChanged(propertyGrid.SelectedObject, new EventHandler(this.InputPropertyChanged));

            typeDescriptor.GetProperties().Add(propertyDescriptor);
        }

        //private void CreateCheckBox(QikCheckBoxControl checkBox)
        //{
        //    Dyn.TypeDescriptor typeDescriptor = Dyn.TypeDescriptor.GetTypeDescriptor(propertyGrid.SelectedObject);

        //    Dyn.PropertyDescriptor propertyDescriptor = new Dyn.PropertyDescriptor(propertyGrid.SelectedObject.GetType(),
        //                                                checkBox.ControlId,
        //                                                typeof(bool), bool.Parse(checkBox.DefaultValue),
        //                                                new Scm.BrowsableAttribute(true),
        //                                                new Scm.DisplayNameAttribute(checkBox.Title),
        //                                                new Scm.DescriptionAttribute("Select true/false."),
        //                                                new Scm.DefaultValueAttribute(bool.Parse(checkBox.DefaultValue))
        //                                                );
        //    propertyDescriptor.Attributes.Add(new Scm.CategoryAttribute(CATEGORY_USER_INPUT), true);
        //    propertyDescriptor.Attributes.Add(new PropertyControlAttribute(ControlTypeEnum.CheckBox), true);

        //    propertyDescriptor.AddValueChanged(propertyGrid.SelectedObject, new EventHandler(this.InputPropertyChanged));
        //    typeDescriptor.GetProperties().Add(propertyDescriptor);
        //}

        private void CreateOptionsBox(IQikOptionBoxControl optionBox)
        {
            Dyn.TypeDescriptor typeDescriptor = Dyn.TypeDescriptor.GetTypeDescriptor(propertyGrid.SelectedObject);
            Dyn.PropertyDescriptor propertyDescriptor = new Dyn.PropertyDescriptor(propertyGrid.SelectedObject.GetType(),
                                                        optionBox.Symbol,
                                                        typeof(int), optionBox.SelectedIndex,
                                                        new Scm.BrowsableAttribute(true),
                                                        new Scm.DisplayNameAttribute(optionBox.Title),
                                                        new Scm.DescriptionAttribute("Select an option."),
                                                        new Scm.DefaultValueAttribute(optionBox.SelectedIndex)
                                                        );
            propertyDescriptor.Attributes.Add(new Scm.CategoryAttribute(CATEGORY_USER_INPUT), true);
            propertyDescriptor.Attributes.Add(new PropertyControlAttribute(ControlTypeEnum.OptionBox), true);

            propertyDescriptor.Attributes.Add(new Scm.TypeConverterAttribute(typeof(Dyn.StandardValueConverter)), true);
            propertyDescriptor.Attributes.Add(new Scm.EditorAttribute(typeof(Dyn.StandardValueEditor), typeof(UITypeEditor)), true);

            BuildOptions(propertyDescriptor, optionBox.Options);

            propertyDescriptor.AddValueChanged(propertyGrid.SelectedObject, new EventHandler(this.InputPropertyChanged));
            typeDescriptor.GetProperties().Add(propertyDescriptor);
        }

        private void CreateExpression(IQikExpression expression)
        {
            Dyn.TypeDescriptor typeDescriptor = Dyn.TypeDescriptor.GetTypeDescriptor(propertyGrid.SelectedObject);

            Dyn.PropertyDescriptor propertyDescriptor = new Dyn.PropertyDescriptor(propertyGrid.SelectedObject.GetType(),
                                                        expression.Symbol,
                                                        typeof(string), expression.Execute(),
                                                        //typeof(string), null,
                                                        new Scm.BrowsableAttribute(true),
                                                        new Scm.DisplayNameAttribute(expression.Title),
                                                        new Scm.DescriptionAttribute("Derived expression."),
                                                        new Scm.DefaultValueAttribute(null),
                                                        new Scm.ReadOnlyAttribute(true)
                                                        );
            propertyDescriptor.Attributes.Add(new Scm.CategoryAttribute(CATEGORY_EXPRESSION), true);
            propertyDescriptor.Attributes.Add(new PropertyControlAttribute(ControlTypeEnum.ExpressionBox), true);
            
            // If you don't want to raise  an "InputPropertyChanged" event for this property, then don't add a delegate.
            // Also, you'll be able to know exactly what property changed by having different handlers for different property
            // descriptors... good thing to know for the future !!!
            //propertyDescriptor.AddValueChanged(propertyGrid.SelectedObject, new EventHandler(this.InputPropertyChanged));

            typeDescriptor.GetProperties().Add(propertyDescriptor);
        }

        private void BuildOptions(Dyn.PropertyDescriptor pd, IQikOptionBoxOption[] options)
        {
            pd.StandardValues.Clear();

            foreach (IQikOptionBoxOption option in options)
            {
                Dyn.StandardValue sv = new Dyn.StandardValue(option.Index, option.Value);
                sv.Description = "Description of " + sv.DisplayName + ".";
                pd.StandardValues.Add(sv);
            }
        }

        private void InputPropertyChanged(object sender, EventArgs e)
        {
            UserInputProperties userInputProperties = sender as UserInputProperties;
            Dyn.TypeDescriptor typeDescriptor = Dyn.TypeDescriptor.GetTypeDescriptor(propertyGrid.SelectedObject);
            PropertyDescriptorCollection propertyDescriptors = typeDescriptor.GetProperties();

            foreach (Dyn.PropertyDescriptor propertyDescriptor in propertyDescriptors)
            {
                PropertyControlAttribute propertyControl = propertyDescriptor.Attributes[typeof(PropertyControlAttribute)] as PropertyControlAttribute;
                if (propertyControl != null && propertyControl.ControlType == ControlTypeEnum.TextBox)
                {
                    string name = propertyDescriptor.Name;
                    object value = propertyDescriptor.GetValue(userInputProperties);

                    IQikTextBoxControl textBox = optionsDictionary[name] as IQikTextBoxControl;
                    textBox.SetCurrentValue(value != null ? value.ToString() : null);
                }
                else if (propertyControl != null && propertyControl.ControlType == ControlTypeEnum.OptionBox)
                {
                    string name = propertyDescriptor.Name;
                    object value = propertyDescriptor.GetValue(userInputProperties);

                    IQikOptionBoxControl optionBox = optionsDictionary[name] as IQikOptionBoxControl;
                    if (optionBox != null)
                    {
                        if (value != null)
                            optionBox.SelectOption(int.Parse(value.ToString()));
                        else
                            optionBox.ClearSelection(false);
                    }
                }
                //else if (propertyControl != null && propertyControl.ControlType == ControlTypeEnum.CheckBox)
                //{
                //    string name = propertyDescriptor.Name;
                //    string value = propertyDescriptor.GetValue(userInputProperties).ToString();

                //    QikCheckBoxControl checkBox = optionsDictionary[name] as QikCheckBoxControl;
                //    checkBox.SetCurrentValue(value);
                //}
            }

            CalculateExpressions(userInputProperties);
            propertyGrid.Refresh();
        }

        private void CalculateExpressions(UserInputProperties userInputProperties)
        {
            Dyn.TypeDescriptor typeDescriptor = Dyn.TypeDescriptor.GetTypeDescriptor(propertyGrid.SelectedObject);
            PropertyDescriptorCollection propertyDescriptors = typeDescriptor.GetProperties();

            foreach (Dyn.PropertyDescriptor propertyDescriptor in propertyDescriptors)
            {
                PropertyControlAttribute propertyControl = propertyDescriptor.Attributes[typeof(PropertyControlAttribute)] as PropertyControlAttribute;
                if (propertyControl != null && propertyControl.ControlType == ControlTypeEnum.ExpressionBox)
                {
                    string name = propertyDescriptor.Name;
                    //string value = propertyDescriptor.GetValue(userInputProperties).ToString();

                    IQikExpression expression = expressionDictionary[name] as IQikExpression;
                    string newValue = expression.Execute();
                    //propertyDescriptor.SetValue(userInputProperties, newValue);
                    propertyDescriptor.SetValue(userInputProperties, newValue == null ? string.Empty : newValue);
                }
            }
        }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(e.ChangedItem.Label);
            if (InputChanged != null)
                InputChanged(this, new EventArgs());
        }
    }
}
