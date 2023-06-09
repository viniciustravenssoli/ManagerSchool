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

        public async Task<List<Class>> GetClassesWithRelatedTeacher(long teacherId)
        {
            var Class = await _context.Classes
                                .Include
                                (
                                    x => x.Teacher
                                )
                                .Where
                                (
                                    x => x.TeacherId == teacherId
                                )
                                .AsNoTracking()
                                .ToListAsync();

            return Class;
        }

        public async Task<List<Class>> GetAllClasses(int skip, int take)
        {
            var Class = await _context.Classes
                                .Include
                                (
                                    x => x.Teacher
                                )
                                .Skip(skip)
                                .Take(take)
                                .OrderByDescending(c => c.Price)
                                .AsNoTracking()
                                .ToListAsync();

            return Class;
        }

        public async Task<Class> GetByCode(int code)
        {
            var classes = await _context.Classes
                            .Where
                            (
                                x => x.ClassCode == code
                            )
                            .AsNoTracking()
                            .ToListAsync();

            return classes.FirstOrDefault();
        }

        public async Task<Class> Createe(Class classToCreate)
        {
            var teacherExists = await _context.Teachers.FindAsync(classToCreate.TeacherId);
            classToCreate.Teacher = teacherExists;
            _context.Classes.Add(classToCreate);
            await _context.SaveChangesAsync();

            return classToCreate;
        }
    }
}