using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Constraints;

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
    public int FuncionarioId { get; set; }
    public Funcionario? Funcionario { get; set; }

    public Folha()
    {
        
    }

    public double SalarioB(double Hora,double Qhoras)
    {
        double Resultado = (double)(ValorHora * QuantidadeHoras);
        return Resultado;
    }


    public double ImpostoRendaCalc()
    {
        if(SalarioB(ValorHora , QuantidadeHoras) <= 1903.98){
                return SalarioB(ValorHora , QuantidadeHoras);
        } else if(SalarioB(ValorHora , QuantidadeHoras) <= 2826.66){
           return (SalarioB(ValorHora , QuantidadeHoras) * 0.075) - 142.8;
        } else if(SalarioB(ValorHora , QuantidadeHoras) <= 3751.06){
            return(SalarioB(ValorHora , QuantidadeHoras) * 0.15) - 354.8;
        } else if(SalarioB(ValorHora , QuantidadeHoras) <= 4664.68){
            return(SalarioB(ValorHora , QuantidadeHoras) * 0.225) - 636.13;
        } else {
            return (SalarioB(ValorHora , QuantidadeHoras) * 0.275) - 869.36;
        } 
    }

    public double InssCalc(){
        
        if( SalarioB(ValorHora , QuantidadeHoras) <= 1693.72){
            return SalarioB(ValorHora , QuantidadeHoras) * 0.08;
        } else if( SalarioB(ValorHora , QuantidadeHoras) <= 2822.9){
            return SalarioB(ValorHora , QuantidadeHoras) * 0.09;
        } else if(SalarioB(ValorHora , QuantidadeHoras) <= 5645.81){
            return SalarioB(ValorHora , QuantidadeHoras) * 0.11;
        } else {
            return SalarioB(ValorHora , QuantidadeHoras) - 621.03;
        }
    }

    public double FgtsCalc(){
        return SalarioB(ValorHora , QuantidadeHoras) * 0.08;
    }

    public double SalarioL(){
        return SalarioB(ValorHora , QuantidadeHoras) - ImpostoRendaCalc() - InssCalc(); 
    }
}

