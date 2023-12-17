using BdKursDis.Model;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BdKursDis.ViewModels.Interfaces;

namespace BdKursDis.ViewModels
{
    public class EditEquipmentViewModel : ViewModelBase , ICloseWindow
    {
        private bool Cancel = false;
        public Equipment Equipment { get; set; }
        public EditEquipmentViewModel(Equipment equipment)
        {
            Equipment = equipment;
        }
        public ICommand SaveEquipmentCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    DatabaseLocator.Context.SaveChanges();
                    Close();
                });
            }
        }
        public ICommand CancelSaveEquipmentCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    DatabaseLocator.RollBack();
                    Cancel = true;
                    Close();
                });
            }
        }

        public Action Close { get; set; }
        public bool CanClose()
        {
            if (Cancel)
                return true;
            if (string.IsNullOrWhiteSpace(Equipment.Name))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
