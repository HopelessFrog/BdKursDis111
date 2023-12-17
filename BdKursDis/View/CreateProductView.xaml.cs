using BdKursDis.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BdKursDis.View
{
    /// <summary>
    /// Interaction logic for CreateProductView.xaml
    /// </summary>
    public partial class CreateProductView : Window
    {
        public CreateProductView()
        {
            InitializeComponent();
            Loaded += CreateProductView_Loaded;
        }

        private void CreateProductView_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ICloseWindow viewModel)
            {
                viewModel.Close +=
                    () => { this.Close(); };

                Closing += (s, e) => { e.Cancel = !viewModel.CanClose(); };
            }
        }

        private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Text))
            {
                string newText = (sender as TextBox).Text + e.Text;
                if (!int.TryParse(newText, out int result) || result > 99)
                {
                    e.Handled = true;
                    (sender as TextBox).Text = "99";
                }
            }
        }
    }
}
