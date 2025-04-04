using System.Text.Json.Serialization;

namespace Revenda.Domain.Entities;

public class Loja
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Cnpj { get; set; } = null!;
    public string RazaoSocial { get; set; } = null!;
    public string NomeFantasia { get; set; } = null!;
    public string Email { get; set; } = null!;
    public ICollection<LojaTelefone> Telefones { get; set; } = new List<LojaTelefone>();
    public ICollection<Contato> Contatos { get; set; } = new List<Contato>();
    public ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();
}