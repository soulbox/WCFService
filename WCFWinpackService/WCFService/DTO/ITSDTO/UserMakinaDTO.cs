using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ITSDTO
{
    public partial class UserMakinaDTO
    {
        public int Id { get; set; }
        public int MakinaID { get; set; }
        public int UserID { get; set; }

        public virtual MakinalarDTO MakinalarDTO { get; set; }
        public virtual UsersDTO UsersDTO { get; set; }
    }
}
