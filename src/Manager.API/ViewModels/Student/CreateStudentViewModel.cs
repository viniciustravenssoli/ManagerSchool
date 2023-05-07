using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Sistema_Escolar.Validations;

namespace Manager.API.ViewModels
{
    public class CreateStudentViewModel
    {
        [Required(ErrorMessage = "O nome nao pode ser vazio")]
        [MinLength(3, ErrorMessage = "O nome deve conter no minimo 3 caracteres")]
        [MaxLength(80, ErrorMessage = "O nome deve conter no maximo 80 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O cpf nao pode ser vazio")]
        [MinLength(11, ErrorMessage = "O cpf deve conter exatamente 11 caracteres")]
        [MaxLength(11, ErrorMessage = "O cpf deve conter exatamente 11 caracteres")]
        [CpfEmUso]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O Rgm nao pode ser vazio")]
        [MinLength(12, ErrorMessage = "O Rgm deve conter no minimo 12 caracteres")]
        [MaxLength(12, ErrorMessage = "O Rgm deve conter no maximo 12 caracteres")]
        public string Rgm { get; set; }
        
        [EmailAddress]
        [Required(ErrorMessage = "O Email nao pode ser vazio")]
        [MinLength(8, ErrorMessage = "O Email deve ter no minomo 8 caracteres")]
        [MaxLength(180, ErrorMessage = "O Email nao pode ter mais de 180 caracteres")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "O email é invalido")]
        public string Email { get; set; }
        public string Phone {get; set;}
        public DateTime Birth { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}