using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BdKursDis.Model;
using System.Windows.Input;
using BdKursDis.ViewModels.Interfaces;
using DevExpress.Mvvm;

namespace BdKursDis.ViewModels
{
    public  class EditIngridientViewModel : ViewModelBase , ICloseWindow
    {
        public EditIngridientViewModel(Ingredient ingredient)
        {
            Ingredient = ingredient;
        }
        public Ingredient Ingredient { get; set; }
        public bool cancel = false;
        public Action Close { get; set; }
        public List<Unit> Units => DatabaseLocator.Context.Units.ToList();

        public ICommand SaveIngridientCommand
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
        public ICommand CancelSaveIngridientCommand
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
            if(cancel)
                return true;
            if(string.IsNullOrWhiteSpace(Ingredient.Name) || Ingredient.Unit == null)
                return false;
            return true;
        }
    }
}
