using AdminPessoal.Model.Entities;
using AdminPessoal.Model.Services;
using AdminPessoal.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AdminPessoal.ViewModel
{
    public class NewUserViewModel : BaseViewModel
    {
        private string _nome;
        private string _email;
        private string _login;
        private string _password;
        private string _rePassword;
        private int _nivel;
        private bool _isBusy;

        public string Name
        {
            get { return _nome; }
            set
            {
                if (SetProperty(ref _nome, value))
                    AddUserCommand.ChangeCanExecute();
            }
        }
        
        public string Email
        {
            get { return _email; }
            set
            {
                if (SetProperty(ref _email, value))
                    AddUserCommand.ChangeCanExecute();
            }
        }

        public string Login
        {
            get { return _login; }
            set
            {
                if (SetProperty(ref _login, value))
                    AddUserCommand.ChangeCanExecute();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (SetProperty(ref _password, value))
                    AddUserCommand.ChangeCanExecute();
            }
        }        

        public string RePassword
        {
            get { return _rePassword; }
            set
            {
                if (SetProperty(ref _rePassword, value))
                    AddUserCommand.ChangeCanExecute();
            }
        }

        public int Nivel
        {
            get { return _nivel; }
            set
            {
                if (SetProperty(ref _nivel, value))
                    AddUserCommand.ChangeCanExecute();
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                SetProperty(ref _isBusy, value);
            }
        }        

        public Command AddUserCommand { get; }

        public NewUserViewModel()
        {
            IsBusy = false;
            AddUserCommand = new Command(ExecuteSaveUserCommand, CanExecuteSaveUserCommand);
        }

        bool CanExecuteSaveUserCommand()
        {
            bool validations = false;

            if ((!string.IsNullOrWhiteSpace(Name)) && 
                (!string.IsNullOrWhiteSpace(Email)) &&
                (!string.IsNullOrWhiteSpace(Login)) &&
                (!string.IsNullOrWhiteSpace(Password)) && 
                (!string.IsNullOrWhiteSpace(RePassword)))
                validations = true;            

            return validations;
        }

        async void ExecuteSaveUserCommand()
        {
            IsBusy = true;

            if (ComparePassword())
            {
                if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
                {
                    AzureClient _client = new AzureClient();

                    User newUser = new User();
                    newUser.Name = Name;
                    newUser.Email = Email;
                    newUser.Password = Password;

                    bool addeduser = await _client.AddUser(newUser);

                    if (addeduser)
                    {
                        Name = "";
                        Email = "";
                        Password = "";
                        RePassword = "";
                        IsBusy = false;                        
                        await App.Current.MainPage.DisplayAlert("Administrador Financeiro", "Usuário salvo com sucesso", "Ok");
                    }
                    else
                    {
                        Password = "";
                        RePassword = "";
                        IsBusy = false;
                        await App.Current.MainPage.DisplayAlert("Administrador Financeiro", "Ocorreu um erro ao salvar o usuário.", "Ok");                        
                    }
                }
                else
                {
                    IsBusy = false;
                    await App.Current.MainPage.DisplayAlert("Administrador Financeiro", "Sem conexão à rede.", "Ok");
                }
            }
            else
            {
                IsBusy = false;
                await App.Current.MainPage.DisplayAlert("Administrador Financeiro", "A Senha e a Confirmação de Senha são diferentes.", "Ok");
            }
        }

        bool ComparePassword()
        {
            bool result = false;

            if (Password == RePassword)
                result = true;

            return result;
        }
    }
}
