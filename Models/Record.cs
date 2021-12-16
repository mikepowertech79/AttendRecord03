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


        [DisplayName("Person's email")]
        public string PersonEmail { get; set; }


        //[Required]
        [StringLength(20, MinimumLength = 4)]
        [DisplayName("Person's full name")]
        public string PersonName { get; set; }


        [DisplayName("Absence's Type")]
        public string AbsenceType { get; set; }


        [DataType(DataType.DateTime)]
        [DisplayName("Absence Start Time")]
        public DateTime AbsenceTimeStart { get; set; }


        [DataType(DataType.DateTime)]
        [DisplayName("Absence End Time")]
        public DateTime AbsenceTimeEnd { get; set; }

        [Range(1, 8)]
        [DisplayName("Absence Hours Number (1 to 8)")]
        public int AbsenceHours { get; set; }



        public Record()
        {
            
        }



    }
}
