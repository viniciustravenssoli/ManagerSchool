using System.Collections.Generic;
using System.Threading.Tasks;
using Manager.Domain.Entities;

namespace Manager.Infra.Interfaces{
    public interface IStudentRepository: IBaseRepository<Student>{
        Task<Student> GetByEmail(string email);

        Task<List<Student>> SearchByEmail(string email);

        Task<List<Student>> SearchByName(string name);

    }
}