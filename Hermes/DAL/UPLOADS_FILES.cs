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
    
    public partial class UPLOADS_FILES
    {
        public System.Guid ID { get; set; }
        public string STATION_USER { get; set; }
        public string SCHOOLYEAR_TEXT { get; set; }
        public string FILENAME { get; set; }
        public string EXTENSION { get; set; }
        public Nullable<int> UPLOAD_ID { get; set; }
    
        public virtual UPLOADS UPLOADS { get; set; }
    }
}
