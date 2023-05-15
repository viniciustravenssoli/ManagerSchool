using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Domain.Entities
{
    public class Class : Base
    {
         public Class () => CreatedAt = DateTime.Now;

        public int ClassCode{ get;  set;}
        public string Discipline { get;  set;}
        public DateTime Schedule { get;  set;}
        public double Price { get;  set;}
        public DateTime CreatedAt { get;  set; }
        public long TeacherId {get;  set;}
        public Teacher Teacher { get;  set; }

        public Class(int classCode, string discipline, DateTime schedule, double price, DateTime createdAt, long teacherId, Teacher teacher)
        {
            ClassCode = classCode;
            Discipline = discipline;
            Schedule = schedule;
            Price = price;
            CreatedAt = createdAt;
            TeacherId = teacherId;
            Teacher = teacher;
        }
    }
}