using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace MNAIS.Views
{
    /// <summary>
    /// Interaction logic for PricingOptions.xaml
    /// </summary>
    public partial class PricingOptions : UserControl
    {
        public PricingOptions()
        {
            InitializeComponent();
        }

        private void SelectAll(object sender, MouseButtonEventArgs e)
        {
            TextBox tb = (sender as TextBox);

            if (tb == null)
                return;

            if (!tb.IsKeyboardFocusWithin)
            {
                tb.SelectAll();
                e.Handled = true;
                tb.Focus();
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            tb.SelectAll();
        }
    }
}
