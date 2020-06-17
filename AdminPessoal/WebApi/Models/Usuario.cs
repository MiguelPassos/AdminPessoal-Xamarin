using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Nome")]
        public string Nome { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Login")]
        [JsonIgnore]
        public string Login { get; set; }

        [JsonProperty("Senha")]
        [JsonIgnore]
        public string Senha { get; set; }
        
        [JsonProperty("Nivel")]
        public int Nivel { get; set; }

        [JsonProperty("Ativo")]
        [JsonIgnore]
        public bool Ativo { get; set; }
    }
}