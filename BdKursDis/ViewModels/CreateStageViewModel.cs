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
     public  class CreateStageViewModel  : ViewModelBase , ICloseWindow
    {
        public CreateStageViewModel()
        {
            Stage = new Stage();
        }

        public Stage Stage { get; set; }

        public List<Equipment> Equipments
        {
            get
            {
                return DatabaseLocator.Context.Equipments.ToList();
            }
        }

        public event EventHandler? Create;
        public ICommand AddStageCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (!string.IsNullOrWhiteSpace(Stage.Name) && !string.IsNullOrWhiteSpace(Stage.Description) && Stage.Equipment != null )
                    {
                        DatabaseLocator.Context.Stages.Add(Stage);
                        DatabaseLocator.Context.SaveChanges();
                        Create?.Invoke(this, EventArgs.Empty);

                        Close();
                    }
                    else
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show("Заполните название, описание и оборудование ", "",
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
