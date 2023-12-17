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
    public  class EditStageViewModel :ViewModelBase , ICloseWindow
    {
        public EditStageViewModel(Stage stage)
        {
            Stage = stage;
        }

        public List<Equipment> Equipments
        {
            get
            {
                return DatabaseLocator.Context.Equipments.ToList();
            }
        }
        public Stage Stage { get; set; }
        public bool cancel = false;
        public Action Close { get; set; }
        public List<Unit> Units => DatabaseLocator.Context.Units.ToList();

        public ICommand SaveStageCommand
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
        public ICommand CancelSaveStageCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    DatabaseLocator.RollBack();
                    cancel = true;
                    Close();
                });
            }
        }
        public bool CanClose()
        {
            if (cancel)
                return true;
            if (string.IsNullOrWhiteSpace(Stage.Name) || Stage.Equipment == null || string.IsNullOrWhiteSpace(Stage.Description))
                return false;
            return true;
        }
    }
}

