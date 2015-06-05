using System;
using System.Collections.Generic;

namespace Pauta.Component.Framework
{
    public class Class
    {
        public string Id { get; set; }

        public string Discipline { get; set; }
        
        public string ClassRoom { get; set; }
        
        public DateTime InitialDate { get; set; }
        
        public DateTime EndDate { get; set; }

        public ICollection<Attendance> Attendances { get; set; }
    }
}