using AdminPessoal.Model.Entities;
using AdminPessoal.Model.Services;
using AdminPessoal.ViewModel.Base;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AdminPessoal.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private string _email;
        private string _password;
        private bool _isBusy;

        public string Email
        {
            get { return _email; }
            set
            {
                if(SetProperty(ref _email, value))
                    LogUserCommand.ChangeCanExecute();
            }
        }        
        public string Password
        {
            get { return _password; }
            set
            {
                if (SetProperty(ref _password, value))
                    LogUserCommand.ChangeCanExecute();
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
        
        private User Usuario { get; set; }

        public Command LogUserCommand { get; }

        public MainViewModel()
        {
            IsBusy = false;
            LogUserCommand = new Command(ExecuteLogUserCommand, CanExecuteLogUserCommand);            
        }

        bool CanExecuteLogUserCommand()
        {
            bool validations = false;

            if ((!string.IsNullOrWhiteSpace(Email)) && (!string.IsNullOrWhiteSpace(Password)))
                validations = true;

            return validations;
        }

        async void ExecuteLogUserCommand()
        {
            IsBusy = true;

            if (true)
            {
                AzureClient _client = new AzureClient();
                var usuarios = await _client.GetAllUsers();

                if (usuarios != null)
                {
                    foreach (var item in usuarios)
                    {
                        if (item.Email.Equals(Email) && item.Password.Equals(Password))
                        {
                            Usuario = item;
                            break;
                        }                        
                    }
                }

                IsBusy = false;

                if (Usuario != null)
                    await App.Current.MainPage.DisplayAlert("Administrador Financeiro", "Usuário conectado.", "OK");
                else
                {
                    await App.Current.MainPage.DisplayAlert("Administrador Financeiro", "Usuário e/ou senha incorreto(s).", "OK");
                    Password = "";
                    Usuario = null;
                }
            }
            else
            {
                IsBusy = false;
                await App.Current.MainPage.DisplayAlert("Administrador Financeiro", "Sem conexão à rede.", "Ok");                
            }
        }        
    }
}
