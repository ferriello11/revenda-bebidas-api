using Revenda.Domain.Entities;

namespace SeuProjeto.Services.Interfaces
{
    public interface ILojaService
    {
        Task<Loja> CriarLojaAsync(Loja loja);
        Task<Loja> ObterLojaPorIdAsync(int id);
    }
}
