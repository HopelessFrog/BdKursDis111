using BdKursDis.Model;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BdKursDis.ViewModels.Interfaces;

namespace BdKursDis.ViewModels
{
    public  class CreateEqupmentViewModel : ViewModelBase , ICloseWindow
    {
        public CreateEqupmentViewModel()
        {
            Equipment = new Equipment();
        }

        public Equipment Equipment { get; set; }

        public event EventHandler? Create;
        public ICommand AddEquipCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (!string.IsNullOrWhiteSpace(Equipment.Name))
                    {
                        DatabaseLocator.Context.Equipments.Add(Equipment);
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
