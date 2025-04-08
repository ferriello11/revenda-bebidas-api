using System.Text.Json.Serialization;
using Revenda.Domain.Entities;

namespace Revenda.Domain.Entities
{
    public class LojaTelefone
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Telefone { get; set; } = null!;
        [JsonIgnore]
        public int LojaId { get; set; }
        [JsonIgnore]
        public Loja? Loja { get; set; } = null!;
    }
}