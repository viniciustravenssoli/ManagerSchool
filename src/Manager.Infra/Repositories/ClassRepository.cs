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
    public class ClassRepository : BaseRepository<Class>, IClassRepository
    {
        private readonly ManagerContext _context;

        public ClassRepository(ManagerContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Class>> GetClassesWithRelatedTeacher()
        {
            var Classes = await _context.Classes.Include(t => t.Teacher).AsNoTracking().ToListAsync();

            return Classes;
        }

        public async Task<Class> GetByCode(int code)
        {
            var student = await _context.Classes
                            .Where
                            (
                                x => x.ClassCode == code
                            )
                            .AsNoTracking()
                            .ToListAsync();

            return student.FirstOrDefault();
        }
    }
}