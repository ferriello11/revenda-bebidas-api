using System.Text.Json.Serialization;

namespace Revenda.Domain.Entities;

public class Endereco
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Rua { get; set; } = null!;
    public string Numero { get; set; } = null!;
    public string Bairro { get; set; } = null!;
    public string Cidade { get; set; } = null!;
    public string Estado { get; set; } = null!;
    public string Cep { get; set; } = null!;
    [JsonIgnore]
    public int LojaId { get; set; }
    [JsonIgnore]
    public Loja? Loja { get; set; } = null!;
}