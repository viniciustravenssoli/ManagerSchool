using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manager.Services.DTO;

namespace Manager.Services.Interface
{
    public interface IClassService
    {
        Task<ClassDTO> Create(ClassDTO classDTO);

        Task<ClassDTO> Get(long id);

        Task<List<ClassDTO>> Get();

        Task<ClassDTO> Update(ClassDTO classDTO);

        Task Remove(long id);

        Task<List<ClassDTO>> GetClassWithTeacher(long teacherId);

        Task<ClassDTO> Createe(ClassDTO classDTO);

        Task<List<ClassDTO>> GetAllClasses(int skip, int take);

        
    }
}