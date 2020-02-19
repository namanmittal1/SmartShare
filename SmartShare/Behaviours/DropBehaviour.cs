using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SmartShare
{
    public static class DropBehaviour
    {
        public static void SetDropCommand(UIElement inUIElement, ICommand inCommand)
        {
            inUIElement.SetValue(DropCommandProperty, inCommand);
        }

        private static ICommand GetDropCommand(UIElement inUIElement)
        {
            return (ICommand)inUIElement.GetValue(DropCommandProperty);
        }


        public static readonly DependencyProperty DropCommandProperty =
            DependencyProperty.RegisterAttached("DropCommand", typeof(ICommand), typeof(DropBehaviour), 
            new FrameworkPropertyMetadata(DropCommandPropertyChangedCallback));
    
        private static void DropCommandPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement uiElement = d as UIElement;
            if (null == uiElement) return;

            uiElement.Drop += (sender, args) =>
            {
                GetDropCommand(uiElement).Execute(args.Data);
                args.Handled = true;
            };
        }

        
    }
}
