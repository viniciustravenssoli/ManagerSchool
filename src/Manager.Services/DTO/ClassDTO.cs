using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manager.Domain.Entities;

namespace Manager.Services.DTO
{
    public class ClassDTO
    {
        public long Id { get; set;}
        public int ClassCode { get; set;}
        public string ProfessorCpf {get; set;}
        public Teacher Teacher { get; set; }
        public string Discipline { get; set;}
        public DateTime Schedule { get; set;}
        public double Price { get; set;}
        public DateTime CreatedAt { get; set; }
    }
}