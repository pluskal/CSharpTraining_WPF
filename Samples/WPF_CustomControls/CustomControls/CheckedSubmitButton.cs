using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CustomControls { 

    public class CheckedSubmitButton : Control
    {
        public static readonly DependencyProperty CheckTextProperty = DependencyProperty.Register(
            nameof(CheckText), typeof(string), typeof(CheckedSubmitButton), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty ButtonTextProperty = DependencyProperty.Register(
            nameof(ButtonText), typeof(string), typeof(CheckedSubmitButton), new PropertyMetadata(default(string)));


        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            nameof(Command), typeof(ICommand), typeof(CheckedSubmitButton), new PropertyMetadata(default(ICommand)));


        private CheckBox _checkedSubmitButton_CheckBox;
        private Button _checkedSubmitButton_Button;

        public event EventHandler<RoutedEventArgs> Checked; 
        public event EventHandler<RoutedEventArgs> Clicked; 

        static CheckedSubmitButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CheckedSubmitButton),
                new FrameworkPropertyMetadata(typeof(CheckedSubmitButton)));

        }

        public override void OnApplyTemplate()
        {
            this._checkedSubmitButton_CheckBox = this.GetTemplateChild<CheckBox>("CheckedSubmitButton_CheckBox");
            this._checkedSubmitButton_Button = this.GetTemplateChild<Button>("CheckedSubmitButton_Button");

            this._checkedSubmitButton_CheckBox.Checked += (sender, args) => this.Checked?.Invoke(sender, args);
            this._checkedSubmitButton_Button.Click += (sender, args) => this.Clicked?.Invoke(sender, args);
        }

        public string CheckText
        {
            get => (string) this.GetValue(CheckTextProperty);
            set => this.SetValue(CheckTextProperty, value);
        }

        public string ButtonText
        {
            get => (string) this.GetValue(ButtonTextProperty);
            set => this.SetValue(ButtonTextProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand) this.GetValue(CommandProperty);
            set => this.SetValue(CommandProperty, value);
        }

        T GetTemplateChild<T>(string name) where T : DependencyObject
        {
            if (!(this.GetTemplateChild(name) is T child))
            {
                throw new NullReferenceException(name);
            }
            return child;
        }
    }
}