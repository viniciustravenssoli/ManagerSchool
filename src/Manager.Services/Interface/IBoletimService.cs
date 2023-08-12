using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manager.Domain.Entities;
using Manager.Services.DTO;

namespace Manager.Services.Interface
{
    public interface IBoletimService
    {
        Task<BoletimDTO> Create(BoletimDTO teacherDTO);
        Task<Boletim> Update(Boletim teacherDTO);
        Task Remove(long id);
        Task<Boletim> Get(long id);
        Task<List<Boletim>> Get();
        Task<Boletim> GetById(long id);
    }
}