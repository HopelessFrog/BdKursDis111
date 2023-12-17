using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using BdKursDis.Model;
using BdKursDis.ViewModels.Interfaces;
using DevExpress.Mvvm;

namespace BdKursDis.ViewModels
{
    public class CreateUnitsViewModel :ViewModelBase , ICloseWindow
    {

        public CreateUnitsViewModel()
        {
            Unit = new Unit();
        }

        public Unit Unit { get; set; }

        public event EventHandler? Create;
        public ICommand AddUnitCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (!string.IsNullOrWhiteSpace(Unit.Name))
                    {
                        DatabaseLocator.Context.Units.Add(Unit);
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


        public Action Close { get; set; }
        public bool CanClose()
        {
            return true;
        }
    }
}
