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
    
    public partial class FirmaKoli
    {
        public int Id { get; set; }
        public int KoliID { get; set; }
        public int isemriId { get; set; }
        public int PaletID { get; set; }
        public string Etiket { get; set; }
        public Nullable<byte> MakinaNo { get; set; }
        public Nullable<int> SatışID { get; set; }
    
        public virtual İşEmri İşEmri { get; set; }
        public virtual Satış Satış { get; set; }
    }
}
