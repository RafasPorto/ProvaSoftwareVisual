using Microsoft.AspNetCore.Mvc;
using ProvaBackEnd.Data;
using ProvaBackEnd.Models;

namespace ProvaBackEnd.Controllers;
[ApiController]
[Route("api/funcionario")]
public class FuncionarioController:ControllerBase
{
    private readonly AppDataContext _ctx;
    public FuncionarioController(AppDataContext ctx)
    {
        _ctx = ctx;
    }

    [HttpPost]
    [Route("cadastrar")]
    public ActionResult Cadastrar([FromBody] Funcionario NovoFunc){
        try
        {
            _ctx.Funcionarios.Add(NovoFunc);
            _ctx.SaveChanges();
            return Created("", NovoFunc);
        }
        catch (Exception e)
        {
            
            return BadRequest(e.Message);
        }
    }
    [HttpGet]
    [Route("listar")]
    public ActionResult Listar(){
        try
        {
            var funcionarios = _ctx.Funcionarios.ToList();
            if(funcionarios.Count == 0){
                return NotFound("Nenhum Funcionário Encontrado");
            }
            return funcionarios.Count == 0? NotFound():Ok(funcionarios);

        }catch(Exception e){
            return BadRequest(e);
        }
    }
}
