﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ITSDTO
{
    public partial class ÜrünlerDTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ÜrünlerDTO()
        {
            this.İşEmriDTO = new HashSet<İşEmriDTO>();
        }

        public int Id { get; set; }
        public string Ürün_Adı { get; set; }
        public string GTIN { get; set; }
        public System.DateTime Tarih { get; set; }
        public int Barcodesize { get; set; }
        public string SKT_X { get; set; }
        public string SKT_Y { get; set; }
        public string LOT_X { get; set; }
        public string LOT_Y { get; set; }
        public string GTIN_X { get; set; }
        public string GTIN_Y { get; set; }
        public string SN_X { get; set; }
        public string SN_Y { get; set; }
        public string Gecikme { get; set; }
        public string SKT_FONT { get; set; }
        public string SN_FONT { get; set; }
        public string LOT_Font { get; set; }
        public string GTIN_FONT { get; set; }
        public int KolidekiShrik { get; set; }
        public int ShrinkdekiÜrün { get; set; }
        public int PalettekiKoli { get; set; }
        public System.DateTime Rafömrü { get; set; }
        public int FİrmaId { get; set; }

        public virtual FirmaDTO FirmaDTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<İşEmriDTO> İşEmriDTO { get; set; }
    }
}
