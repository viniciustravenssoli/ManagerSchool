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
    public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
    {
        private readonly ManagerContext _context;

        public TeacherRepository(ManagerContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Teacher> GetByEmail(string email)
        {
            var teachers = await _context.Teachers
                            .Where
                            (
                                x => x.Email.ToLower() == email.ToLower()
                            )
                            .AsNoTracking()
                            .ToListAsync();

            return teachers.FirstOrDefault();
        }

        public async Task<List<Teacher>> SearchByEmail(string email)
        {
            var teachers = await _context.Teachers
                                    .Where(
                                        x => x.Email.ToLower().Contains(email.ToLower())
                                    )
                                    .AsNoTracking()
                                    .ToListAsync();

            return teachers;
        }

        public async Task<List<Teacher>> SearchByName(string name)
        {
            var teachers = await _context.Teachers
                                 .Where(
                                     x => x.Name.ToLower().Contains(name.ToLower())
                                 )
                                 .AsNoTracking()
                                 .ToListAsync();

            return teachers;
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