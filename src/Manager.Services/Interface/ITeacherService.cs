using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manager.Services.DTO;

namespace Manager.Services.Interface
{
    public interface ITeacherService
    {
        Task<TeacherDTO> Create(TeacherDTO teacherDTO);
        Task<TeacherDTO> Update(TeacherDTO teacherDTO);
        Task Remove(long id);
        Task<TeacherDTO> Get(long id);
        Task<List<TeacherDTO>> Get();
        Task<List<TeacherDTO>> SearchByName(string name);
        Task<List<TeacherDTO>> SearchByEmail(string email);
        Task<TeacherDTO> GetByEmail(string email);
        Task<TeacherDTO> GetById(long id);

    }
}
