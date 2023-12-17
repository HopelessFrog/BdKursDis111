using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BdKursDis.Model;
using BdKursDis.ViewModels.Interfaces;
using DevExpress.Mvvm;

namespace BdKursDis.ViewModels
{
    public class EditUnitViewModel : ViewModelBase , ICloseWindow
    {
        private bool Cancel = false;
        public Unit Unit { get; set; }
        public EditUnitViewModel(Unit unit)
        {
            Unit = unit;
        }
        public ICommand SaveUnitCommand
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
        public ICommand CancelSaveUnitCommand
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
            if (string.IsNullOrWhiteSpace(Unit.Name))
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
