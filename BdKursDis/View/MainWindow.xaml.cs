using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BdKursDis.Model;
using BdKursDis.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BdKursDis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FocusableChanged += MainWindow_FocusableChanged;
        }

        private void MainWindow_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            (this.DataContext as MainViewModel).SearchProduct.Execute(this);
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
