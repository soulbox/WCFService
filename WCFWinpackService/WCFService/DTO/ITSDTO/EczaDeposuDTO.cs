using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ITSDTO
{
    public partial class EczaDeposuDTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EczaDeposuDTO()
        {
            this.SatışDTO = new HashSet<SatışDTO>();
            this.Satış1DTO = new HashSet<SatışDTO>();
        }


        public int Id { get; set; }
        public string FirmaAdı { get; set; }
        public string GLN { get; set; }
        public string İl { get; set; }
        public string İlçe { get; set; }
        public Nullable<bool> Durum { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
        public string Adress { get; set; }
        public string MerkezSube { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SatışDTO> SatışDTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SatışDTO> Satış1DTO { get; set; }
    }
}
