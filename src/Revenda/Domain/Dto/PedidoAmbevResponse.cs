using Revenda.Domain.Entities;

namespace Revenda.Domain.Dto
{
    public class PedidoAmbevResponse
    {
        public int PedidoId { get; set; }
        public List<ItemPedidoAmbev> Itens { get; set; }
        public string Mensagem { get; set; }
        public int StatusCode { get; set; }
        public DateTime ProcessadoEm { get; set; }
    }
}
