using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ITSDTO
{
    public partial class MakinalarDTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MakinalarDTO()
        {
            this.UserMakinaDTO = new HashSet<UserMakinaDTO>();
        }

        public int Id { get; set; }
        public byte MakinaNo { get; set; }
        public string HWID { get; set; }
        public string Açıklama { get; set; }
        public string MakinaIP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserMakinaDTO> UserMakinaDTO { get; set; }
    }
}
