using Microsoft.AspNetCore.Identity;

namespace ProvaBackEnd.Models;
public class Folha
{
    public int FolhaId { get; set; }
    public double ValorHora { get; set; }
    public int QuantidadeHoras { get; set; }
    public int Mes { get; set; }
    public int Ano { get; set; }
    public double SalarioBruto { get; set; }
    public double ImpostoRenda { get; set; }
    public double Inss { get; set; }
    public double Fgts { get; set; }
    public double SalarioLiquido { get; set; }
    public Funcionario? Funcionario { get; set; }
public Folha()
{
    // Construtor Vazio
}

}
