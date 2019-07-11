using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceHost
{
    public class PLCBase
    {
        private static PLC _plc;
        public PLC PLC => _plc = _plc ?? new PLC();
        public PLC Asansör { get; set; }
        public PLC Işıklar { get; set; }
        public PLC IsıNem { get; set; }


    }
    [ServiceContract]
    public interface IPLCBase
    {
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        PLC GetStatus();
    //    [OperationContract]
    //    [WebInvoke(Method = "*",
    //ResponseFormat = WebMessageFormat.Json,
    //RequestFormat = WebMessageFormat.Json,
    //BodyStyle = WebMessageBodyStyle.Wrapped)]
    //    object Property(string name, object value);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        object GetPropValue(string name);

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        object SetPropValue(string name, object value);
    }
}
