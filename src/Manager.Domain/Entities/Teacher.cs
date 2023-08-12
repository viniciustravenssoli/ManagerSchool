using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Domain.Entities
{
    public class Teacher : Base
    {
        public string Name { get; private set; }

        public string Phone { get; private set; }

        public string Email { get; private set; }

        public string Cpf { get; private set; }

        public DateTime CreatedAt { get;  private set; }

        //public List<Class> Classes { get; set;}

        protected Teacher() { }

        public Teacher(string name, string phone, string email, string cpf, DateTime createdAt)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Cpf = cpf;
            CreatedAt = createdAt;

            //Validate();
        }


        // public static implicit operator Teacher(Class v)
        // {
        //     throw new NotImplementedException();
        // }

        // public bool Validate()
        //    => base.Validate<StudentValidator, Student>(new StudentValidator(), this);

        // public string ErrorsToString()
        // {
        //     var builder = new StringBuilder();

        //     foreach (var error in _errors)
        //         builder.AppendLine(error);

        //     return builder.ToString();
        // }
    }
}