using System.Text.Json.Serialization;

namespace Revenda.Domain.Entities
{
    public class Clientes
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string? Nome { get; set; }

        public string? Email { get; set; }

        public string? Telefone { get; set; }

    }
}
