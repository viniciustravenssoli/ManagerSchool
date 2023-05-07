
using System;
using System.Collections.Generic;
using System.Text;
using Manager.Domain.Validators;

namespace Manager.Domain.Entities
{
    public class Student : Base
    {
        public string Name { get; private set; }
        public string Cpf { get; private set; }
        public string Rgm { get; private set; }
        public string Email { get; private set; }
        public string Phone {get; private set;}
        public DateTime Birth { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected Student() => CreatedAt = DateTime.Now;

        public Student(string name, string email, string cpf, string rgm, string phone, DateTime birth, DateTime createdAt)
        {
            Name = name;
            Email = email;
            Cpf = cpf;
            Rgm = rgm;
            Phone = phone;
            Birth = birth;
            CreatedAt = createdAt;
            _errors = new List<string>();

            Validate();
        }

        public void ChangeName(string name){
            Name = name;
            Validate();
        }

        public void ChangeCPF(string cpf){
            Cpf = cpf;
            Validate();
        }

        public void ChangeEmail(string email){
            Email = email;
            Validate();
        }

        public bool Validate() 
            => base.Validate<StudentValidator, Student>(new StudentValidator(), this);

        public string ErrorsToString()
        {
            var builder = new StringBuilder();

            foreach(var error in _errors)
                builder.AppendLine(error);

            return builder.ToString();
        }
    }
}