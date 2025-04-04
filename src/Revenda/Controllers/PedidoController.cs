using Microsoft.AspNetCore.Mvc;
using Revenda.Domain.Entities;

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
    public async Task<IActionResult> CriarPedido([FromBody] Pedido pedido)
    {
        if (pedido.LojaId == 0 || pedido.ClienteId == 0 || pedido.TipoPedido == 0)
        {
            return BadRequest("Dados do pedido estão incompletos.");
        }

        if (pedido.Itens == null || !pedido.Itens.Any())
        {
            return BadRequest("O pedido deve conter pelo menos um item.");
        }

        var result = await _pedidoService.CriarPedidoAsync(pedido);
        return CreatedAtAction(nameof(ObterPorId), new { id = result.Id }, result);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId([FromRoute] int id)
    {
        var pedido = await _pedidoService.ObterPorIdAsync(id);
        if (pedido == null) return NotFound();
        return Ok(pedido);
    }
}
