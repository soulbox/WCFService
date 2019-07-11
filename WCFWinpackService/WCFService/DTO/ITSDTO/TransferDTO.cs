using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ITSDTO
{
    public partial class TransferDTO
    {
        public int Id { get; set; }
        public long TransferID { get; set; }
        public Nullable<int> SatışID { get; set; }

        public virtual SatışDTO SatışDTO { get; set; }
    }
}
