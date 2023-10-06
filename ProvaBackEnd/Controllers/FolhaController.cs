using ProvaBackEnd.Data;
using ProvaBackEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProvaBackEnd.Controllers;
[ApiController]
[Route("api/folha")]
public class FolhaController:ControllerBase
{
    private readonly AppDataContext _ctx;

    public FolhaController(AppDataContext ctx)
    {
        _ctx = ctx;
    }

 [HttpPost]
[Route("cadastrar")]
public IActionResult Cadastrar([FromBody] Folha folha)
{
    try
    {
        if (folha == null)
        {
            return BadRequest("Dados de folha inválidos.");
        }

        if (folha.FuncionarioId <= 0)
        {
            return BadRequest("FuncionarioId inválido.");
        }

        folha.SalarioBruto = folha.SalarioB(folha.ValorHora, folha.QuantidadeHoras);
        folha.ImpostoRenda = folha.ImpostoRendaCalc();
        folha.Inss = folha.InssCalc();
        folha.Fgts = folha.FgtsCalc();
        folha.SalarioLiquido = folha.SalarioL();

        _ctx.Folhas.Add(folha);

        _ctx.SaveChanges();

        return Created("",folha);
    }
    catch (Exception e)
    {
        return StatusCode(500, e.Message);
    }
}


    [HttpGet("listar")]
    public IActionResult Listar()
    {
        try
        {

            var folhas = _ctx.Folhas.ToList();

            return folhas.Count == 0? NotFound("Nenhuma FOLHA encontrado.") : Ok(folhas);

        }
        catch(Exception e)
        {
            return NotFound(e.Message);
        }
    }
    [HttpGet("buscar/{cpf}/{mes}/{ano}")]
    public IActionResult Buscar([FromRoute] string cpf, int mes, int ano)
    {
        try
        {
            Folha? folhaCadastrada = _ctx.Folhas.FirstOrDefault(x => x.Funcionario.Cpf == cpf && x.Mes == mes && x.Ano == ano);
            if (folhaCadastrada != null)
            {
                return Ok(folhaCadastrada);
            }
            else
            {
                return NotFound("Folha not found.");
            }

        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
}
