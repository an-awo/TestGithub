using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace PatientData.Models
{
    public class PatientList
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public int phone { get; set; }
        public string Name { get; set; }
        public string DiseaseType { get; set; }

        [Display(Name = "看診日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime VisitDate { get; set; }

        public string Gender { get; set; }
    }

    public class PatientDBContext : DbContext
    {
        public DbSet<PatientList> Patient { get; set; }
    }
}