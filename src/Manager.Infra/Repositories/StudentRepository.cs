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
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {

        private readonly ManagerContext _context;

        public StudentRepository(ManagerContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Student> GetByEmail(string email)
        {
            var student = await _context.Students
                            .Where
                            (
                                x => x.Email.ToLower() == email.ToLower()
                            )
                            .AsNoTracking()
                            .ToListAsync();

            return student.FirstOrDefault();
        }

        public async Task<List<Student>> SearchByEmail(string email)
        {
            var allStudents = await _context.Students
                                    .Where(
                                        x => x.Email.ToLower().Contains(email.ToLower())
                                    )
                                    .AsNoTracking()
                                    .ToListAsync();

            return allStudents;
        }

        public async Task<List<Student>> SearchByName(string name)
        {
            var allStudents = await _context.Students
                                 .Where(
                                     x => x.Name.ToLower().Contains(name.ToLower())
                                 )
                                 .AsNoTracking()
                                 .ToListAsync();

            return allStudents;
        }
    }
}