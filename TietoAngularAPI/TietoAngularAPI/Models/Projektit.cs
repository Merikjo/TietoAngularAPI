//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TietoAngularAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Projektit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Projektit()
        {
            this.Tunnit = new HashSet<Tunnit>();
        }
    
        public int Projekti_id { get; set; }
        public string ProjektiNimi { get; set; }
        public Nullable<int> Esimies { get; set; }
        public Nullable<System.DateTime> Avattu { get; set; }
        public Nullable<System.DateTime> Suljettu { get; set; }
        public string Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tunnit> Tunnit { get; set; }
    }
}
