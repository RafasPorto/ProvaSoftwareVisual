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
    public IActionResult Cadastrar([FromBody] int valor, int quantidade, int mes, int ano, int FuncionarioId)
    {
        try
        {
            Folha novaFolha= new Folha();
            novaFolha.ValorHora = valor;
            novaFolha.QuantidadeHoras=quantidade;
            novaFolha.Mes=mes;
            novaFolha.Ano = ano;
            novaFolha.Funcionario.FuncionarioId= FuncionarioId;
            novaFolha.SalarioBruto =novaFolha.SalarioB();
            novaFolha.ImpostoRenda =novaFolha.ImpostoRendaCalc();
            novaFolha.Inss =novaFolha.InssCalc();
            novaFolha.Fgts =novaFolha.FgtsCalc();
            novaFolha.SalarioLiquido =novaFolha.SalarioL();
            _ctx.Add(novaFolha);
            _ctx.SaveChanges();
            return Created("", novaFolha);

        }
        catch (Exception e)
        {
            return NotFound(e.Message);
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
