using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using WcfClient.Wcf;
//using WcfClient.;

namespace WcfClient
{
    public class AsansörCallback :IAsansorCallback
    {

        public void AsansörCycleHandler(PLC plc)
        {
            Console.WriteLine("Asansör Durumu:{0}",plc.İmalatKapıDurumu);
        }

    }         

}
