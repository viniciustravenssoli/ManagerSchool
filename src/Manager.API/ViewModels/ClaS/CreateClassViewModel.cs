using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Manager.Domain.Entities;
using Sistema_Escolar.Validations;

namespace Manager.API.ViewModels
{
    public class CreateClassViewModel
    {
        public int ClassCode { get; set; }
        
        [Required(ErrorMessage = "O cpf nao pode ser vazio")]
        [MinLength(11, ErrorMessage = "O cpf deve conter exatamente 11 caracteres")]
        [MaxLength(11, ErrorMessage = "O cpf deve conter exatamente 11 caracteres")]
        public string ProfessorCpf { get; set; }
        public Teacher Teacher { get; set; }
        public string Discipline { get; set; }
        public double Price { get; set;}
        public DateTime Schedule { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}