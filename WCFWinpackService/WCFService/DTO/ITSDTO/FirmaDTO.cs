using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ITSDTO
{
    public partial class FirmaDTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FirmaDTO()
        {
            this.İşEmriDTO = new HashSet<İşEmriDTO>();
            this.SatışDTO = new HashSet<SatışDTO>();
            this.ÜrünlerDTO = new HashSet<ÜrünlerDTO>();
        }

        public int Id { get; set; }
        public string Firma_Adı { get; set; }
        public string Firma_GLN { get; set; }
        public string ITSID { get; set; }
        public string ITSPW { get; set; }
        public System.DateTime Tarih { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<İşEmriDTO> İşEmriDTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SatışDTO> SatışDTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ÜrünlerDTO> ÜrünlerDTO { get; set; }
    }
}
