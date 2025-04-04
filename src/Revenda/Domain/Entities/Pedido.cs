
using System.Text.Json.Serialization;
using Revenda.Domain.Enums;

namespace Revenda.Domain.Entities
{
    public class Pedido
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int LojaId { get; set; }
        [JsonIgnore]
        public Loja Loja { get; set; }
        public int ClienteId { get; set; }
        [JsonIgnore]
        public Clientes Cliente { get; set; }
        public List<ItemPedido> Itens { get; set; }
        public TipoPedido TipoPedido { get; set; }
    }

}
