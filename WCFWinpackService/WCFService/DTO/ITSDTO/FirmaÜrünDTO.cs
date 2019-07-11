using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ITSDTO
{
    public partial class FirmaÜrünDTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FirmaÜrünDTO()
        {
            this.BildirmTipiDTO = new HashSet<BildirmTipiDTO>();
        }

        public decimal Id { get; set; }
        public int isEmriId { get; set; }
        public int PaletID { get; set; }
        public int KoliID { get; set; }
        public int ShrinkID { get; set; }
        public string GTN { get; set; }
        public string SKT { get; set; }
        public long SN { get; set; }
        public string LOT { get; set; }
        public string ShrinkKarekod { get; set; }
        public string KoliKarekod { get; set; }
        public string PaletKarekod { get; set; }
        public Nullable<System.DateTime> tarih { get; set; }
        public int UsersID { get; set; }
        public Nullable<decimal> BildirimID { get; set; }
        public Nullable<int> HataKodu { get; set; }
        public string HataMesajı { get; set; }
        public Nullable<int> SatışID { get; set; }
        public Nullable<byte> MakinaNo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BildirmTipiDTO> BildirmTipiDTO { get; set; }
        public virtual İşEmriDTO İşEmriDTO { get; set; }
        public virtual SatışDTO SatışDTO { get; set; }
        public virtual UsersDTO UsersDTO { get; set; }
    }
}
