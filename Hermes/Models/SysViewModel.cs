using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Hermes.Models;
using Hermes.DAL;
using System.Web.Mvc;

namespace Hermes.Models
{

    public class SYS_GENDERSViewModel
    {
        public int GENDER_ID { get; set; }
        public string GENDER { get; set; }
    }

    public class ParentTypeViewModel
    {
        public int PARENT_TYPEID { get; set; }

        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [Display(Name = "Γονέας")]
        public string PARENT_TYPETEXT { get; set; }
    }

    public class SYS_PERIFERIAKESViewModel
    {
        public int PERIFEREIAKI_ID { get; set; }
        public string PERIFERIAKI { get; set; }
    }

    public class SYS_PERIFERIESViewModel
    {
        public SYS_PERIFERIESViewModel()
        {
            this.SYS_DIMOS = new HashSet<SYS_DIMOS>();
        }

        [Display(Name = "Κωδ. Περιφέρειας")]
        public int PERIFERIA_ID { get; set; }

        [Display(Name = "Περιφερειακή Ενότητα")]
        public string PERIFERIA_NAME { get; set; }

        public virtual ICollection<SYS_DIMOS> SYS_DIMOS { get; set; }
    }

    public class SYS_DIMOSViewModel
    {
        public int DIMOS_ID { get; set; }
        public string DIMOS { get; set; }
        public Nullable<int> DIMOS_PERIFERIA { get; set; }
        public virtual SYS_PERIFERIES SYS_PERIFERIES { get; set; }
    }

    public class FamilyIncomeViewModel
    {
        public int INCOME_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Οικογενειακό εισόδημα")]
        public string INCOME_TEXT { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Μόρια")]
        public Nullable<int> INCOME_MORIA { get; set; }
    }

    public class FamilyStatusViewModel
    {
        public int FSTATUS_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Οικογ. κατάσταση")]
        public string FSTATUS_TEXT { get; set; }
    }

    public class NationalityViewModel
    {
        public int NATIONALITY_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Εθνικότητα")]
        public string NATIONALITY_TEXT { get; set; }
    }

    public class JobSectorViewModel
    {
        public int JOBSECTOR_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Τομέας επαγγέλματος")]
        public string JOBSECTOR_TEXT { get; set; }
    }

    public class StationViewModel
    {
        public int ΣΤΑΘΜΟΣ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Επωνυμία")]
        public string ΕΠΩΝΥΜΙΑ { get; set; }

        [Display(Name = "Συμμετοχή")]
        public bool ΣΥΜΜΕΤΟΧΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Ταχ. Διεύθυνση")]
        public string ΤΑΧ_ΔΙΕΥΘΥΝΣΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Τηλ. Υπεύθυνου")]
        public string ΤΗΛΕΦΩΝΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Τηλ. Γραμματείας")]
        public string ΓΡΑΜΜΑΤΕΙΑ { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Φαξ")]
        public string ΦΑΞ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "E-Mail")]
        public string EMAIL { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Υπεύθυνος")]
        public string ΥΠΕΥΘΥΝΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Φύλο Υπεύθυνου")]
        public Nullable<int> ΥΠΕΥΘΥΝΟΣ_ΦΥΛΟ { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Κινητό Υπεύθυνου")]
        public string ΚΙΝΗΤΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Περιφερειακή Δ/νση")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Περιφερειακή Ενότητα")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑ { get; set; }

        public virtual SYS_PERIFERIAKES SYS_PERIFERIAKES { get; set; }

    }

    public class StationsGridViewModel
    {
        public int ΣΤΑΘΜΟΣ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Επωνυμία")]
        public string ΕΠΩΝΥΜΙΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "E-Mail")]
        public string EMAIL { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Περιφερειακή Δ/νση")]
        public int ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Περιφερειακή Ενότητα")]
        public int ΠΕΡΙΦΕΡΕΙΑ { get; set; }

        [Display(Name = "Συμμετοχή")]
        public bool ΣΥΜΜΕΤΟΧΗ { get; set; }

    }

    #region TOOLS
    //----------------------------------------------------
    // new addition 30-07-2016 for MasterChild grids
    public class PeriferiaViewModel
    {
        public int PERIFERIA_ID { get; set; }

        [Display(Name = "Περιφερειακή Ενότητα")]
        public string PERIFERIA_NAME { get; set; }
    }

    public class DimosViewModel
    {
        public int DIMOS_ID { get; set; }

        [Display(Name = "Δήμος")]
        public string DIMOS { get; set; }
        public Nullable<int> DIMOS_PERIFERIA { get; set; }
    }
    //----------------------------------------------------

    public class SchoolYearsViewModel
    {
        public int SY_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(9, ErrorMessage = "Πρέπει να είναι μέχρι 9 χαρακτήρες (π.χ.2015-2016).")]
        [Display(Name = "Σχολικό Έτος")]

        public string SY_TEXT { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]        
        [Display(Name = "Ημερομηνία Έναρξης")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> SY_DATESTART { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ημερομηνία Λήξης")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> SY_DATEEND { get; set; }

    }

    #endregion
    
    public class StatisticsParameters
    {
        public int? PROSKLISI_ID { get; set; }
    }

    public class StationProsklisiParameters
    {
        public int? PROSKLISI_ID { get; set; }
        public int? STATION_ID { get; set; }
    }

    public class DimoiParameters
    {
        public int PERIFERIA_ID { get; set; }
    }

    public class AitisiParameters
    {
        public int AITISI_ID { get; set; }
    }

    public class PARAMETROI_PINAKES
    {
        public int? PROSKLISI_ID { get; set; }
        public int? PERIFERIAKI_ID { get; set; }
        public int? STATION_ID { get; set; }
    }

}