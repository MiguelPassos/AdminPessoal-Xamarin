using AdminPessoal.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AdminPessoal
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage main = new MainPage();
            main.Title = "Login";
            main.Icon = "loginIcon.png";

            NewUserPage newUserPage = new NewUserPage();
            newUserPage.Title = "Novo";
            newUserPage.Icon = "newUserIcon.png";

            TabbedPage tabPage = new TabbedPage();
            tabPage.Children.Add(main);
            tabPage.Children.Add(newUserPage);
            tabPage.Title = "Administrador Financeiro";
            tabPage.BarTextColor = Color.FromHex("#FFFFFF");
            tabPage.BarBackgroundColor = Color.FromHex("#010050");
            
            MainPage = tabPage;

            //Process.KillProcess(Process.MyPid()); Fechar APP no android.
        }

        protected override void OnStart()
        {
            // Handle when your app starts            
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }        
    }
}
