using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BdKursDis.Model;
using BdKursDis.ViewModels.Interfaces;
using DevExpress.Mvvm;

namespace BdKursDis.ViewModels
{
    public  class CraftViewModel : ViewModelBase , ICloseWindow
    {
        private Product Product;

        public CraftViewModel(Product product)
        {
            Product = product;
        }

        public List<RecipeIngridient> RecipeIngridients
        {
            get
            {
                return DatabaseLocator.Context.RecipeIngridients.Where(p =>p.Product == Product).ToList();
            }
        }
        public List<TechnicalStage> TechnicalStages
        {
            get
            {
                return DatabaseLocator.Context.TechnicalStages.Where(p => p.Product == Product).ToList();
            }
        }
        public Action Close { get; set; }
        public bool CanClose()
        {
            return true;
        }
    }
}
