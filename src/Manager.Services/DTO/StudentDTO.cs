using System;


namespace Manager.Services.DTO
{
    public class StudentDTO
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Rgm { get; set; }
        public string Email { get; set; }
        public string Phone { get; set;}
        public DateTime Birth { get; set; }
        public DateTime CreatedAt { get; private set; }

        public StudentDTO( ) => CreatedAt = DateTime.Now;
        

        public StudentDTO(long id, string name, string cpf, string rgm, string email, string phone, DateTime birth, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Cpf = cpf;
            Rgm = rgm;
            Email = email;
            Phone = phone;
            Birth = birth;
            CreatedAt = createdAt;
        }
        
    }
}