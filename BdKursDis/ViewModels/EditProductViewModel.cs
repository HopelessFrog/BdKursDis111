using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BdKursDis.Model;
using BdKursDis.View;
using BdKursDis.ViewModels.Interfaces;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using Microsoft.EntityFrameworkCore;

namespace BdKursDis.ViewModels
{
    public  class EditProductViewModel : ViewModelBase , ICloseWindow
    {
        private User _user;
        public Visibility Tech { get; set; } = Visibility.Collapsed;
        public Visibility Cook { get; set; } = Visibility.Collapsed;

        private bool cancel = false;
        public EditProductViewModel(Product product, User user)
        {
            Product = product;
            this._user = user;
            if (_user.IsCook)
                Cook = Visibility.Visible;

            if (_user.IsOperator)
                Tech = Visibility.Visible;
        }
        public ICommand SaveProductCommand
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
       
        public ICommand CancelSaveProductCommand
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
        public ICommand AddStageTech
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    var window = new CreateTechnicalStage();
                    window.Closed += WindowOnClosedTech;
                    window.DataContext = new CreateTechnicalStageViewModel(Product);
                    window.ShowDialog();
                });
            }
        }
        public ICommand AddRecipeIng
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    var window = new CreateRecipeIngView();
                    window.Closed += ClosedR;
                    window.DataContext = new CreateRecipeIngViewModel(Product);
                    window.ShowDialog();
                });
            }
        }

        public ICommand DeleteRecipeIngridientsCommand
        {
            get
            {
                return new DelegateCommand<RecipeIngridient>((recipeIngridients) =>
                {
                    DatabaseLocator.Context.RecipeIngridients.Remove(recipeIngridients);
                    DatabaseLocator.Context.SaveChanges();
                    RaisePropertyChanged(nameof(RecipeIngridients));
                });
            }
        }
        public ICommand DeleteStageTechCommand
        {
            get
            {
                return new DelegateCommand<TechnicalStage>((technicalStage) =>
                {
                    DatabaseLocator.Context.TechnicalStages.Remove(technicalStage);
                    DatabaseLocator.Context.SaveChanges();
                    RaisePropertyChanged(nameof(TechnicalStages));
                });
            }
        }

        private void ClosedR(object? sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(RecipeIngridients));
        }

        private void WindowOnClosedTech(object? sender, EventArgs e)
        {
           RaisePropertyChanged(nameof(TechnicalStages));
        }

        
        public List<Ingredient> Ingredients
        {
            get
            {
                return DatabaseLocator.Context.Ingredients.ToList();
            }
        }
        public Product Product { get; set; }

        public List<Stage> Stages
        {
            get
            {
                return DatabaseLocator.Context.Stages.ToList();
            }
        }

        public List<RecipeIngridient> RecipeIngridients
        {
            get
            {
                return DatabaseLocator.Context.RecipeIngridients.Where(s => s.Product == Product).ToList();
            }
        }

        public List<TechnicalStage> TechnicalStages 
        {
            get
            {
                return DatabaseLocator.Context.TechnicalStages.Where(s => s.Product == Product).ToList();
            }
        }
        public Action Close { get; set; }
        public bool CanClose()
        {
            if (cancel)
                return true;
            if(string.IsNullOrWhiteSpace(Product.Name)) 
                return false;
            return true;
        }
    }
}
