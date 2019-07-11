using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ITSDTO
{
    public partial class SatışDTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SatışDTO()
        {
            this.FirmaKoliDTO = new HashSet<FirmaKoliDTO>();
            this.FirmaÜrünDTO = new HashSet<FirmaÜrünDTO>();
            this.TransferDTO = new HashSet<TransferDTO>();
        }

        public int Id { get; set; }
        public System.DateTime Tarih { get; set; }
        public int MerkezDepoID { get; set; }
        public bool Durumu { get; set; }
        public int ÜretimFirmaID { get; set; }
        public string SiparişNo { get; set; }
        public int AlıcıŞubeID { get; set; }

        public virtual EczaDeposuDTO EczaDeposuDTO { get; set; }
        public virtual EczaDeposuDTO EczaDeposu1DTO { get; set; }
        public virtual FirmaDTO FirmaDTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FirmaKoliDTO> FirmaKoliDTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FirmaÜrünDTO> FirmaÜrünDTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransferDTO> TransferDTO { get; set; }
    }
}
