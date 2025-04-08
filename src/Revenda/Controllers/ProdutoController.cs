using Microsoft.AspNetCore.Mvc;
using Revenda.Domain.Entities;
using SeuProjeto.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutoController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpPost]
    public async Task<IActionResult> CriarProduto([FromBody] Produto produto)
    {
        var result = await _produtoService.CriarProdutoAsync(produto);
        return CreatedAtAction(nameof(ObterPorId), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId([FromRoute] int id)
    {
        var produto = await _produtoService.ObterProdutoPorIdAsync(id);
        if (produto == null)
            return NotFound(new { mensagem = $"Produto com ID {id} não encontrado." });

        return Ok(produto);
    }
}
