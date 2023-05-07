using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Services.DTO
{
    public class TeacherDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Cpf { get; set; }

        public DateTime CreatedAt { get; set; }

        protected TeacherDTO() => CreatedAt = DateTime.Now;

        public TeacherDTO(string name, string phone, string email, string cpf, DateTime createdAt)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Cpf = cpf;
            CreatedAt = createdAt;
        }
    }
}