using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ITSDTO
{
    public partial class BildirmTipiDTO
    {
        public int Id { get; set; }
        public string Tipi { get; set; }
        public DateTime Tarih { get; set; }
        public decimal ÜrünID { get; set; }
        public virtual FirmaÜrünDTO FirmaÜrünDTO { get; set; }
    }
}
