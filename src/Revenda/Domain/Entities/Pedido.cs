
using System.Text.Json.Serialization;
using Revenda.Domain.Enums;

namespace Revenda.Domain.Entities
{
    public class Pedido
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int LojaId { get; set; }
        public int ClienteId { get; set; }
        [JsonIgnore]
        public TipoPedido TipoPedido { get; set; }
        public List<ItemPedido> Itens { get; set; }
        public Loja Loja { get; set; }
        public Cliente Cliente { get; set; }
    }

}
