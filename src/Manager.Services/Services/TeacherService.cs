using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Manager.Core.Exceptions;
using Manager.Domain.Entities;
using Manager.Infra.Interfaces;
using Manager.Services.DTO;
using Manager.Services.Interface;

namespace Manager.Services.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IMapper _mapper;
        private readonly ITeacherRepository _teacherRepository;

        public TeacherService(IMapper mapper, ITeacherRepository teacherRepository)
        {
            _mapper = mapper;
            _teacherRepository = teacherRepository;
        }

        public async Task<TeacherDTO> Create(TeacherDTO teacherDTO)
        {
            var teacherExists = await _teacherRepository.GetByEmail(teacherDTO.Email);
            if(teacherExists != null)
                throw new DomainException("Já existe um usuario cadastrado com esse email");

            var teacher = _mapper.Map<Teacher>(teacherDTO);
            //teacher.Validate();

            var teacherCreated = await _teacherRepository.Create(teacher);

            return _mapper.Map<TeacherDTO>(teacherCreated);
        }

        public async Task<TeacherDTO> GetById(long id)
        {
            var teacher = await _teacherRepository.GetById(id);

            return _mapper.Map<TeacherDTO>(teacher);
        }

        public async Task<TeacherDTO> Get(long id)
        {
            var teacher = await _teacherRepository.Get(id);

            return _mapper.Map<TeacherDTO>(teacher);
        }

        public async Task<List<TeacherDTO>> Get()
        {
           var allTeachers = await _teacherRepository.Get();

           return _mapper.Map<List<TeacherDTO>>(allTeachers);
        }

        public async Task<TeacherDTO> GetByEmail(string email)
        {
            var teacher = await _teacherRepository.GetByEmail(email);

            return _mapper.Map<TeacherDTO>(teacher);
        }

        public async Task Remove(long id)
        {
            await _teacherRepository.Remove(id);
        }

        public async Task<List<TeacherDTO>> SearchByEmail(string email)
        {
            var allTeachers = await _teacherRepository.SearchByEmail(email);

            return _mapper.Map<List<TeacherDTO>>(allTeachers);
        }

        public async Task<List<TeacherDTO>> SearchByName(string name)
        {
           var allTeachers = await _teacherRepository.SearchByName(name);

            return _mapper.Map<List<TeacherDTO>>(allTeachers);
        }

        public async Task<TeacherDTO> Update(TeacherDTO teacherDTO)
        {
            var teacherExists = await _teacherRepository.Get(teacherDTO.Id);

            if(teacherExists == null)
                throw new DomainException("Nao existe nenhum usuario com o Id informado");

            var teacher = _mapper.Map<Teacher>(teacherDTO);
            //teacher.Validate();

            var teacherUpdated = await _teacherRepository.Update(teacher);

            return _mapper.Map<TeacherDTO>(teacherUpdated);
        }
    }
}