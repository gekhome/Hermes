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
    
    public partial class mor_AITISI_DATA
    {
        public int AITISI_ID { get; set; }
        public Nullable<int> PROSKLISI_ID { get; set; }
        public Nullable<int> STATION_ID { get; set; }
        public int CHILD_ID { get; set; }
        public string FULLNAME { get; set; }
        public Nullable<int> FATHER_JOBSECTOR { get; set; }
        public Nullable<int> MOTHER_JOBSECTOR { get; set; }
        public Nullable<bool> FATHER_EPIDOMA { get; set; }
        public Nullable<bool> MOTHER_EPIDOMA { get; set; }
        public Nullable<int> FAMILY_INCOME { get; set; }
        public Nullable<bool> DIKAIOYXOI_BOTH { get; set; }
        public Nullable<bool> FATHER_DISABILITY { get; set; }
        public Nullable<bool> MOTHER_DISABILITY { get; set; }
        public Nullable<bool> CHILD_AMEA { get; set; }
        public Nullable<bool> CHILD_ORPHAN { get; set; }
        public Nullable<bool> PARENT_DIVORCED { get; set; }
        public Nullable<bool> PARENT_INARMY { get; set; }
        public Nullable<int> CHILDREN_NUMBER { get; set; }
        public Nullable<bool> RE_REGISTRATION { get; set; }
        public Nullable<bool> SIBLING_IN_BNS { get; set; }
        public Nullable<int> PARENTS_ID { get; set; }
        public int WORKING_MOTHER { get; set; }
    }
}
