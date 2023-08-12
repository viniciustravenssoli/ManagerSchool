using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Manager.Core.Exceptions;
using Manager.Domain.Entities;
using Manager.Infra.Interfaces;
using Manager.Services.DTO;
using Manager.Services.Interface;

namespace Manager.Services.Services
{
    public class ClassService : IClassService
    {
        private readonly IMapper _mapper;
        private readonly IClassRepository _classRepository;
        private readonly ITeacherRepository _teacherRepository;

        public ClassService(IMapper mapper, IClassRepository classRepository, ITeacherRepository teacherRepository)
        {
            _mapper = mapper;
            _classRepository = classRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task<ClassDTO> Create(ClassDTO classDTO)
        {
            var classExists = await _classRepository.GetByCode(classDTO.ClassCode);

            if (classExists != null)
                throw new DomainException("Já existe uma classe cadastrada com esse codigo");

            var clas = _mapper.Map<Class>(classDTO);
            //student.Validate();

            var classCreated = await _classRepository.Create(clas);

            return _mapper.Map<ClassDTO>(classCreated);
        }

        public async Task<ClassDTO> Createe(ClassDTO classDTO)
        {
            var classExists = await _classRepository.GetByCode(classDTO.ClassCode);
            var teacherExists = await _teacherRepository.GetById(classDTO.TeacherId);

            if (classExists != null)
                throw new DomainException("Já existe uma classe cadastrada com esse codigo");

            var clas = _mapper.Map<Class>(classDTO);
            //student.Validate();

            var classCreated = await _classRepository.Create(clas);
            classCreated.Teacher = teacherExists;

            return _mapper.Map<ClassDTO>(classCreated);
        }

        public async Task<ClassDTO> Get(long id)
        {
            var student = await _classRepository.Get(id);

            return _mapper.Map<ClassDTO>(student);
        }

        public async Task<List<ClassDTO>> Get()
        {
            var allStudents = await _classRepository.Get();

            return _mapper.Map<List<ClassDTO>>(allStudents);
        }

        public async Task<List<ClassDTO>> GetAllClasses(int skip, int take)
        {
            var allStudents = await _classRepository.GetAllClasses(skip, take);

            return _mapper.Map<List<ClassDTO>>(allStudents);
        }

        public async Task<List<ClassDTO>> GetClassWithTeacher(long teacherId)
        {
            var allClasses = await _classRepository.GetClassesWithRelatedTeacher(teacherId);

            return _mapper.Map<List<ClassDTO>>(allClasses);
        }

        public async Task Remove(long id)
        {
            await _classRepository.Remove(id);
        }


        public async Task<ClassDTO> Update(ClassDTO classDTO)
        {
            var stundetExists = await _classRepository.Get(classDTO.Id);

            if (stundetExists == null)
                throw new DomainException("Nao existe nenhum usuario com o Id informado");

            var student = _mapper.Map<Class>(classDTO);
            //student.Validate();

            var studentUpdated = await _classRepository.Update(student);

            return _mapper.Map<ClassDTO>(studentUpdated);
        }
    }
}