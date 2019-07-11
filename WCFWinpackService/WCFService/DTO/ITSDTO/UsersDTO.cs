using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ITSDTO
{
    public partial class UsersDTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UsersDTO()
        {
            this.FirmaÜrünDTO = new HashSet<FirmaÜrünDTO>();
            this.UserMakinaDTO = new HashSet<UserMakinaDTO>();
        }

        public int Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string description { get; set; }
        public System.DateTime Tarih { get; set; }
        public bool admin { get; set; }
        public string adı { get; set; }
        public string soyadı { get; set; }
        public string eposta { get; set; }
        public string tel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FirmaÜrünDTO> FirmaÜrünDTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserMakinaDTO> UserMakinaDTO { get; set; }
    }
}
