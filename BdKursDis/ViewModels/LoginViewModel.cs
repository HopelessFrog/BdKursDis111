﻿using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BdKursDis.Model;
using BdKursDis.ViewModels.Interfaces;

namespace BdKursDis.ViewModels
{
    internal class LoginViewModel : ViewModelBase, ICloseWindow
    {
        public string Name { get; set; }
        public string Password { get; set; }


        public ICommand Login
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    
                    var user = DatabaseLocator.Context.Users.FirstOrDefault(u => u.Name == Name && u.Password == Password);
                   
                    if (user != null)
                    {
                        var viewModel = new MainViewModel(user);
                        var window = new MainWindow();
                        window.DataContext = viewModel;
                        window.Show();
                        Close();
                    }
                    else
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show("Неправильный логин или пароль", "",
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
