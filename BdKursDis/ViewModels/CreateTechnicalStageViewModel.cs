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
    public class CreateTechnicalStageViewModel : ViewModelBase , ICloseWindow
    {
        private Product product;
        public CreateTechnicalStageViewModel(Product product)
        {
            this.product = product;
            TechnicalStage = new TechnicalStage() { Product = product };
        }

        public event EventHandler? Create;
        public TechnicalStage TechnicalStage { get; set; }

        public List<Stage> Stages
        {
            get
            {
                return DatabaseLocator.Context.Stages.ToList();
            }
        }

        public ICommand AddTechnicalStageCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (!string.IsNullOrWhiteSpace(TechnicalStage.Time) && !(TechnicalStage.Stage == null))
                    {
                        DatabaseLocator.Context.TechnicalStages.Add(TechnicalStage);
                        DatabaseLocator.Context.SaveChanges();
                        Create?.Invoke(this, EventArgs.Empty);

                        Close();
                    }
                    else
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show("Заполните все поля", "",
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
