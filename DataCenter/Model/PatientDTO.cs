using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCenter.Model
{
    [Table("PatientDTO")]
    public class PatientDTO
    {
        public int ID { get; set; }
        public string Photo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string PassportNumber { get; set; }
        public string PassportSerai { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? IDGender { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string MedicalCardNumber { get; set; }
        public string InsuransePolicyNumber { get; set; }
        public string Diagnos { get; set; }
        public int? IDDiseaseHistory { get; set; }
        public int IDInsuranseCompany { get; set; }
        public string WorkPlace { get; set; }
        public string PathContract { get; set; }
        public string PathPersonalData { get; set; }
        // Дополнительные свойства, при необходимости
        public string Gender { get; set; }
    }


}
