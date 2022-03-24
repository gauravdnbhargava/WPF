using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MNAIS.Utility
{
    public class DataGridDynamicColumn : DataGridTemplateColumn
    {
        protected override object PrepareCellForEdit(FrameworkElement editingElement, RoutedEventArgs editingEventArgs)
        {
            editingElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Last));
            TextBox txtBox = GetVisualChild<TextBox>(editingElement);
            txtBox.SelectAll();
            return base.PrepareCellForEdit(editingElement, editingEventArgs);
        }

        static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

    }
}
