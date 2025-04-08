using Microsoft.AspNetCore.Mvc;
using Revenda.Domain.Entities;
using SeuProjeto.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpPost]
    public async Task<IActionResult> CriarCliente([FromBody] Cliente cliente)
    {
        var result = await _clienteService.CriarClienteAsync(cliente);

        if (result.Sucesso)
        {
            return CreatedAtAction(nameof(CriarCliente), new { mensagem = result.Mensagem });
        }

        return BadRequest(new { mensagem = result.Mensagem });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId([FromRoute] int id)
    {
        var cliente = await _clienteService.ObterClientePorIdAsync(id);
        if (cliente == null)
            return NotFound(new { mensagem = $"Cliente com ID {id} não encontrado." });

        return Ok(cliente);
    }

}
