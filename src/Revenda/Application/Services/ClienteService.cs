using System.Threading.Tasks;
using Revenda.Domain.Entities;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<ApiResponse> CriarClienteAsync(Clientes clientes)
    {
        var clienteExistente = await _clienteRepository.ObterClienteAsync(clientes.Email, clientes.Telefone);

        if (clienteExistente != null)
        {
            return new ApiResponse(false, "E-mail ou Telefone já existente.");
        }

        await _clienteRepository.AdicionarAsync(clientes);

        return new ApiResponse(true, "Cliente criado com sucesso.");
    }

    public async Task<Clientes> ObterClientePorIdAsync(int id)
    {
        return await _clienteRepository.ObterPorIdAsync(id);
    }
}
