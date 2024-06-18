using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Hermes.DAL;

namespace Hermes.Models
{
    public class ParentsViewModel
    {
        public int PARENTS_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z]+[ Α-ΩA-Z-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά")]
        [Display(Name = "Όνομα πατέρα")]
        public string FATHER_FIRSTNAME { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z]+[ Α-ΩA-Z-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά")]
        [Display(Name = "Επώνυμο πατέρα")]
        public string FATHER_LASTNAME { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z]+[ Α-ΩA-Z-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά")]
        [Display(Name = "Όνομα μητέρας")]
        public string MOTHER_FIRSTNAME { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z]+[ Α-ΩA-Z-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά")]
        [Display(Name = "Επώνυμο μητέρας")]
        public string MOTHER_LASTNAME { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "ΑΦΜ πατέρα")]
        public string FATHER_AFM { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "ΑΦΜ μητέρας")]
        public string MOTHER_AFM { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[0-9]+[ 0-9]*$", ErrorMessage = "Μόνο αριθμοί και κενά")]
        [Display(Name = "ΑΜΚΑ πατέρα")]
        public string FATHER_AMKA { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[0-9]+[ 0-9]*$", ErrorMessage = "Μόνο αριθμοί και κενά")]
        [Display(Name = "ΑΜΚΑ μητέρας")]
        public string MOTHER_AMKA { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z0-9]+[ Α-ΩA-Z0-9-]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά, αριθμοί")]
        [Display(Name = "ΑΔΤ πατέρα")]
        public string FATHER_ADT { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z0-9]+[ Α-ΩA-Z0-9-]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά, αριθμοί")]
        [Display(Name = "ΑΔΤ μητέρας")]
        public string MOTHER_ADT { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Διαβατήριο πατέρα")]
        public string FATHER_PASSPORT { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Διαβατήριο μητέρας")]
        public string MOTHER_PASSPORT { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[ΆΈΊΉΌΎΏΑ-Ω0-9]+[ ΆΈΊΉΌΎΏΑ-Ω0-9-.,'ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, αριθμοί, κενά, παύλες")]
        [Display(Name = "Διεύθυνση πατέρα")]
        public string FATHER_ADDRESS { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[ΆΈΊΉΌΎΏΑ-Ω0-9]+[ ΆΈΊΉΌΎΏΑ-Ω0-9-.,'ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, αριθμοί, κενά, παύλες")]
        [Display(Name = "Διεύθυνση μητέρας")]
        public string MOTHER_ADDRESS { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[0-9]+[ 0-9-]*$", ErrorMessage = "Μόνο αριθμοί, κενά και παύλες")]
        [Display(Name = "Τηλ. οικίας πατέρα")]
        public string FATHER_PHONEHOME { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[0-9]+[ 0-9-]*$", ErrorMessage = "Μόνο αριθμοί, κενά και παύλες")]
        [Display(Name = "Τηλ. εργασίας πατέρα")]
        public string FATHER_PHONEWORK { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[0-9]+[ 0-9-]*$", ErrorMessage = "Μόνο αριθμοί, κενά και παύλες")]
        [Display(Name = "Τηλ. κινητό πατέρα")]
        public string FATHER_PHONEMOBILE { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Τηλ. οικίας μητέρας")]
        public string MOTHER_PHONEHOME { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[0-9]+[ 0-9-]*$", ErrorMessage = "Μόνο αριθμοί, κενά και παύλες")]
        [Display(Name = "Τηλ.εργασίας μητέρας")]
        public string MOTHER_PHONEWORK { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[0-9]+[ 0-9-]*$", ErrorMessage = "Μόνο αριθμοί, κενά και παύλες")]
        [Display(Name = "Τηλ. κινητό μητέρας")]
        public string MOTHER_PHONEMOBILE { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Δεν είναι έγκυρη μορφή E-mail")]
        [Display(Name = "E-mail πατέρα")]
        public string FATHER_EMAIL { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Δεν είναι έγκυρη μορφή E-mail")]
        [Display(Name = "E-mail μητέρας")]
        public string MOTHER_EMAIL { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Άδεια παραμονής πατέρα")]
        public string FATHER_PERMIT { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Άδεια παραμονής μητέρας")]
        public string MOTHER_PERMIT { get; set; }

        [Display(Name = "Οικογενειακή κατάσταση")]
        public Nullable<int> FAMILY_STATUS { get; set; }

        [Display(Name = "Επιμέλεια παιδιών")]
        public Nullable<int> CHILD_EPIMELEIA { get; set; }

        [Display(Name = "Κωδ. χρήστη")]
        public Nullable<int> PARENT_USERID { get; set; }

        public ParentsViewModel() { }

        public ParentsViewModel(PARENTS data)
        {
            this.PARENTS_ID = data.PARENTS_ID;
            this.FATHER_AFM = data.FATHER_AFM;
            this.FATHER_AMKA = data.FATHER_AMKA;
            this.FATHER_ADT = data.FATHER_ADT;
            this.FATHER_PASSPORT = data.FATHER_PASSPORT;
            this.FATHER_FIRSTNAME = data.FATHER_FIRSTNAME;
            this.FATHER_LASTNAME = data.FATHER_LASTNAME;
            this.FATHER_ADDRESS = data.FATHER_ADDRESS;
            this.FATHER_EMAIL = data.FATHER_EMAIL;
            this.FATHER_PERMIT = data.FATHER_PERMIT;
            this.FATHER_PHONEHOME = data.FATHER_PHONEHOME;
            this.FATHER_PHONEMOBILE = data.FATHER_PHONEMOBILE;
            this.FATHER_PHONEWORK = data.FATHER_PHONEWORK;

            this.MOTHER_AFM = data.MOTHER_AFM;
            this.MOTHER_AMKA = data.MOTHER_AMKA;
            this.MOTHER_ADT = data.MOTHER_ADT;
            this.MOTHER_PASSPORT = data.MOTHER_PASSPORT;
            this.MOTHER_FIRSTNAME = data.MOTHER_FIRSTNAME;
            this.MOTHER_LASTNAME = data.MOTHER_LASTNAME;
            this.MOTHER_ADDRESS = data.MOTHER_ADDRESS;
            this.MOTHER_EMAIL = data.MOTHER_EMAIL;
            this.MOTHER_PERMIT = data.MOTHER_PERMIT;
            this.MOTHER_PHONEHOME = data.MOTHER_PHONEHOME;
            this.MOTHER_PHONEMOBILE = data.MOTHER_PHONEMOBILE;
            this.MOTHER_PHONEWORK = data.MOTHER_PHONEWORK;

            this.CHILD_EPIMELEIA = data.CHILD_EPIMELEIA;
            this.FAMILY_STATUS = data.FAMILY_STATUS;
        }

    }

    public class ChildrenViewModel
    {
        public int CHILD_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[0-9]+[ 0-9]*$", ErrorMessage = "Μόνο αριθμοί και κενά")]
        [Display(Name = "ΑΜΚΑ")]
        public string AMKA { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z]+[ Α-ΩA-Z-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά")]
        [Display(Name = "Όνομα")]
        public string FIRSTNAME { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z]+[ Α-ΩA-Z-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά")]
        [Display(Name = "Επώνυμο")]
        public string LASTNAME { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία γέννησης")]
        public Nullable<System.DateTime> BIRTHDATE { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Φύλο")]
        public Nullable<int> GENDER { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Εθνικότητα")]
        public Nullable<int> NATIONALITY { get; set; }

        [Display(Name = "Γονέας")]
        public Nullable<int> PARENTS_ID { get; set; }
    }

    public class ChildSelectorViewModel
    {
        public int CHILD_ID { get; set; }

        [Display(Name = "Βρεφονήπιο")]
        public string FULLNAME { get; set; }

        [Display(Name = "Γονείς")]
        public Nullable<int> PARENTS_ID { get; set; }
    }

    public class ParentsInfoViewModel
    {
        public int PARENTS_ID { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string FATHER_FULLNAME { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string MOTHER_FULLNAME { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string FATHER_AFM { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string MOTHER_AFM { get; set; }

        [Display(Name = "ΑΜΚΑ")]
        public string FATHER_AMKA { get; set; }

        [Display(Name = "ΑΜΚΑ")]
        public string MOTHER_AMKA { get; set; }

        [Display(Name = "Τηλ. οικίας")]
        public string FATHER_PHONEHOME { get; set; }

        [Display(Name = "Κινητό")]
        public string FATHER_PHONEMOBILE { get; set; }

        [Display(Name = "Τηλ. εργασίας")]
        public string FATHER_PHONEWORK { get; set; }

        [Display(Name = "Τηλ. οικίας")]
        public string MOTHER_PHONEHOME { get; set; }

        [Display(Name = "Κινητό")]
        public string MOTHER_PHONEMOBILE { get; set; }

        [Display(Name = "Τηλ. εργασίας")]
        public string MOTHER_PHONEWORK { get; set; }

        [Display(Name = "Διεύθυνση")]
        public string FATHER_ADDRESS { get; set; }

        [Display(Name = "Διεύθυνση")]
        public string MOTHER_ADDRESS { get; set; }

        [Display(Name = "E-mail")]
        public string FATHER_EMAIL { get; set; }

        [Display(Name = "E-mail")]
        public string MOTHER_EMAIL { get; set; }

        [Display(Name = "Οικ. κατάσταση")]
        public string FSTATUS_TEXT { get; set; }

        [Display(Name = "Επιμέλεια παιδιού")]
        public string PARENT_TYPETEXT { get; set; }
    }

    public class ParentSelectorViewModel
    {
        public int PARENTS_ID { get; set; }

        [Display(Name = "Πατέρας")]
        public string FATHER_FULLNAME { get; set; }

        [Display(Name = "Μητέρα")]
        public string MOTHER_FULLNAME { get; set; }
    }

    public class StatementViewModel
    {
        public int STATEMENT_ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> STATEMENT_DATE { get; set; }

        public Nullable<int> PROSKLISI_ID { get; set; }

        public Nullable<int> PARENTS_ID { get; set; }

        public Nullable<int> STATION_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Επάγγελμα πατέρα")]
        public Nullable<int> FATHER_JOBSECTOR { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "ΑΜΑ πατέρα")]
        public string FATHER_AMA { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Δελτίο ανεργίας πατέρα")]
        public string FATHER_DELTIOANERGIA { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Τακτικό επίδομα (πατέρας)")]
        public int? FATHER_EPIDOMA { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Επάγγελμα μητέρας")]
        public Nullable<int> MOTHER_JOBSECTOR { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "ΑΜΑ μητέρας")]
        public string MOTHER_AMA { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Δελτίο ανεργίας μητέρας")]
        public string MOTHER_DELTIOANERGIA { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Τακτικό επίδομα (μητέρα)")]
        public int? MOTHER_EPIDOMA { get; set; }

        [Display(Name = "Κατηγορία εισοδήματος")]
        public Nullable<int> INCOME_CATEGORY { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Οικογενειακό εισόδημα")]
        public Nullable<decimal> INCOME_FAMILY { get; set; }

        [Display(Name = "Δικαιούχοι παροχών ΟΑΕΔ και οι δύο γονείς")]
        public bool DIKAIOYXOI_BOTH { get; set; }

        [Display(Name = "Πατέρας ΑΜΕΑ με αναπηρία 67% και άνω")]
        public bool FATHER_DISABILITY { get; set; }

        [Display(Name = "Μητέρα ΑΜΕΑ με αναπηρία 67% και άνω")]
        public bool MOTHER_DISABILITY { get; set; }

        [Display(Name = "Οικογένεια με παιδί ΑΜΕΑ")]
        public bool CHILD_AMEA { get; set; }

        [Display(Name = "Παιδί ορφανό ή μονογονεϊκή οικογένεια")]
        public bool CHILD_ORPHAN { get; set; }

        [Display(Name = "Παιδί διαζευγμένων γονέων")]
        public bool PARENT_DIVORCED { get; set; }

        [Display(Name = "Παιδί με γονέα Στρατευμένο")]
        public bool PARENT_INARMY { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ανήλικα παιδιά στην οικογένεια")]
        public Nullable<int> CHILDREN_MINOR { get; set; }

        [Display(Name = "Επανεγγραφή")]
        public bool RE_REGISTRATION { get; set; }

        [Display(Name = "Αδελφός/φή που ήδη φιλοξενείται στο ΒΝΣ")]
        public bool SIBLING_IN_BNS { get; set; }

        public virtual PARENTS PARENTS { get; set; }

    }

    public class sqlChildrenViewModel
    {
        public int CHILD_ID { get; set; }

        [Display(Name = "ΑΜΚΑ")]
        public string AMKA { get; set; }

        [Display(Name = "Όνομα")]
        public string FIRSTNAME { get; set; }

        [Display(Name = "Επώνυμο")]
        public string LASTNAME { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία γέννησης")]
        public Nullable<System.DateTime> BIRTHDATE { get; set; }

        [Display(Name = "Φύλο")]
        public Nullable<int> GENDER { get; set; }

        [Display(Name = "Εθνικότητα")]
        public Nullable<int> NATIONALITY { get; set; }

        [Display(Name = "Κηδεμόνας")]
        public Nullable<int> PARENTS_ID { get; set; }

        [Display(Name = "Κηδεμόνας")]
        public string PARENT_FULLNAME { get; set; }

        [Display(Name = "Σταθμός")]
        public Nullable<int> STATION_ID { get; set; }
        public Nullable<int> AITISI_ID { get; set; }
        public Nullable<int> PROSKLISI_ID { get; set; }

    }

}