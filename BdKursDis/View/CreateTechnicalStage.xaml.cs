using BdKursDis.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for CreateTechnicalStage.xaml
    /// </summary>
    public partial class CreateTechnicalStage : Window
    {
        public CreateTechnicalStage()
        {
            InitializeComponent();
            Loaded += CreateTechnicalStage_Loaded;
        }

        private void CreateTechnicalStage_Loaded(object sender, RoutedEventArgs e)
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
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void FrameworkElement_OnContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
