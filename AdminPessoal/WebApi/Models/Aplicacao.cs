using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("Aplicacao")]
    public class Aplicacao
    {
        [Key]
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Nome")]
        public string Nome { get; set; }

        [JsonIgnore]
        [JsonProperty("Senha")]        
        public string Senha { get; set; }

        [JsonProperty("Descricao")]
        public string Descricao { get; set; }

        [JsonIgnore]
        [JsonProperty("Ativo")]
        public bool Ativo { get; set; }
    }
}