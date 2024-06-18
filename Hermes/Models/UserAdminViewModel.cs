﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Hermes.Models
{
    public class UserAdminViewModel
    {
        public int USER_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση ονόματος χρήστη")]
        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [Display(Name = "Όνομα χρήστη")]
        public string USERNAME { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση κωδικού ασφαλείας")]
        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [DataType(DataType.Password)]
        [Display(Name = "Κωδικός πρόσβασης")]
        public string PASSWORD { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(120, ErrorMessage = "Πρέπει να είναι μέχρι 120 χαρακτήρες.")]
        [Display(Name = "Ονοματεπώνυμο")]
        public string FULLNAME { get; set; }

        [Display(Name = "Ενεργός")]
        public bool ISACTIVE { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία εγγραφής")]
        public DateTime? CREATEDATE { get; set; }

    }
}