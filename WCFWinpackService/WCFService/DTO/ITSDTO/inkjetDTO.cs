using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ITSDTO
{
    public partial class inkjetDTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public inkjetDTO()
        {
            this.İşEmriDTO = new HashSet<İşEmriDTO>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string COM { get; set; }
        public byte MakinaNo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<İşEmriDTO> İşEmriDTO { get; set; }
    }
}
