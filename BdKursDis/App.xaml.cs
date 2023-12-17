using BdKursDis.Model;
using System.Configuration;
using System.Data;
using System.Windows;
using BdKursDis.View;

namespace BdKursDis
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DatabaseLocator.Context = new DisDbContext();
            DatabaseLocator.Context.Users.RemoveRange(DatabaseLocator.Context.Users);

            DatabaseLocator.Context.Users.Add(new User()
                { Name = "1", Password = "1", IsCook = true, IsOperator = true });

            DatabaseLocator.Context.Users.Add(new User()
                { Name = "2", Password = "2", IsCook = true, IsOperator = false });

            DatabaseLocator.Context.Users.Add(new User()
                { Name = "3", Password = "3", IsCook = false, IsOperator = true });

            DatabaseLocator.Context.SaveChanges();
           
            var window = new LoginView();
            window.Show();

           



        }
    }

}
