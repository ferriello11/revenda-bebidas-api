public interface IProdutoService
{
    Task<Produto> CriarProdutoAsync(Produto produto);
    Task<Produto?> ObterProdutoPorIdAsync(int id);
    Task<IEnumerable<Produto>> ObterTodosAsync();
}
