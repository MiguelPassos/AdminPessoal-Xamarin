using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;

namespace AdminPessoal.Model.Entities
{
    [DataTable("Usuarios")]
    public class User
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Nome")]
        public string Name { get; set; }

        [JsonProperty("Senha")]
        public string Password { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [Version]
        public string Version { get; set; }
    }
}
