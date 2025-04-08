namespace Revenda.Domain.Entities
{
    public class PedidoAmbevMessage
    {
        public int PedidoId { get; set; }
        public int LojaId { get; set; }
        public List<ItemPedidoAmbev> Itens { get; set; }

    }
}