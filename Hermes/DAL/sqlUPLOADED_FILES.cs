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
    
    public partial class sqlUPLOADED_FILES
    {
        public System.Guid FILE_ID { get; set; }
        public Nullable<int> PROSKLISI_ID { get; set; }
        public Nullable<int> AITISI_ID { get; set; }
        public Nullable<int> STATION_ID { get; set; }
        public string UPLOAD_NAME { get; set; }
        public string UPLOAD_SUMMARY { get; set; }
        public string FILENAME { get; set; }
        public string EXTENSION { get; set; }
        public string STATION_USER { get; set; }
        public string SCHOOLYEAR_TEXT { get; set; }
    }
}
