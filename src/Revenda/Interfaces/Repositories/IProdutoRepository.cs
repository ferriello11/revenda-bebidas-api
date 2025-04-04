public interface IProdutoRepository
{
    Task<int> AdicionarAsync(Produto produto);
    Task<Produto?> ObterPorIdAsync(int id);
    Task<IEnumerable<Produto>> ObterTodosAsync();
}
