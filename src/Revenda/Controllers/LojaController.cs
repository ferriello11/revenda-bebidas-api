using Microsoft.AspNetCore.Mvc;
using Revenda.Domain.Entities;
using SeuProjeto.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class LojaController : ControllerBase
{
    private readonly ILojaService _lojaService;

    public LojaController(ILojaService lojaService)
    {
        _lojaService = lojaService;
    }

    [HttpPost]
    public async Task<IActionResult> CriarLoja([FromBody] Loja loja)
    {
        var result = await _lojaService.CriarLojaAsync(loja);
        return CreatedAtAction(nameof(ObterPorId), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId([FromRoute] int id)
    {
        var loja = await _lojaService.ObterLojaPorIdAsync(id);
        if (loja == null)
            return NotFound(new { mensagem = $"loja com ID {id} não encontrado." });

        return Ok(loja);
    }
}
