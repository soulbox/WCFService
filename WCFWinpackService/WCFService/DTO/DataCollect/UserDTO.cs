using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.DataCollect
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string description { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string eposta { get; set; }
        public string address { get; set; }
        public string tel { get; set; }
        public bool MainAdmin { get; set; }
        public System.DateTime BasTarihi { get; set; }

        public virtual ICollection<LogDTO> LogDTO { get; set; }
        public virtual ICollection<UserPermissionDTO> userPermissionDTO { get; set; }
    }
}
