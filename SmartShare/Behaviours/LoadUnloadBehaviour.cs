using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SmartShare
{
    public static class LoadUnloadBehaviour
    {
        public static readonly DependencyProperty LoadUnloadProperty =
       DependencyProperty.RegisterAttached("LoadUnload", typeof(Boolean),
       typeof(LoadUnloadBehaviour),
       new FrameworkPropertyMetadata(false,
           new PropertyChangedCallback(OnLoadUnloadChanged)));

        public static void SetLoadUnload(FrameworkElement element, Boolean value)
        {
            element.SetValue(LoadUnloadProperty, value);
        }

        public static Boolean GetLoadUnload(FrameworkElement element)
        {
            return (Boolean)element.GetValue(LoadUnloadProperty);
        }

        public static void OnLoadUnloadChanged(DependencyObject obj,
            DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement element = obj as FrameworkElement;

            if (element == null)
                throw new InvalidOperationException();

            element.DataContextChanged += (sender, e) =>
            {
                if (!element.IsLoaded)
                    return;

                if (e.OldValue is BaseViewModel)
                {
                    BaseViewModel viewModel = ((BaseViewModel)e.OldValue);
                    if (viewModel.Initialized)
                    {
                        viewModel.Unload(element);
                        viewModel.Initialized = false;
                    }
                }

                if (e.NewValue is BaseViewModel)
                {
                    BaseViewModel viewModel = ((BaseViewModel)e.NewValue);
                    if (!viewModel.Initialized)
                    {
                        viewModel.Initialized = true;
                        viewModel.Load(element);
                    }
                }
            };

            element.Loaded += (sender, e) =>
            {
                BaseViewModel viewModel =
                    element.GetValue(FrameworkElement.DataContextProperty) as BaseViewModel;

                if (viewModel != null && !viewModel.Initialized)
                {
                    viewModel.Initialized = true;
                    viewModel.Load(element);
                }
            };

            element.Unloaded += (sender, e) =>
            {
                BaseViewModel viewModel =
                    element.GetValue(FrameworkElement.DataContextProperty) as BaseViewModel;

                if (viewModel != null && viewModel.Initialized)
                {
                    viewModel.Unload(element);
                    viewModel.Initialized = false;
                }
            };
        }
    }
}
