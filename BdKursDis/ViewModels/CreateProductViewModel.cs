using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BdKursDis.Model;
using MaterialDesignColors;
using System.Windows.Input;
using System.Windows;
using BdKursDis.ViewModels.Interfaces;

namespace BdKursDis.ViewModels
{
    public class CreateProductViewModel : ViewModelBase , ICloseWindow
    {
        public CreateProductViewModel()
        {
            Product = new Product();
        }

        public event EventHandler? Create;
        public Product Product { get; set; }

        public ICommand AddProductCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (!string.IsNullOrWhiteSpace(Product.Name))
                    {
                        DatabaseLocator.Context.Products.Add(Product);
                        DatabaseLocator.Context.SaveChanges();
                        Create?.Invoke(this, EventArgs.Empty);
                       
                        Close();
                    }
                    else
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show("Заполните название", "",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                });
            }
        }

        public Action? Close { get; set; }
        public bool CanClose()
        {
            return true;
        }
    }
}
