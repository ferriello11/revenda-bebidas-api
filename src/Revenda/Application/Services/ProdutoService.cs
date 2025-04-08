using Revenda.Domain.Entities;
using SeuProjeto.Services.Interfaces;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<Produto> CriarProdutoAsync(Produto produto)
    {
        await _produtoRepository.AdicionarAsync(produto);
        return produto;
    }

    public async Task<Produto?> ObterProdutoPorIdAsync(int id)
    {
        return await _produtoRepository.ObterPorIdAsync(id);
    }

    public async Task<IEnumerable<Produto>> ObterTodosAsync()
    {
        return await _produtoRepository.ObterTodosAsync();
    }
}
