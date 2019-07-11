using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceHost.ITS_Service
{
   public  class MethodResult
    {
        public Status Status { get; set; }
        public string Result { get; set; }
    }
    public enum Status
    {
        Error,
        Success,
        information
    }
}
