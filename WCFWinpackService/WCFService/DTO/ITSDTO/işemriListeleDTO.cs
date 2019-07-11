﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ITSDTO
{
    public partial class işemriListeleDTO
    {
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
        public bool ihracat { get; set; }
        public Nullable<System.DateTime> MFG { get; set; }
        public string MakinaIP { get; set; }
        public string Etiket { get; set; }
        public string EtiketIP { get; set; }
        public string Inkjet { get; set; }
        public string InkjetPort { get; set; }
        public string Kamera { get; set; }
        public string KameraIP { get; set; }
    }
}
