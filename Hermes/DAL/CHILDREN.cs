//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hermes.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class CHILDREN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CHILDREN()
        {
            this.AITISIS = new HashSet<AITISIS>();
        }
    
        public int CHILD_ID { get; set; }
        public string AMKA { get; set; }
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public Nullable<System.DateTime> BIRTHDATE { get; set; }
        public Nullable<int> GENDER { get; set; }
        public Nullable<int> NATIONALITY { get; set; }
        public Nullable<int> PARENTS_ID { get; set; }
    
        public virtual PARENTS PARENTS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AITISIS> AITISIS { get; set; }
    }
}