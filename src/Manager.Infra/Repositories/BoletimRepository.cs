using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manager.Domain.Entities;
using Manager.Infra.Context;
using Manager.Infra.Interfaces;
using Manager.Infra.Repositiries;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infra.Repositories
{
    public class BoletimRepository : BaseRepository<Boletim>, IBoletimRepository
    {
        private readonly ManagerContext _context;

        public BoletimRepository(ManagerContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Teacher> GetById(long id)
        {
            var obj = await _context.Teachers
                    .AsNoTracking()
                    .Where(x => x.Id == id)
                    .ToListAsync();

            return obj.FirstOrDefault();
        }
    }
}