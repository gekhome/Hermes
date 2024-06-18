using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Hermes.DAL;

namespace Hermes.Models
{
    public class sqlParentGridViewModel
    {
        public int PARENTS_ID { get; set; }

        [Display(Name = "Πατέρας")]
        public string FATHER_FULLNAME { get; set; }

        [Display(Name = "Μητέρα")]
        public string MOTHER_FULLNAME { get; set; }

        [Display(Name = "Τηλ.οικίας (Π)")]
        public string FATHER_PHONEHOME { get; set; }

        [Display(Name = "Τηλ.εργασίας (Π)")]
        public string FATHER_PHONEWORK { get; set; }

        [Display(Name = "Κινητό (Π)")]
        public string FATHER_PHONEMOBILE { get; set; }

        [Display(Name = "Τηλ.οικίας (Μ)")]
        public string MOTHER_PHONEHOME { get; set; }

        [Display(Name = "Τηλ.εργασίας (Μ)")]
        public string MOTHER_PHONEWORK { get; set; }

        [Display(Name = "Κινητό (Μ)")]
        public string MOTHER_PHONEMOBILE { get; set; }

        [Display(Name = "Σταθμός")]
        public Nullable<int> STATION_ID { get; set; }

        public Nullable<int> PROSKLISI_ID { get; set; }

        public Nullable<int> AITISI_ID { get; set; }

    }

    public class sqlAitisiGridViewModel
    {
        public int AITISI_ID { get; set; }

        [Display(Name = "Αρ. Πρωτοκόλλου")]
        public string AITISI_PROTOCOL { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> AITISI_DATE { get; set; }

        [Display(Name = "Πρόσκληση")]
        public string PROTOCOL { get; set; }

        [Display(Name = "Σταθμός")]
        public Nullable<int> STATION_ID { get; set; }

        public Nullable<int> SCHOOL_YEAR { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public string SY_TEXT { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string FULLNAME { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ. γέννησης")]
        public Nullable<System.DateTime> BIRTHDATE { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Ηλικία")]
        public Nullable<decimal> AGE { get; set; }

        [Display(Name = "Κατηγορία")]
        public Nullable<int> AGE_CATEGORY { get; set; }

        [Display(Name = "Φύλο")]
        public Nullable<int> GENDER { get; set; }
        public Nullable<int> PARENTS_ID { get; set; }

    }

    public class sqlStatementGridViewModel
    {
        public int STATEMENT_ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> STATEMENT_DATE { get; set; }

        [Display(Name = "Πρόσκληση")]
        public Nullable<int> PROSKLISI_ID { get; set; }

        [Display(Name = "Πρόσκληση")]
        public string PROTOCOL { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public string SY_TEXT { get; set; }

        public Nullable<int> PARENTS_ID { get; set; }

        public Nullable<int> STATION_ID { get; set; }

        [Display(Name = "Σταθμός")]
        public string STATION_NAME { get; set; }

        [Display(Name = "Πατέρας")]
        public string FATHER_FULLNAME { get; set; }

        [Display(Name = "Μητέρα")]
        public string MOTHER_FULLNAME { get; set; }

        [Display(Name = "Εργασία (Π)")]
        public Nullable<int> FATHER_JOBSECTOR { get; set; }

        [Display(Name = "Εργασία (Μ)")]
        public Nullable<int> MOTHER_JOBSECTOR { get; set; }
    }

}