using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminPessoal.Model.Entities;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using AdminPessoal.Model.Services;

namespace AdminPessoal.ViewModel
{
    public class UserViewModel
    {
        private bool isBusy;
        private AzureClient _client;
        public ObservableCollection<User> Users { get; set; }
        public Command ConnectUserCommand { get; set; }
        public Command CancelConnectUserCommand { get; set; }
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; }            
        }

        public User User { get; set; }

        public UserViewModel()
        {
            ConnectUserCommand = new Command(() => VerifyUserInformation());
            CancelConnectUserCommand = new Command(() => ClearUsersList());
            
            Users = new ObservableCollection<User>();
            _client = new AzureClient();
        }

        private async void VerifyUserInformation()
        {
            var result = await _client.GetAllUsers();
            Users.Clear();

            User currentUser = new User();

            
            //currentUser.Email = 

            foreach (var item in result)
            {
                //if()
            }

            //User = Users.
        }

        private void ClearUsersList()
        {
            Users.Clear();            
        }
    }
}
