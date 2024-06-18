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
    public class AitisiViewModel
    {
        public int AITISI_ID { get; set; }

        public Nullable<int> PROSKLISI_ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> AITISI_DATE { get; set; }

        [Display(Name = "Αρ. Πρωτοκόλλου")]
        public string AITISI_PROTOCOL { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Βρεφονηπιακός Σταθμός")]
        public Nullable<int> STATION_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ονοματεπώνυμο παιδιού")]
        public Nullable<int> CHILD_ID { get; set; }

        [Display(Name = "Γονείς")]
        public Nullable<int> PARENTS_ID { get; set; }

        [Display(Name = "Χρονική σήμανση")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public Nullable<System.DateTime> TIMESTAMP { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Ηλικία")]
        public Nullable<decimal> AGE { get; set; }

        [Display(Name = "Κατηγορία")]
        public Nullable<int> AGE_CATEGORY { get; set; }

        [Display(Name = "Επανεγγραφή")]
        public bool RE_REGISTRATION { get; set; }

        [Display(Name = "Μόρια κοινωνικών κριτηρίων")]
        public Nullable<int> MORIA_SOCIAL { get; set; }

        [Display(Name = "Μόρια δικαιούχων παροχών ΟΑΕΔ")]
        public Nullable<int> MORIA_OAED { get; set; }

        [Display(Name = "Μόρια οικογενειακού εισοδήματος")]
        public Nullable<int> MORIA_INCOME { get; set; }

        [Display(Name = "Μόρια οικογενειακής κατάστασης")]
        public Nullable<int> MORIA_FAMILY { get; set; }

        [Display(Name = "Μόρια επανεγγραφής στον ΒΝΣ")]
        public Nullable<int> MORIA_REREGISTER { get; set; }

        [Display(Name = "Μόρια από αδελφό/φή στον ΒΝΣ")]
        public Nullable<int> MORIA_SIBLING { get; set; }

        [Display(Name = "Συνολικά μόρια")]
        public Nullable<int> MORIA_TOTAL { get; set; }

        [Display(Name = "Αξιολόγηση")]
        public Nullable<int> RANKING { get; set; }

        [Display(Name = "Υπέβαλε ένσταση")]
        public bool ENSTASI { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Αιτία αποκλεισμού")]
        public string APOKLEISMOS_AITIA { get; set; }

        [Display(Name = "Κείμενο Α'/βάθμιας Επιτροπής")]
        public string EPITROPI1_TEXT { get; set; }

        [Display(Name = "Κείμενο Β'/βάθμιας Επιτροπής")]
        public string EPITROPI2_TEXT { get; set; }

        public virtual PARENTS PARENTS { get; set; }

        public virtual CHILDREN CHILDREN { get; set; }

    }

    public class AitisiCheckViewModel
    {
        public int AITISI_ID { get; set; }

        public Nullable<int> PROSKLISI_ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> AITISI_DATE { get; set; }

        [Display(Name = "Αρ. Πρωτοκόλλου")]
        public string AITISI_PROTOCOL { get; set; }

        [Display(Name = "Βρεφονηπιακός Σταθμός")]
        public Nullable<int> STATION_ID { get; set; }

        [Display(Name = "Ονοματεπώνυμο παιδιού")]
        public Nullable<int> CHILD_ID { get; set; }

        [Display(Name = "Γονείς")]
        public Nullable<int> PARENTS_ID { get; set; }

        [Display(Name = "Χρονική σήμανση")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public Nullable<System.DateTime> TIMESTAMP { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Ηλικία")]
        public Nullable<decimal> AGE { get; set; }

        [Display(Name = "Κατηγορία")]
        public Nullable<int> AGE_CATEGORY { get; set; }

        [Display(Name = "Επανεγγραφή")]
        public bool RE_REGISTRATION { get; set; }

        [Display(Name = "Μόρια κοινωνικών κριτηρίων")]
        public Nullable<int> MORIA_SOCIAL { get; set; }

        [Display(Name = "Μόρια δικαιούχων παροχών ΟΑΕΔ")]
        public Nullable<int> MORIA_OAED { get; set; }

        [Display(Name = "Μόρια οικογενειακού εισοδήματος")]
        public Nullable<int> MORIA_INCOME { get; set; }

        [Display(Name = "Μόρια οικογενειακής κατάστασης")]
        public Nullable<int> MORIA_FAMILY { get; set; }

        [Display(Name = "Μόρια επανεγγραφής στον ΒΝΣ")]
        public Nullable<int> MORIA_REREGISTER { get; set; }

        [Display(Name = "Μόρια από αδελφό/φή στον ΒΝΣ")]
        public Nullable<int> MORIA_SIBLING { get; set; }

        [Display(Name = "Συνολικά μόρια")]
        public Nullable<int> MORIA_TOTAL { get; set; }

        [Display(Name = "Αξιολόγηση")]
        public Nullable<int> RANKING { get; set; }

        [Display(Name = "Υπέβαλε ένσταση")]
        public bool ENSTASI { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ0-9.,]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, αριθμοί και σημεία στίξης")]
        [Display(Name = "Αιτία αποκλεισμού")]
        public string APOKLEISMOS_AITIA { get; set; }

        [Display(Name = "Κείμενο Α'/βάθμιας Επιτροπής")]
        public string EPITROPI1_TEXT { get; set; }

        [Display(Name = "Κείμενο Β'/βάθμιας Επιτροπής")]
        public string EPITROPI2_TEXT { get; set; }

        public virtual PARENTS PARENTS { get; set; }

    }


    public class AitiseisListViewModel
    {
        public int AITISI_ID { get; set; }

        public Nullable<int> PROSKLISI_ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> AITISI_DATE { get; set; }

        [Display(Name = "Αρ. Πρωτοκόλλου")]
        public string AITISI_PROTOCOL { get; set; }

        [Display(Name = "Ονοματεπώνυμο παιδιού")]
        public string CHILD_FULLNAME { get; set; }

        public Nullable<int> STATION_ID { get; set; }

        [Display(Name = "Βρεφονηπιακός σταθμός")]
        public string STATION_NAME { get; set; }

        public Nullable<int> PARENTS_ID { get; set; }

        public Nullable<int> CHILD_ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ. γέννησης")]
        public Nullable<System.DateTime> BIRTHDATE { get; set; }

        [Display(Name = "Φύλο")]
        public string GENDER { get; set; }

        [Display(Name = "Ηλικία")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public Nullable<decimal> AGE { get; set; }

        [Display(Name = "Κατηγορία")]
        public Nullable<int> AGE_CATEGORY { get; set; }

        [Display(Name = "Μόρια")]
        public Nullable<int> MORIA_TOTAL { get; set; }

        [Display(Name = "Αξιολόγηση")]
        public string RANKING_TEXT { get; set; }

    }

    public class AitisiMoriaViewModel
    {
        public int AITISI_ID { get; set; }
        public Nullable<int> STATION_ID { get; set; }
        public Nullable<int> PROSKLISI_ID { get; set; }

        [Display(Name = "Πρόσκληση")]
        public string PROSKLISI_PROTOCOL { get; set; }

        [Display(Name = "Α.Π. αίτησης")]
        public string AITISI_PROTOCOL { get; set; }

        [Display(Name = "ΑΜΚΑ")]
        public string CHILD_AMKA { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string CHILD_NAME { get; set; }

        [Display(Name = "Κοινωνικά κριτήρια")]
        public Nullable<int> MORIA_SOCIAL { get; set; }

        [Display(Name = "Δικαιούχοι παροχών ΟΑΕΔ")]
        public Nullable<int> MORIA_OAED { get; set; }

        [Display(Name = "Οικογενειακό εισόδημα")]
        public Nullable<int> MORIA_INCOME { get; set; }

        [Display(Name = "Ανήλικα παιδιά οικογένειας")]
        public Nullable<int> MORIA_FAMILY { get; set; }

        [Display(Name = "Επανεγγραφή")]
        public Nullable<int> MORIA_REREGISTER { get; set; }

        [Display(Name = "Αδελφός/φή στον ίδιο ΒΝΣ")]
        public Nullable<int> MORIA_SIBLING { get; set; }

        [Display(Name = "Σύνολο μορίων")]
        public Nullable<int> MORIA_TOTAL { get; set; }

        [Display(Name = "Αξιολόγηση")]
        public string RANKING { get; set; }

    }

    public class RankingViewModel
    {
        public int RANKING_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Κατάταξη")]
        public string RANKING_TEXT { get; set; }
    }

    public class ChildInfoViewModel
    {
        public int CHILD_ID { get; set; }
        public int AITISI_ID { get; set; }

        [Display(Name = "ΑΜΚΑ")]
        public string AMKA { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string FULLNAME { get; set; }
    }

}