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
    
    public partial class BildirmTipi
    {
        public int Id { get; set; }
        public string Tipi { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
        public decimal ÜrünID { get; set; }
    
        public virtual FirmaÜrün FirmaÜrün { get; set; }
    }
}
