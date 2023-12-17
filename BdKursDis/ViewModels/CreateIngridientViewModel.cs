using BdKursDis.Model;
using BdKursDis.ViewModels.Interfaces;
using DevExpress.Mvvm;
using System.Windows.Input;
using System.Windows;

namespace BdKursDis.ViewModels;

public class CreateIngridientViewModel : ViewModelBase, ICloseWindow
{

    public CreateIngridientViewModel()
    {
        Ingredient = new Ingredient();
    }
    public event EventHandler? Create;
    public Ingredient Ingredient { get; set; }

    public List<Unit> Units => DatabaseLocator.Context.Units.ToList();

    public Action Close { get; set; }

    public ICommand AddIngridientCommand
    {
        get
        {
            return new DelegateCommand(() =>
            {
                if (!string.IsNullOrWhiteSpace(Ingredient.Name) && Ingredient.Unit != null )
                {
                    DatabaseLocator.Context.Ingredients.Add(Ingredient);
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

    public bool CanClose()
    {
        return true;
    }
}