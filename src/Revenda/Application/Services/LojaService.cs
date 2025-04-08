using Revenda.Domain.Entities;
using SeuProjeto.Services.Interfaces;

public class LojaService : ILojaService
{
    private readonly ILojaRepository _lojaRepository;

    public LojaService(ILojaRepository lojaRepository)
    {
        _lojaRepository = lojaRepository;
    }

    public async Task<Loja> CriarLojaAsync(Loja loja)
    {
        var lojaExistente = await _lojaRepository
                .ObterPorCnpjOuEmailAsync(loja.Cnpj, loja.Email);

        if (lojaExistente != null)
            throw new InvalidOperationException("Já existe uma loja cadastrada com este CNPJ ou e-mail.");

        await _lojaRepository.AdicionarAsync(loja);
        return loja;
    }

    public async Task<Loja> ObterLojaPorIdAsync(int id)
    {
        return await _lojaRepository.ObterPorIdAsync(id);
    }

}
