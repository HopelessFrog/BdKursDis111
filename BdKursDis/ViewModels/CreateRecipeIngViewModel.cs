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
    public class CreateRecipeIngViewModel : ViewModelBase , ICloseWindow
    {
        private Product product;
        public CreateRecipeIngViewModel(Product product)
        {
            this.product = product;
            RecipeIngridient = new RecipeIngridient() { Product = product };
        }

        public event EventHandler? Create;
        public RecipeIngridient RecipeIngridient { get; set; }

        public List<Ingredient> Ingredients
        {
            get
            {
                return DatabaseLocator.Context.Ingredients.ToList();
            }
        }

        public ICommand AddRecipeIngridientCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (RecipeIngridient.Ingredient != null)
                    {
                        DatabaseLocator.Context.RecipeIngridients.Add(RecipeIngridient);
                        DatabaseLocator.Context.SaveChanges();
                        Create?.Invoke(this, EventArgs.Empty);

                        Close();
                    }
                    else
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show("Заполните ингридиент", "",
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
