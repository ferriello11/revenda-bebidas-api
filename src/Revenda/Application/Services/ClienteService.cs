using Revenda.Domain.Entities;
using SeuProjeto.Services.Interfaces;
public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<ApiResponse> CriarClienteAsync(Cliente clientes)
    {

        if (string.IsNullOrWhiteSpace(clientes.Nome) ||
        string.IsNullOrWhiteSpace(clientes.Email) ||
        string.IsNullOrWhiteSpace(clientes.Telefone))
        {
            return new ApiResponse(false, "Nome, E-mail e Telefone são obrigatórios.");
        }

        var clienteExistente = await _clienteRepository.ObterClienteAsync(clientes.Email, clientes.Telefone);

        if (clienteExistente != null)
        {
            return new ApiResponse(false, "E-mail ou Telefone já existente.");
        }

        await _clienteRepository.AdicionarAsync(clientes);

        return new ApiResponse(true, "Cliente criado com sucesso.");
    }

    public async Task<Cliente> ObterClientePorIdAsync(int id)
    {
        return await _clienteRepository.ObterPorIdAsync(id);
    }
}
