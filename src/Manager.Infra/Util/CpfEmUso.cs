using System.ComponentModel.DataAnnotations;
using System.Linq;
using Manager.Domain.Entities;
using Manager.Infra.Context;

namespace Manager.API.Util
{
    public class CpfEmUso : ValidationAttribute
    {
        // public CpfEmUso(string cpf) { }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string cpf = (string)value;

            ManagerContext context =
                (ManagerContext)validationContext.GetService(typeof(ManagerContext));

            var teacher = context.Teachers.FirstOrDefault(t => t.Cpf.Equals(cpf));
            var student = context.Students.FirstOrDefault(s => s.Cpf.Equals(cpf));
                
            if (teacher == null || student == null)
            {
                //sucess
                return ValidationResult.Success;
            }
            //error
            return new ValidationResult("O CPF já está em uso!");
        }
    }
}