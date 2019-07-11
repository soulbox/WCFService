using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.DataCollect
{
    public class UserPermissionDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool Raporlama { get; set; }
        public bool Asansör { get; set; }
        public bool Alarm { get; set; }
        public bool Ayarlar { get; set; }
        public bool Aydınlatma { get; set; }
        public virtual UserDTO UserDTO { get; set; }
    }
}
