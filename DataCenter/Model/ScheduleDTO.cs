using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCenter.Model
{
    public class ScheduleDTO
    {
        public int ScheduleID { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public string StartTime { get; set; } 
        public string EndTime { get; set; }
        public string ScheduleType { get; set; }
        public string Color { get; set; }
        public string DoctorName { get; set; }
        public string SpecialityName { get; set; }
        // Другие необходимые свойства
    }

}
