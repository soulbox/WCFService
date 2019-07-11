using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.DataCollect
{
    public class LogDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Durum { get; set; }
        public System.DateTime Tarih { get; set; }

        public virtual UserDTO  UserDTO { get; set; }
    }
}
