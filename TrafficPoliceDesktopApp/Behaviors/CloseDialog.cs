using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace TrafficPoliceDesktopApp.Behaviors
{
    public static class CloseDialog
    {
        public static readonly DependencyProperty DialogResultProperty = DependencyProperty.RegisterAttached("DialogResult", typeof(Boolean?), typeof(CloseDialog), new PropertyMetadata(DialogResultChanged));

        private static void DialogResultChanged(DependencyObject pDependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var wndWindow = pDependencyObject as Window;
            Boolean? blnIsModal = System.Windows.Interop.ComponentDispatcher.IsThreadModal;
            if (wndWindow != null)
                if (blnIsModal == true)
                    wndWindow.DialogResult = e.NewValue as Boolean?;
                else
                    wndWindow.Close();
        }

        public static void SetDialogResult(Window pTarget, Boolean? pblnDialogResult)
        {
            pTarget.SetValue(DialogResultProperty, pblnDialogResult);
        }
    }
}
