using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manager.Domain.Entities;

namespace Manager.Infra.Interfaces
{
    public interface IClassRepository : IBaseRepository<Class>
    {
        Task<List<Class>> GetClassesWithRelatedTeacher();

        Task<Class> GetByCode(int code);

    }
}