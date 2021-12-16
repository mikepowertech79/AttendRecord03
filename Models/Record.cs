using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AttendRecord03.Models
{
    public class Record
    {
        public int Id { get; set; }

        public string PersonEmail { get; set; }

        public string PersonName { get; set; }

        public string AbsenceType { get; set; }

        public DateTime AbsenceTimeStart { get; set; }

        public DateTime AbsenceTimeEnd { get; set; }

        public int AbsenceHours { get; set; }



        public Record()
        {
            
        }



    }
}
