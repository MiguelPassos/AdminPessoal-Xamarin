using AdminPessoal.Model.Entities;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPessoal.Model.Services
{
    public class AzureClient
    {
        private IMobileServiceClient _client;
        private IMobileServiceTable<User> _userTable;
        private const string serviceUri = "http://miguelpassos.azurewebsites.net/";

        public AzureClient()
        {
            _client = new MobileServiceClient(serviceUri);
            _userTable = _client.GetTable<User>();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            //var empty = new User[0];

            try
            {
                return await _userTable.ToEnumerableAsync(); //await _userTable.ToEnumerableAsync().Where(x => x.Nome == Nome && x.Senha == Senha);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> AddUser(User usuario)
        {
            try
            {
                await _userTable.InsertAsync(usuario);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }            
        }

        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await _client.SyncContext.PushAsync();
                //await _table.PullAsync("allUsers", _table.CreateQuery());
            }
            catch (MobileServicePushFailedException pushEx)
            {
                if (pushEx.PushResult != null)
                    syncErrors = pushEx.PushResult.Errors;
            }
        }
    }
}
