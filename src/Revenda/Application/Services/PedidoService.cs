using Revenda.Domain.Dto;
using Revenda.Domain.Entities;
using Revenda.Domain.Enums;
using SeuProjeto.Services.Interfaces;

public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly ILojaRepository _lojaRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly ISqsService _sqsService;

    public PedidoService(IPedidoRepository pedidoRepository, ILojaRepository lojaRepository, IProdutoRepository produtoRepository, ISqsService sqsService)
    {
        _pedidoRepository = pedidoRepository;
        _lojaRepository = lojaRepository;
        _produtoRepository = produtoRepository;
        _sqsService = sqsService;
    }

    public async Task<Pedido> CriarPedidoAsync(PedidoDto pedidoDto)
    {

        var loja = await _lojaRepository.ObterPorIdAsync(pedidoDto.LojaId);

        if (loja == null)
            throw new KeyNotFoundException($"Loja com ID {pedidoDto.LojaId} não encontrada.");

        var Pedido = new Pedido
        {
            LojaId = pedidoDto.LojaId,
            ClienteId = pedidoDto.ClienteId,
            TipoPedido = TipoPedido.Loja,
            Itens = new List<ItemPedido>()
        };

        foreach (var itemDto in pedidoDto.ItemPedido)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(itemDto.ProdutoId);
            if (produto == null)
                throw new KeyNotFoundException($"Produto com ID {itemDto.ProdutoId} não encontrado.");

            Pedido.Itens.Add(new ItemPedido
            {
                ProdutoId = itemDto.ProdutoId,
                Quantidade = itemDto.Quantidade
            });
        }

        var result = await _pedidoRepository.AdicionarAsync(Pedido);

        await _sqsService.SendMessageAsync(new PedidoAmbevMessage
        {
            PedidoId = Pedido.Id,
            LojaId = Pedido.LojaId,
            Itens = Pedido.Itens.Select(i => new ItemPedidoAmbev
            {
                ProdutoId = i.ProdutoId,
                Quantidade = i.Quantidade
            }).ToList()
        });

        var pedidoResponse = new Pedido
        {
            Itens = Pedido.Itens.Select(i => new ItemPedido
            {
                PedidoId = i.PedidoId,
                NomeProduto = i.Produto.Nome,
                Quantidade = i.Quantidade,
            }).ToList()
        };

        return pedidoResponse;
    }

    public async Task<Pedido> ObterPorIdAsync(int id)
    {
        return await _pedidoRepository.ObterPorIdAsync(id);
    }
}
