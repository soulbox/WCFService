using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DataCollect
{
    public class DB_Machine
    {
         SqlConnectionStringBuilder _strbuild = new SqlConnectionStringBuilder();
        public  SqlConnectionStringBuilder stringBuilder
        {
            get
            {
                _strbuild.DataSource = MakinaIP;
                _strbuild.InitialCatalog = "ITS_Client_" + İşEmriID;
                _strbuild.UserID = "sa";
                _strbuild.Password = "63792958";
                _strbuild.ConnectTimeout = 10;
                
                return _strbuild;
            }
            set { _strbuild = value; }
        }
        public int İşEmriID { get; set; }
        public string Makina { get; set; }
        public string Ürün_Adı { get; set; }
        public string SN { get; set; }
        public string MaxSn { get; set; }
        public int ÜretimMiktarı { get; set; }
        public string LOT { get; set; }
        public System.DateTime SKT { get; set; }
        public System.DateTime Tarih { get; set; }
        public bool Durum { get; set; }
        public string MakinaIP { get; set; }
        public string Etiket { get; set; }
        public string EtiketIP { get; set; }
        public string Inkjet { get; set; }
        public string InkjetPort { get; set; }
        public string Kamera { get; set; }
        public string KameraIP { get; set; }
        public int Count { get; set; }
        public bool İhracat { get; set; }
        public System.DateTime MFG { get; set; }


    }
}
