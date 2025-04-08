using Microsoft.AspNetCore.Mvc;
using Revenda.Domain.Dto;
using SeuProjeto.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    public PedidoController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpPost]
    public async Task<IActionResult> CriarPedido([FromBody] PedidoDto pedido)
    {
        if (pedido.LojaId == 0 || pedido.ClienteId == 0)
        {
            return BadRequest("Dados do pedido estão incompletos.");
        }

        if (pedido.ItemPedido == null || !pedido.ItemPedido.Any())
        {
            return BadRequest("O pedido deve conter pelo menos um item.");
        }

        var totalUnidades = pedido.ItemPedido.Sum(i => i.Quantidade);

        if (totalUnidades < 1000)
        {
            return BadRequest("O pedido precisa ter no mínimo 1000 unidades.");
        }

        var resultado = await _pedidoService.CriarPedidoAsync(pedido);
        return CreatedAtAction(nameof(ObterPorId), new { id = resultado }, resultado);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId([FromRoute] int id)
    {
        var pedido = await _pedidoService.ObterPorIdAsync(id);
        if (pedido == null)
            return NotFound(new { mensagem = $"Pedido com ID {id} não encontrado." });

        return Ok(pedido);
    }
}