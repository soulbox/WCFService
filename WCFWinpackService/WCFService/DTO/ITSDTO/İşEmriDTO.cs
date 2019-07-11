using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ITSDTO
{
    public partial class İşEmriDTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public İşEmriDTO()
        {
            this.FirmaKoliDTO = new HashSet<FirmaKoliDTO>();
            this.FirmaPaletDTO = new HashSet<FirmaPaletDTO>();
            this.FirmaShrinkDTO = new HashSet<FirmaShrinkDTO>();
            this.FirmaÜrünDTO = new HashSet<FirmaÜrünDTO>();
        }

        public int Id { get; set; }
        public string GTN { get; set; }
        public string SN { get; set; }
        public string LOT { get; set; }
        public System.DateTime SKT { get; set; }
        public bool Durum { get; set; }
        public int firmaid { get; set; }
        public int kameraid { get; set; }
        public int inkjetid { get; set; }
        public int EtiketCihazıId { get; set; }
        public Nullable<int> EtiketId { get; set; }
        public int PLCId { get; set; }
        public int FirmaUrunlerId { get; set; }
        public System.DateTime Tarih { get; set; }
        public bool isSended { get; set; }
        public string MaxSn { get; set; }
        public bool ShrinkEtiketi { get; set; }
        public bool KoliEtiketi { get; set; }
        public bool PaletEtiketi { get; set; }
        public byte MakinaNo { get; set; }
        public int ÜretimMiktarı { get; set; }
        public int ShrinkMiktarı { get; set; }
        public int KoliMiktarı { get; set; }
        public int PaletMiktarı { get; set; }
        public int MaxShrinkID { get; set; }
        public int MaxKoliID { get; set; }
        public int MaxPaletID { get; set; }
        public int MinShrinkID { get; set; }
        public int MinKoliID { get; set; }
        public int MinPaletID { get; set; }
        public bool ihracat { get; set; }
        public Nullable<System.DateTime> MFG { get; set; }

        public virtual EtiketCihazıDTO EtiketCihazıDTO { get; set; }
        public virtual EtiketlerDTO EtiketlerDTO { get; set; }
        public virtual FirmaDTO FirmaDTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FirmaKoliDTO> FirmaKoliDTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FirmaPaletDTO> FirmaPaletDTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FirmaShrinkDTO> FirmaShrinkDTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FirmaÜrünDTO> FirmaÜrünDTO { get; set; }
        public virtual inkjetDTO inkjetDTO { get; set; }
        public virtual KameraDTO KameraDTO { get; set; }
        public virtual PLCDTO PLCDTO { get; set; }
        public virtual ÜrünlerDTO ÜrünlerDTO { get; set; }
    }
}
