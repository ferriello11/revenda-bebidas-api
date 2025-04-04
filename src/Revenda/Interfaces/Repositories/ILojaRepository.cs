using Revenda.Domain.Entities;
using System.Threading.Tasks;

public interface ILojaRepository
{
    Task<int> AdicionarAsync(Loja loja);
    Task<Loja> ObterPorIdAsync(int id);
    Task<Loja?> ObterPorCnpjOuEmailAsync(string cnpj, string email);
}
