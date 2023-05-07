using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manager.Services.DTO;

namespace Manager.Services.Interface
{
    public interface IStudentService
    {
        Task<StudentDTO> Create(StudentDTO studentDTO);
        Task<StudentDTO> Update(StudentDTO studentDTO);
        Task Remove(long id);
        Task<StudentDTO> Get(long id);
        Task<List<StudentDTO>> Get();
        Task<List<StudentDTO>> SearchByName(string name);
        Task<List<StudentDTO>> SearchByEmail(string email);
        Task<StudentDTO> GetByEmail(string email);

    }
}
