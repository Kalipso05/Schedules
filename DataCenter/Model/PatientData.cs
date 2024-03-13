using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCenter.Model
{
    public class PatientData
    {
        public MedicalCard MedicalCard { get; set; }
        public Patient Patient { get; set; }
        public DiseaseHistory DiseaseHistory { get; set; }
        public Passport Passport { get; set; }
        public InsuransePolicy InsuransePolicy { get; set; }
        public InsuranseCompany insuranseCompany { get; set; }

    }
}
