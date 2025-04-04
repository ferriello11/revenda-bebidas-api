using System.Text.Json.Serialization;

public class ItemPedido
{
    [JsonIgnore]  
    public int Id { get; set; }
    public int ProdutoId { get; set; }
    [JsonIgnore]
    public Produto Produto { get; set; }
    [JsonIgnore]
    public int PedidoId { get; set; }
    public int Quantidade { get; set; }
}

