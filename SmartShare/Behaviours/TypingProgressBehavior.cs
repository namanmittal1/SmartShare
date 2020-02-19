using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace SmartShare
{
    public static class TypingProgressBehavior
    {

        // Using a DependencyProperty as the backing store for IsTypingProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsTypingProperty =
            DependencyProperty.RegisterAttached("IsTyping", typeof(ICommand), typeof(TypingProgressBehavior),
                new FrameworkPropertyMetadata(null, new
                     PropertyChangedCallback(OnTypingBehavior)));

        public static void SetIsTyping(TextBox element, ICommand value)
        {
            element.SetValue(IsTypingProperty, value);
        }

        public static ICommand GetIsTyping(TextBox element)
        {
            return (ICommand)element.GetValue(IsTypingProperty);
        }

        private static void OnTypingBehavior(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            TextBox element = obj as TextBox;

            if (element == null)
                throw new InvalidOperationException();

            if (element != null)
            {
                if (e.NewValue != null)
                {
                    element.TextChanged += Element_TextChanged;
                }
                else
                {
                    element.TextChanged -= Element_TextChanged;
                }
            }
        }

        private static void Element_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox != null)
            {
                ICommand command = GetIsTyping(textBox);

                if (command.CanExecute(null))
                {
                    command.Execute(true);
                }
            }
        }
    }
}
