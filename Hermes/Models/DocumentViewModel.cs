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
    public class UploadsViewModel
    {
        public int UPLOAD_ID { get; set; }

        [Display(Name = "Γονέας")]
        public Nullable<int> PARENT_ID { get; set; }

        [Display(Name = "Πρόσκληση")]
        public Nullable<int> PROSKLISI_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Αίτηση")]
        public Nullable<int> AITISI_ID { get; set; }

        [Display(Name = "Σταθμός")]
        public Nullable<int> STATION_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> UPLOAD_DATE { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Ονοματεπώνυμο")]
        public string UPLOAD_NAME { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Περιγραφή αρχείων")]
        public string UPLOAD_SUMMARY { get; set; }
    }

    public class UploadsFilesViewModel
    {
        [Display(Name = "Κωδικός")]
        public System.Guid ID { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Φάκελος (ΒΝΣ)")]
        public string STATION_USER { get; set; }

        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "Σχολ. έτος")]
        public string SCHOOLYEAR_TEXT { get; set; }

        [StringLength(120, ErrorMessage = "Πρέπει να είναι μέχρι 120 χαρακτήρες.")]
        [Display(Name = "Όνομα αρχείου")]
        public string FILENAME { get; set; }

        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "Επέκταση")]
        public string EXTENSION { get; set; }

        public Nullable<int> UPLOAD_ID { get; set; }

        public virtual UPLOADS UPLOADS { get; set; }
    }

    public class sqlUploadedFilesViewModel
    {
        public System.Guid FILE_ID { get; set; }

        public Nullable<int> PROSKLISI_ID { get; set; }

        public Nullable<int> AITISI_ID { get; set; }

        public Nullable<int> STATION_ID { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string UPLOAD_NAME { get; set; }

        [Display(Name = "Περιγραφή αρχείων")]
        public string UPLOAD_SUMMARY { get; set; }

        [Display(Name = "Ονομα αρχείου")]
        public string FILENAME { get; set; }

        [Display(Name = "Επέκταση")]
        public string EXTENSION { get; set; }

        [Display(Name = "Φάκελος")]
        public string STATION_USER { get; set; }

        [Display(Name = "Υποφάκελος")]
        public string SCHOOLYEAR_TEXT { get; set; }

    }

}