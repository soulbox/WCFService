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
    
    public partial class UserMakina
    {
        public int Id { get; set; }
        public int MakinaID { get; set; }
        public int UserID { get; set; }
    
        public virtual Makinalar Makinalar { get; set; }
        public virtual Users Users { get; set; }
    }
}
