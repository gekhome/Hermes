using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace Hermes.Models
{
    public class UserParentViewModel
    {
        public int USER_ID { get; set; }

        //[Required(ErrorMessage = "Υποχρεωτική συμπλήρωση ονόματος χρήστη")]
        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [Display(Name = "Όνομα χρήστη")]
        public string USERNAME { get; set; }

        //[Required(ErrorMessage = "Υποχρεωτική συμπλήρωση κωδικού ασφαλείας")]
        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        //[DataType(DataType.Password)]
        [Display(Name = "Κωδικός πρόσβασης")]
        public string PASSWORD { get; set; }

        //[Required(ErrorMessage = "Υποχρεωτική συμπλήρωση ΑΦΜ")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.", MinimumLength = 9)]
        [Display(Name = "ΑΦΜ")]
        public string USER_AFM { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση γονέα")]
        [Display(Name = "Γονέας")]
        public int? PARENT_TYPE { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία εγγραφής")]
        public DateTime? CREATEDATE { get; set; }
    }

    public class TaxisnetViewModel
    {
        public int TAXISNET_ID { get; set; }

        public Nullable<int> RANDOM_NUMBER { get; set; }

        public string TAXISNET_AFM { get; set; }
    }


    public class sqlUserParentViewModel
    {
        public int USER_ID { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string USER_AFM { get; set; }

        [Display(Name = "Κηδεμόνας")]
        public string PARENT_TYPETEXT { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία εγγραφής")]
        public Nullable<System.DateTime> CREATEDATE { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string PARENT_FULLNAME { get; set; }
    }


    public class NewUserParentViewModel
    {
        public int USER_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση ονόματος χρήστη")]
        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [RegularExpression(@"^[.A-Za-z0-9_-]+$", ErrorMessage = "Μόνο λατινικοί χαρακτήρες, αριθμοί, τελείες, παύλες χωρίς κενά")]
        [Display(Name = "Όνομα χρήστη")]
        public string USERNAME { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση κωδικού ασφαλείας")]
        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [RegularExpression(@"^[\S]+$", ErrorMessage = "Δεν επιτρέπεται ο κενός χαρακτήρας")]
        [DataType(DataType.Password)]
        [Display(Name = "Κωδικός πρόσβασης")]
        public string PASSWORD { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση ΑΦΜ")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.", MinimumLength = 9)]
        [Display(Name = "ΑΦΜ")]
        public string AFM { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Γονέας")]
        public int? PARENT_TYPE { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία εγγραφής")]
        public DateTime? CREATEDATE { get; set; }

        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [DataType(DataType.Password)]
        [Compare("PASSWORD", ErrorMessage = "Ο κωδικός πρόσβασης και αυτός της επιβεβαίωσης δεν είναι ίδιοι.")]
        [Display(Name = "Επιβεβαίωση κωδικού")]
        public string ConfirmPassword { get; set; }

    }

    public class UserParentEditViewModel
    {
        public int USER_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση ονόματος χρήστη")]
        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [Display(Name = "Όνομα χρήστη")]
        public string USERNAME { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση κωδικού ασφαλείας")]
        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [Display(Name = "Κωδικός πρόσβασης")]
        public string PASSWORD { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση ΑΦΜ")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.", MinimumLength = 9)]
        [Display(Name = "ΑΦΜ")]
        public string AFM { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Γονέας")]
        public int? PARENT_TYPE { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία εγγραφής")]
        public DateTime? CREATEDATE { get; set; }
    }

    public class ParentAccountInfoViewModel
    {
        public int USER_ID { get; set; }

        [Display(Name = "Όνομα χρήστη")]
        public string USERNAME { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string PARENT_FULLNAME { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string PARENT_AFM { get; set; }

        [Display(Name = "ΑΜΚΑ")]
        public string PARENT_AMKA { get; set; }

        [Display(Name = "Τηλ. οικίας")]
        public string PARENT_PHONEHOME { get; set; }

        [Display(Name = "Τηλ. εργασίας")]
        public string PARENT_PHONEWORK { get; set; }

        [Display(Name = "Κινητό")]
        public string PARENT_PHONEMOBILE { get; set; }

        public int PARENTS_ID { get; set; }
    }

    public class gridUserParentViewModel
    {
        public int USER_ID { get; set; }

        [Display(Name = "Όνομα χρήστη")]
        public string USERNAME { get; set; }

        [Display(Name = "Κωδικός πρόσβασης")]
        public string PASSWORD { get; set; }

        [Display(Name = "ΑΦΜ χρήστη")]
        public string USER_AFM { get; set; }

        [Display(Name = "Γονέας")]
        public Nullable<int> PARENT_TYPE { get; set; }

        [Display(Name = "Ημ/νία εγγραφής")]
        public Nullable<System.DateTime> CREATEDATE { get; set; }

        public Nullable<int> STATION_ID { get; set; }
        public Nullable<int> AITISI_ID { get; set; }
    }

}