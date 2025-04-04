using Revenda.Domain.Entities;
using System;
using System.Threading.Tasks;

public interface ILojaService
{
    Task<Loja> CriarLojaAsync(Loja loja);
    Task<Loja> ObterLojaPorIdAsync(int id);
}
