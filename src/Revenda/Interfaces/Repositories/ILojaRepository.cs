using Revenda.Domain.Entities;

public interface ILojaRepository
{
    Task<int> AdicionarAsync(Loja loja);
    Task<Loja> ObterPorIdAsync(int id);
    Task<Loja?> ObterPorCnpjOuEmailAsync(string cnpj, string email);
}
