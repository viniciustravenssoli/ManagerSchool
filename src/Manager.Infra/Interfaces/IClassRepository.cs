using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manager.Domain.Entities;

namespace Manager.Infra.Interfaces
{
    public interface IClassRepository : IBaseRepository<Class>
    {
        Task<List<Class>> GetClassesWithRelatedTeacher(long teacherId);

        Task<Class> GetByCode(int code);

        Task<Class> Createe(Class classToCreate);

        Task<List<Class>> GetAllClasses(int skip, int take);

    }
}