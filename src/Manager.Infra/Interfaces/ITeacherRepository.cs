using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manager.Domain.Entities;

namespace Manager.Infra.Interfaces
{
    public interface ITeacherRepository : IBaseRepository<Teacher>
    {
        Task<Teacher> GetByEmail(string email);

        Task<List<Teacher>> SearchByEmail(string email);

        Task<List<Teacher>> SearchByName(string name);

        Task<Teacher> GetByCpf(string cpf);
    }
}