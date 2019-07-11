﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ITSDTO
{
    public partial class EtiketCihazıDTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EtiketCihazıDTO()
        {
            this.EtiketlerDTO = new HashSet<EtiketlerDTO>();
            this.İşEmriDTO = new HashSet<İşEmriDTO>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string ip { get; set; }
        public int port { get; set; }
        public byte MakinaNo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EtiketlerDTO> EtiketlerDTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<İşEmriDTO> İşEmriDTO { get; set; }

    }
}
