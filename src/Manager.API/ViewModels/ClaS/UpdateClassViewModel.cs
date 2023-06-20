using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.API.ViewModels.ClaS
{
    public class UpdateClassViewModel
    {
        public long Id { get; set; }

        public int ClassCode { get; set; }
        public string TeacherId { get; set; }
        public string Discipline { get; set; }
        public double Price { get; set; }
        public DateTime Schedule { get; set; }
    }
}