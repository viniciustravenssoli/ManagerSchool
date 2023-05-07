using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Manager.Core.Exceptions;
using Manager.Domain.Entities;
using Manager.Infra.Interfaces;
using Manager.Services.DTO;
using EscNet.Hashers.Interfaces.Algorithms;
using Manager.Services.Interface;

namespace Manager.Services.Services{
    public class StudentService : IStudentService
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;

        public StudentService(IMapper mapper, IStudentRepository studentRepository)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
        }

        public async Task<StudentDTO> Create(StudentDTO studentDTO)
        {
            var studentExists = await _studentRepository.GetByEmail(studentDTO.Email);
            if(studentExists != null)
                throw new DomainException("Já existe um usuario cadastrado com esse email");

            var student = _mapper.Map<Student>(studentDTO);
            student.Validate();

            var studentCreated = await _studentRepository.Create(student);

            return _mapper.Map<StudentDTO>(studentCreated);
        }

        public async Task<StudentDTO> Get(long id)
        {
            var student = await _studentRepository.Get(id);

            return _mapper.Map<StudentDTO>(student);
        }

        public async Task<List<StudentDTO>> Get()
        {
           var allStudents = await _studentRepository.Get();

           return _mapper.Map<List<StudentDTO>>(allStudents);
        }

        public async Task<StudentDTO> GetByEmail(string email)
        {
            var student = await _studentRepository.GetByEmail(email);

            return _mapper.Map<StudentDTO>(student);
        }

        public async Task Remove(long id)
        {
            await _studentRepository.Remove(id);
        }

        public async Task<List<StudentDTO>> SearchByEmail(string email)
        {
            var allStudents = await _studentRepository.SearchByEmail(email);

            return _mapper.Map<List<StudentDTO>>(allStudents);
        }

        public async Task<List<StudentDTO>> SearchByName(string name)
        {
           var allStudents = await _studentRepository.SearchByName(name);

            return _mapper.Map<List<StudentDTO>>(allStudents);
        }

        public async Task<StudentDTO> Update(StudentDTO studentDTO)
        {
            var stundetExists = await _studentRepository.Get(studentDTO.Id);

            if(stundetExists == null)
                throw new DomainException("Nao existe nenhum usuario com o Id informado");

            var student = _mapper.Map<Student>(studentDTO);
            student.Validate();

            var studentUpdated = await _studentRepository.Update(student);

            return _mapper.Map<StudentDTO>(studentUpdated);
        }
    }
}