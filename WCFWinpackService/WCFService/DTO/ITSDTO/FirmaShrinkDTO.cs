using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ITSDTO
{
    public partial class FirmaShrinkDTO
    {
        public int Id { get; set; }
        public int ShrinkID { get; set; }
        public int isemriId { get; set; }
        public int PaletID { get; set; }
        public int KoliID { get; set; }
        public string Etiket { get; set; }
        public Nullable<byte> MakinaNo { get; set; }

        public virtual İşEmriDTO İşEmriDTO { get; set; }
    }
}
