using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using BdKursDis.Model;
using DevExpress.Mvvm;
using BdKursDis.View;
using Microsoft.EntityFrameworkCore;

namespace BdKursDis.ViewModels
{
    public  class MainViewModel : ViewModelBase
    {
        private User User {get; set; }

        public string SearchStageD { get; set; }
        public string SerchName { get; set; }

        public int SearchLoverStr { get; set; }
        public int SearchHigherStr { get; set; }

        public Visibility Tech { get; set; } = Visibility.Collapsed;
        public Visibility Cook { get; set; } = Visibility.Collapsed;

        public MainViewModel(User user)
        {

            this.User = user;
            if(User.IsCook)
                Cook = Visibility.Visible;

            if(User.IsOperator)
                Tech = Visibility.Visible;
            Products = new ObservableCollection<Product>(DatabaseLocator.Context.Products.ToList());
            Units = new ObservableCollection<Unit>(DatabaseLocator.Context.Units.ToList());
            Ingredients = new ObservableCollection<Ingredient>(DatabaseLocator.Context.Ingredients.ToList());
            Equipments = new ObservableCollection<Equipment>(DatabaseLocator.Context.Equipments.ToList());
            Stages = new ObservableCollection<Stage>(DatabaseLocator.Context.Stages.ToList());
            Products.CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null)
                    DatabaseLocator.Context.Products.AddRange(e.NewItems.Cast<Product>());
                else if (e.OldItems != null)
                    DatabaseLocator.Context.Products.RemoveRange(e.OldItems.Cast<Product>());
                else if (e.Action == NotifyCollectionChangedAction.Replace)
                {
                    foreach (Product item in e!.NewItems!)
                    {
                        DatabaseLocator.Context.Products.Entry(item).State = EntityState.Modified;
                    }
                }
                DatabaseLocator.Context.SaveChanges();
                DatabaseLocator.Context.SaveChanges();

            };
            Units.CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null)
                    DatabaseLocator.Context.Units.AddRange(e.NewItems.Cast<Unit>());
                else if (e.OldItems != null)
                    DatabaseLocator.Context.Units.RemoveRange(e.OldItems.Cast<Unit>());
                else if (e.Action == NotifyCollectionChangedAction.Replace)
                {
                    foreach (Unit item in e!.NewItems!)
                    {
                        DatabaseLocator.Context.Units.Entry(item).State = EntityState.Modified;
                    }
                }
                DatabaseLocator.Context.SaveChanges();
                DatabaseLocator.Context.SaveChanges();

            };

            Ingredients.CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null)
                    DatabaseLocator.Context.Ingredients.AddRange(e.NewItems.Cast<Ingredient>());
                else if (e.OldItems != null)
                    DatabaseLocator.Context.Ingredients.RemoveRange(e.OldItems.Cast<Ingredient>());
                else if (e.Action == NotifyCollectionChangedAction.Replace)
                {
                    foreach (Ingredient item in e!.NewItems!)
                    {
                        DatabaseLocator.Context.Ingredients.Entry(item).State = EntityState.Modified;
                    }
                }
                DatabaseLocator.Context.SaveChanges();
                DatabaseLocator.Context.SaveChanges();

            };
            Equipments.CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null)
                    DatabaseLocator.Context.Equipments.AddRange(e.NewItems.Cast<Equipment>());
                else if (e.OldItems != null)
                    DatabaseLocator.Context.Equipments.RemoveRange(e.OldItems.Cast<Equipment>());
                else if (e.Action == NotifyCollectionChangedAction.Replace)
                {
                    foreach (Equipment item in e!.NewItems!)
                    {
                        DatabaseLocator.Context.Equipments.Entry(item).State = EntityState.Modified;
                    }
                }
                DatabaseLocator.Context.SaveChanges();
                DatabaseLocator.Context.SaveChanges();

            };
            Stages.CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null)
                    DatabaseLocator.Context.Stages.AddRange(e.NewItems.Cast<Stage>());
                else if (e.OldItems != null)
                    DatabaseLocator.Context.Stages.RemoveRange(e.OldItems.Cast<Stage>());
                else if (e.Action == NotifyCollectionChangedAction.Replace)
                {
                    foreach (Stage item in e!.NewItems!)
                    {
                        DatabaseLocator.Context.Stages.Entry(item).State = EntityState.Modified;
                    }
                }
                DatabaseLocator.Context.SaveChanges();
                DatabaseLocator.Context.SaveChanges();

            };



        }

        public ObservableCollection<Product> Products { get; set; } 
        public ObservableCollection<Unit> Units { get; set; }
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        public ObservableCollection<Equipment> Equipments { get; set; }

        public ObservableCollection<Stage> Stages { get; set; }


        public ICommand AddProductCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    var window = new CreateProductView();
                    (window.DataContext as CreateProductViewModel).Create += MainViewModel_Create;
                    window.Show();
                });
            }
        }

        public ICommand AddUnitCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    var window = new CreateUnitsView();
                    (window.DataContext as CreateUnitsViewModel).Create += MainViewModel_Create1; ;
                    window.Show();
                });
            }
        }
        public ICommand AddEquipCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    var window = new CreateEqupmentView();
                    (window.DataContext as CreateEqupmentViewModel).Create += MainViewModel_CreateEquip; ; ;
                    window.Show();
                });
            }
        }

        private void MainViewModel_CreateEquip(object? sender, EventArgs e)
        {
            SearchEquipment.Execute(this);
        }

        public ICommand AddIngridientCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    var window = new CreateIngridientView();
                    (window.DataContext as CreateIngridientViewModel).Create += MainViewModel_CreateIngridient; 
                    window.Show();
                });
            }
        }

        public ICommand AddStageCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    var window = new CreateStageView();
                    (window.DataContext as CreateStageViewModel).Create += OnCreateStage;
                    window.Show();
                });
            }
        }

        private void OnCreateStage(object? sender, EventArgs e)
        {
            SearchStage.Execute(this);
        }

       
        private void MainViewModel_CreateIngridient(object? sender, EventArgs e)
        {
            Ingredients = new ObservableCollection<Ingredient>(DatabaseLocator.Context.Ingredients.ToList());
        }

        public ICommand EditProductCommand
        {
            get
            {
                return new DelegateCommand<Product>((product) =>
                {
                    var window = new EditProductView();
                    window.Closed += Window_Closed;
                    window.DataContext = new EditProductViewModel(product , User);
                    window.ShowDialog();
                });
            }
        }

        public ICommand EditStageCommand
        {
            get
            {
                return new DelegateCommand<Stage>((stage) =>
                {
                    var window = new EditStageView();
                    window.Closed +=CloseStage;
                    window.DataContext = new EditStageViewModel(stage);
                    window.ShowDialog();
                });
            }
        }

        private void CloseStage(object? sender, EventArgs e)
        {
            SearchStage.Execute(this);
        }


        public ICommand DeleteStageCommand
        {
            get
            {
                return new DelegateCommand<Stage>((stage) =>
                {
                    DatabaseLocator.Context.Stages.Remove(stage);
                    DatabaseLocator.Context.SaveChanges();
                    SearchStage.Execute(this);
                });
            }
        }

        public ICommand Craft
        {
            get
            {
                return new DelegateCommand<Product>((product) =>
                {
                    var window = new CraftView();
                    window.DataContext = new CraftViewModel(product);
                    window.ShowDialog();
                });
            }
        }

        public ICommand EditUnitCommand
        {
            get
            {
                return new DelegateCommand<Unit>((unit) =>
                {
                    var window = new EditUnitView();
                    window.Closed += Window_Closedunit;
                    window.DataContext = new EditUnitViewModel(unit);
                    window.ShowDialog();
                });
            }
        }


        public ICommand DeleteUnitCommand
        {
            get
            {
                return new DelegateCommand<Unit>((unit) =>
                {
                    DatabaseLocator.Context.Units.Remove(unit);
                    DatabaseLocator.Context.SaveChanges();
                    Units = new ObservableCollection<Unit>(DatabaseLocator.Context.Units.ToList());
                });
            }
        }
        public ICommand EditEquipmentCommand
        {
            get
            {
                return new DelegateCommand<Equipment>((equipment) =>
                {
                    var window = new EditEquipmentView();
                    window.Closed += Window_ClosedEquip; ;
                    window.DataContext = new EditEquipmentViewModel(equipment);
                    window.ShowDialog();
                });
            }
        }

        private void Window_ClosedEquip(object? sender, EventArgs e)
        {
            SearchEquipment.Execute(this);

        }

        public ICommand DeleteEquipmentCommand
        {
            get
            {
                return new DelegateCommand<Equipment>((equipment) =>
                {
                    DatabaseLocator.Context.Equipments.Remove(equipment);
                    DatabaseLocator.Context.SaveChanges();
                    SearchEquipment.Execute(this);
                });
            }
        }

        public ICommand SearchStage
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Stages = new ObservableCollection<Stage>(DatabaseLocator.Context.Stages.ToList());
                    if (!string.IsNullOrWhiteSpace(SearchStageD))
                    {
                        Stages = new ObservableCollection<Stage>( Stages.Where(s=>s.Name.Contains(SearchStageD)).ToList());
                    }
                });
            }
        }
        public string EquipmentSearch { get; set; }
        public ICommand SearchEquipment
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Equipments = new ObservableCollection<Equipment>(DatabaseLocator.Context.Equipments.ToList());
                    if (!string.IsNullOrWhiteSpace(EquipmentSearch))
                    {
                        Equipments = new ObservableCollection<Equipment>(Equipments.Where(e => e.Name.Contains(EquipmentSearch)).ToList());
                    }
                });
            }
        }
        public ICommand EditIngridientCommand
        {
            get
            {
                return new DelegateCommand<Ingredient>((ingredient) =>
                {
                    var window = new EditIngridientView();
                    window.Closed += Window_ClosedIngridient;
                    window.DataContext = new EditIngridientViewModel(ingredient);
                    window.ShowDialog();
                });
            }
        }
        public string IngridientSearch { get; set; }
        public ICommand SearchIngridient
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Ingredients = new ObservableCollection<Ingredient>(DatabaseLocator.Context.Ingredients.ToList());
                    if (!string.IsNullOrWhiteSpace(IngridientSearch))
                    {
                        Ingredients =
                            new ObservableCollection<Ingredient>(Ingredients
                                .Where(i => i.Name.Contains(IngridientSearch)).ToList());
                    }
                });
            }
        }
        private void Window_ClosedIngridient(object? sender, EventArgs e)
        {
            SearchIngridient.Execute(this);
        }

        public ICommand DeleteIngridientCommand
        {
            get
            {
                return new DelegateCommand<Ingredient>((ingredient) =>
                {
                   DatabaseLocator.Context.Ingredients.Remove(ingredient);
                   SearchIngridient.Execute(this);

                });
            }
        }
        private void Window_Closedunit(object? sender, EventArgs e)
        {
            Units = new ObservableCollection<Unit>(DatabaseLocator.Context.Units.ToList());
        }

        private void Window_Closed(object? sender, EventArgs e)
        {
            SearchProduct.Execute(this);
        }

        private void MainViewModel_Create1(object? sender, EventArgs e)
        {
           Units = new ObservableCollection<Unit>(DatabaseLocator.Context.Units.ToList());
        }

        private void MainViewModel_Create(object? sender, EventArgs e)
        {
            Products = new ObservableCollection<Product>(DatabaseLocator.Context.Products.ToList());
        }

        public ICommand SearchProduct
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Products = new ObservableCollection<Product>(DatabaseLocator.Context.Products.ToList());
                    if (!string.IsNullOrEmpty(SerchName))
                    {
                        Products = new ObservableCollection<Product>(Products.Where(p => p.Name.Contains(SerchName))
                            .ToList());
                    }

                    if (SearchHigherStr != 0)
                    {
                        Products = new ObservableCollection<Product>(Products.Where(p =>
                            p.Strength <= SearchHigherStr));
                    }

                    Products = new ObservableCollection<Product>(Products.Where(p =>
                        p.Strength >= SearchLoverStr));
                });
            }
        }
    }
}
