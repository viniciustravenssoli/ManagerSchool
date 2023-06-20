using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Manager.Domain.Entities;
using Manager.Services.DTO;

namespace Manager.API.ViewModels
{
    public class CreateClassViewModel
    {
        public int ClassCode { get; set; }
        public long TeacherId { get; set; }
        public string Discipline { get; set; }
        public double Price { get; set;}
        public DateTime Schedule { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}