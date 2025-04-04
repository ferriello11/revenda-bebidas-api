using System.Text.Json.Serialization;

namespace Revenda.Domain.Entities;

public class Contato
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Numero { get; set; } = null!;
    public bool Principal { get; set; }
    [JsonIgnore]
    public int LojaId { get; set; }
    [JsonIgnore]
    public Loja? Loja { get; set; } = null!;
}