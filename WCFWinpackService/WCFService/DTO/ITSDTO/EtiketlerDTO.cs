using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ITSDTO
{
    public partial class EtiketlerDTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EtiketlerDTO()
        {
            this.İşEmriDTO = new HashSet<İşEmriDTO>();
        }

        public int Id { get; set; }
        public string Açıklama { get; set; }
        public string EtiketShirnk { get; set; }
        public string EtiketKoli { get; set; }
        public string EtiketPalet { get; set; }
        public int EtiketCihazId { get; set; }

        public virtual EtiketCihazıDTO EtiketCihazıDTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<İşEmriDTO> İşEmriDTO { get; set; }
    }
}
