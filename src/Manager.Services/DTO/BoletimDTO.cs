using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Services.DTO
{
    public class BoletimDTO
    {
        public int BoletimId{ get;  set;}
        public double NotaFinal { get;  set;}
        public double Nota1 { get;  set;}
        public double Nota2 { get;  set;}
        public DateTime Schedule { get;  set;}
        public DateTime CreatedAt { get;  set; }
    }
}