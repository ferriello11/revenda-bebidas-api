using System.Text.Json.Serialization;

namespace Revenda.Domain.Dto
{
    public class PedidoDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int LojaId { get; set; }
        public int ClienteId { get; set; }
        public List<ItemPedidoDto> ItemPedido { get; set; }

    }

}