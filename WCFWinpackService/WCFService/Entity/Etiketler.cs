//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Etiketler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Etiketler()
        {
            this.İşEmri = new HashSet<İşEmri>();
        }
    
        public int Id { get; set; }
        public string Açıklama { get; set; }
        public string EtiketShirnk { get; set; }
        public string EtiketKoli { get; set; }
        public string EtiketPalet { get; set; }
        public int EtiketCihazId { get; set; }
    
        public virtual EtiketCihazı EtiketCihazı { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<İşEmri> İşEmri { get; set; }
    }
}