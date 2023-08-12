using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Domain.Entities
{
    public class Boletim : Base
    {
        public Boletim(int boletimId, double notaFinal, double nota1, double nota2, DateTime schedule, DateTime createdAt)
        {
            BoletimId = boletimId;
            NotaFinal = notaFinal;
            Nota1 = nota1;
            Nota2 = nota2;
            Schedule = schedule;
            CreatedAt = createdAt;
        }

        public int BoletimId{ get;  set;}
        public double NotaFinal { get;  set;}
        public double Nota1 { get;  set;}
        public double Nota2 { get;  set;}
        public DateTime Schedule { get;  set;}
        public DateTime CreatedAt { get;  set; }


        public void CalcularNotaFinal(double nota1, double nota2){
            NotaFinal = (nota1 + nota2) / 2;
            
        }
    }
    
}