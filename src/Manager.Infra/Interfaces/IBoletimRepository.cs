using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manager.Domain.Entities;

namespace Manager.Infra.Interfaces
{
    public interface IBoletimRepository : IBaseRepository<Boletim>
    {
        Task<Teacher> GetById(long id);
    }
}