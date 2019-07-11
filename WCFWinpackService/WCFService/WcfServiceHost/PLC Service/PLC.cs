using EasyModbus;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;
using System.Runtime.Serialization;
using Extensions;

namespace WcfServiceHost
{
    //[DataContract]
    public class PLC : IIsıNem
    {
        public delegate void ErrorHandler(string msg);
        public delegate void CycleHandler(PLC Cycle);
        public event ErrorHandler ErrorEvent;
        public event CycleHandler CycleLight;
        public event CycleHandler CycleAsansör;
        public event CycleHandler CycleIsıNem;

        static ModbusClient _modbus;
        static ModbusClient modbus => _modbus = _modbus ?? new ModbusClient("10.0.0.9", 502) { ConnectionTimeout = 5000 };
        static bool started = false;

        static Boolean[] CoilsBuff = CoilsBuff ?? new Boolean[Adresler.Quantity.GetHashCode()];
        static Boolean[] Coils = Coils ?? new Boolean[Adresler.Quantity.GetHashCode()];
        //asansörrrrrrrrr
        static Boolean[] CoilsAsBuff = CoilsAsBuff ?? new Boolean[AdreslerAs.QuantityM.GetHashCode()];
        static Boolean[] CoilsAs = CoilsAs ?? new Boolean[AdreslerAs.QuantityM.GetHashCode()];

        static int[] RegistersAsBuff = RegistersAsBuff ?? new int[AdreslerAs.QuantityD.GetHashCode()];
        static int[] RegistersAs = RegistersAs ?? new int[AdreslerAs.QuantityD.GetHashCode()];

        static int[] RegistersAsKapBuff = RegistersAsKapBuff ?? new int[AdreslerKapı.Quantity.GetHashCode()];
        public static int[] RegistersAsKap = RegistersAsKap ?? new int[AdreslerKapı.Quantity.GetHashCode()];

        //bool yenis(Enum sdd) { return false; }

        static int[] RegisterIsıBuff = new int[AdreslerIsıNem.QuantityIsı.GetHashCode()];
        static int[] RegisterNemBuff = new int[AdreslerIsıNem.QuantityNem.GetHashCode()];
        static int[] RegisterIsı = new int[AdreslerIsıNem.QuantityIsı.GetHashCode()];
        static int[] RegisterNem = new int[AdreslerIsıNem.QuantityNem.GetHashCode()];

        enum AdreslerIsıNem
        {
            BaseD = 4096,
            Kat1_1Isı = BaseD + 200,
            Kat1_1Nem = BaseD + 300,
            Kat1_2Isı = BaseD + 201,
            Kat1_2Nem = BaseD + 301,
            Kat1_3Isı = BaseD + 202,
            Kat1_3Nem = BaseD + 302,
            Kat1_4Isı = BaseD + 203,
            Kat1_4Nem = BaseD + 303,
            Kat1_5Isı = BaseD + 204,
            Kat1_5Nem = BaseD + 304,
            Kat2_1Isı = BaseD + 210,
            Kat2_1Nem = BaseD + 310,
            Kat2_2Isı = BaseD + 211,
            Kat2_2Nem = BaseD + 311,
            Kat2_3Isı = BaseD + 212,
            Kat2_3Nem = BaseD + 312,
            Kat2_4Isı = BaseD + 213,
            Kat2_4Nem = BaseD + 313,
            Kat3_1Isı = BaseD + 220,
            Kat3_1Nem = BaseD + 320,
            FirstIsı = Kat1_1Isı,
            lastIsı = Kat3_1Isı,
            QuantityIsı = lastIsı - FirstIsı + 1,
            FirstNem = Kat1_1Nem,
            lastNem = Kat3_1Nem,
            QuantityNem = lastNem - FirstNem + 1,

        }
        enum AdreslerAs
        {
            İmalatGitAşağı = BaseM + 12,
            Flash1 = BaseM + 20,
            Flash2 = BaseM + 21,
            DepoGitAşaı = BaseM + 23,
            ZemineGönder = BaseM + 34,
            ÇatıyaGönder = BaseM + 42,
            İmalatGitYukarı = BaseM + 43,
            DepoGitYukarı = BaseM + 44,
            AsansörHareketEdebilir = BaseM + 50,

            Pozisyon = BaseD + 450,

            //Config
            //[Delta M]
            BaseM = 2048,
            FirstAdrM = İmalatGitAşağı,
            LastAdrM = AsansörHareketEdebilir,
            QuantityM = LastAdrM - FirstAdrM + 1,
            //[Delta D]
            BaseD = 4096,
            FirstAdrD = Pozisyon,
            LastAdrD = Pozisyon,
            QuantityD = LastAdrD - FirstAdrD + 1,
        }
        enum Adresler
        {

            BaseAdr = 2048,//delta plc
            Kat3_1 = BaseAdr + 512,
            Kat3_2 = BaseAdr + 513,
            Kat3_3 = BaseAdr + 514,
            Kat3_4 = BaseAdr + 515,
            Kat3_5 = BaseAdr + 516,
            Kat3_6 = BaseAdr + 517,
            Kat2_1 = BaseAdr + 521,
            Kat2_2 = BaseAdr + 522,
            Kat2_3 = BaseAdr + 523,
            Kat2_4 = BaseAdr + 524,
            Kat2_5 = BaseAdr + 525,
            Kat2_6 = BaseAdr + 526,
            Kat2_7 = BaseAdr + 527,
            Kat1_1 = BaseAdr + 531,
            Kat1_2 = BaseAdr + 532,
            Kat1_3 = BaseAdr + 533,
            Kat1_4 = BaseAdr + 534,
            Kat1_5 = BaseAdr + 535,
            Kat1_6 = BaseAdr + 536,
            FirstAdr = Kat3_1,
            LastAdr = Kat1_6,
            Quantity = (LastAdr - FirstAdr + 1)

        }
        //[DataContract]
        public enum KapıDurumları
        {
            //[EnumMember]
            boşta,
            //[EnumMember]
            Açılıyor,
            //[EnumMember]
            Kapanıyor
        }
        //[DataContract]
        enum AdreslerKapı
        {
            Base = 4096,
            ÇatıKapıDurumu = Base + 1,
            İmalatKapıDurumu = Base + 10,
            DepoKapıDurumu = Base + 20,
            ZeminKapıDurumu = Base + 40,

            FirstAdr = ÇatıKapıDurumu,
            LastAdr = ZeminKapıDurumu,
            Quantity = LastAdr - FirstAdr + 1
        }
        //[DataMember]
        public bool isConnected { get { return modbus.Connected; } set { } }
        public PLC()
        {
            if (started && modbus.Connected) return;
            try
            {
                modbus.Connect();
                "PLC Bağlantısı Başladı".AppendLog();
                started = true;
                Task.Factory.StartNew(() =>
               {
                   do
                   {
                       try
                       {
                           Coils = modbus.ReadCoils((int)Adresler.FirstAdr, (int)Adresler.Quantity);
                           CoilsAs = modbus.ReadCoils((int)AdreslerAs.FirstAdrM, (int)AdreslerAs.QuantityM);
                           RegistersAs = modbus.ReadHoldingRegisters((int)AdreslerAs.FirstAdrD, (int)AdreslerAs.QuantityD);
                           RegistersAsKap = modbus.ReadHoldingRegisters((int)AdreslerKapı.FirstAdr, (int)AdreslerKapı.Quantity);
                           RegisterIsı = modbus.ReadHoldingRegisters((int)AdreslerIsıNem.FirstIsı, (int)AdreslerIsıNem.QuantityIsı);
                           RegisterNem = modbus.ReadHoldingRegisters((int)AdreslerIsıNem.FirstNem, (int)AdreslerIsıNem.QuantityNem);


                           //CoilsAs[Index(AdreslerAs.Flash1)] = false;
                           //CoilsAs[Index(AdreslerAs.Flash2)] = false;
                           if (!CoilsBuff.SequenceEqual(Coils))
                           {
                               CoilsBuff = Coils;
                               CycleLight?.Invoke(this);
                           }
                           if (!RegistersAsBuff.SequenceEqual(RegistersAs) ||
                           !RegistersAsKapBuff.SequenceEqual(RegistersAsKap) ||
                           !CoilsAsBuff.SequenceEqual(CoilsAs)
                           )
                           //CoilsAsBuff[Index(AdreslerAs.İmalatGitAşağı)] != CoilsAs[Index(AdreslerAs.İmalatGitAşağı)] ||
                           //CoilsAsBuff[Index(AdreslerAs.DepoGitAşaı )] != CoilsAs[Index(AdreslerAs.DepoGitAşaı)] ||
                           //CoilsAsBuff[Index(AdreslerAs.DepoGitYukarı )] != CoilsAs[Index(AdreslerAs.DepoGitYukarı)] ||
                           //CoilsAsBuff[Index(AdreslerAs.İmalatGitYukarı)] != CoilsAs[Index(AdreslerAs.İmalatGitYukarı)] ||
                           //CoilsAsBuff[Index(AdreslerAs.ÇatıyaGönder)] != CoilsAs[Index(AdreslerAs.ÇatıyaGönder)] ||
                           //CoilsAsBuff[Index(AdreslerAs.ZemineGönder )] != CoilsAs[Index(AdreslerAs.ZemineGönder)] ||
                           //CoilsAsBuff[Index(AdreslerAs.AsansörHareketEdebilir)] != CoilsAs[Index(AdreslerAs.AsansörHareketEdebilir)] )
                           {
                               CoilsAsBuff = CoilsAs;
                               RegistersAsBuff = RegistersAs;
                               RegistersAsKapBuff = RegistersAsKap;
                               CycleAsansör?.Invoke(this);
                           }
                           if (!RegisterIsıBuff.SequenceEqual(RegisterIsı) ||
                                !RegisterNemBuff.SequenceEqual(RegisterNem))
                           {
                               RegisterIsıBuff = RegisterIsı;
                               RegisterNemBuff = RegisterNem;
                               CycleIsıNem?.Invoke(this);
                           }
                       }
                       catch (Exception ex)
                       {
                           Console.WriteLine($"PLC okuma Hatası:\n{ex.ToString()}".AppendLog());
                           //_modbus = null;
                           modbus.Disconnect();
                           _modbus = null;
                           Console.WriteLine($"PLC bağlantısı Kapatıldı 2s sonra tekrar bağlanıcak Plc Status:{modbus.Connected}".AppendLog());
                           Task.Delay(2000);
                           if (!modbus.Connected) modbus.Connect();
                           Console.WriteLine($"Plc Bağlandı. Status:{modbus.Connected}".AppendLog());
                       }

                   } while (modbus.Connected);
                   started = false;
                   ErrorEvent?.Invoke($"IP:{modbus.IPAddress}\nPLC'ye Bağlantı Koptu!".AppendLog());

               });
                //Task.Wait();
            }
            catch (EasyModbus.Exceptions.ConnectionException ex)
            {
                started = false;
                ErrorEvent?.Invoke($"IP:{modbus.IPAddress}\nPLC'ye Bağlanılamadı.\n{ex.ToString()}".AppendLog());
            }
            catch (Exception ex)
            {

                started = false;
                ErrorEvent?.Invoke($"IP:{modbus.IPAddress}\nPLC'ye Bağlanılamadı.\n{ex.ToString()}".AppendLog());
            }


        }
        //[DataMember]
        public object this[string propertyName]
        {
            get
            {
                // probably faster without reflection:
                // like:  return Properties.Settings.Default.PropertyValues[propertyName] 
                // instead of the following
                Type myType = typeof(PLC);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                return myPropInfo.GetValue(this, null);
            }
            set
            {
                Type myType = typeof(PLC);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                myPropInfo.SetValue(this, value, null);

            }

        }
        #region Işıklar
        //[DataMember]
        public Boolean Kat1_1
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];
            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        //[DataMember ]
        public Boolean Kat1_2
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];
            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        //[DataMember]
        public Boolean Kat1_3
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];
            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        //[DataMember]
        public Boolean Kat1_4
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        //[DataMember]
        public Boolean Kat1_5
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        //[DataMember]
        public Boolean Kat1_6
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        //[DataMember]
        public Boolean Kat2_1
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        //[DataMember]
        public Boolean Kat2_2
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        //[DataMember]
        public Boolean Kat2_3
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        //[DataMember]
        public Boolean Kat2_4
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        //[DataMember]
        public Boolean Kat2_5
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        //[DataMember]
        public Boolean Kat2_6
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        //[DataMember]
        public Boolean Kat2_7
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        //[DataMember]
        public Boolean Kat3_1
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        //[DataMember]
        public Boolean Kat3_2
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        //[DataMember]
        public Boolean Kat3_3
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        //[DataMember]
        public Boolean Kat3_4
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        //[DataMember]
        public Boolean Kat3_5
        {
            get
            {

                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        //[DataMember]
        public Boolean Kat3_6
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return Coils[Index(GetAdres(PropName))];
            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleCoil(GetAdres(PropName).GetHashCode(), value);
            }
        }
        //[DataMember]
        public Boolean Kat1
        {
            get
            {
                bool[] Dizi = { Kat1_1, Kat1_2, Kat1_3, Kat1_4, Kat1_5, Kat1_6 };
                return Dizi.All(x => x);
            }
            set
            {
                Kat1_1 = value;
                Kat1_2 = value;
                Kat1_3 = value;
                Kat1_4 = value;
                Kat1_5 = value;
                Kat1_6 = value;
            }
        }
        //[DataMember]
        public Boolean Kat2
        {
            get
            {
                bool[] Dizi = { Kat2_1, Kat2_2, Kat2_3, Kat2_4, Kat2_5, Kat2_6, Kat2_7 };
                return Dizi.All(x => x);
            }
            set
            {
                Kat2_1 = value;
                Kat2_2 = value;
                Kat2_3 = value;
                Kat2_4 = value;
                Kat2_5 = value;
                Kat2_6 = value;
                Kat2_7 = value;

            }
        }
        //[DataMember]
        public Boolean Kat3
        {
            get
            {
                bool[] Dizi = { Kat3_1, Kat3_2, Kat3_3, Kat3_4, Kat3_5, Kat3_6 };
                return Dizi.All(x => x);
            }
            set
            {
                Kat3_1 = value;
                Kat3_2 = value;
                Kat3_3 = value;
                Kat3_4 = value;
                Kat3_5 = value;
                Kat3_6 = value;

            }
        }
        //[DataMember]
        public Boolean Hepsi
        {
            get
            {
                bool[] Dizi = { Kat1, Kat2, Kat3 };
                return Dizi.All(x => x);
            }
            set
            {
                Kat1 = value;
                Kat2 = value;
                Kat3 = value;

            }
        }
        int Index(Adresler Data)
        {
            return Data.GetHashCode() - Adresler.FirstAdr.GetHashCode();
        }

        Adresler GetAdres(string str)
        {
            return (Adresler)Enum.Parse(typeof(Adresler), str);
        }
        #endregion

        #region Asansör
        //[DataContract]
        public enum Pozisyonlar
        {
            //[EnumMember]
            zemin,
            //[EnumMember]
            zemindepo,
            //[EnumMember]
            depo,
            //[EnumMember]
            depoimalat,
            //[EnumMember]
            imalat,
            //[EnumMember]
            imalatçatı,
            //[EnumMember]
            çatı
        }
        public Pozisyonlar Pozisyon
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return (Pozisyonlar)RegistersAs[Index(GetAdresAs(PropName), MorD.D)];
            }
            set { }
        }
        public Boolean AsansörHareketEdebilir
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                var a = GetAdresAs(PropName).GetHashCode() - AdreslerAs.FirstAdrM.GetHashCode();
                return CoilsAs[a];
            }
            set { }
        }
        private Boolean İmalatGitAşağı
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return CoilsAs[Index(GetAdresAs(PropName), MorD.M)];
            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                if (AsansörHareketEdebilir & Pozisyon == Pozisyonlar.çatı) modbus.WriteSingleCoil(GetAdresAs(PropName).GetHashCode(), value);
            }

        }
        private Boolean DepoGitAşaı
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return CoilsAs[Index(GetAdresAs(PropName), MorD.M)];

            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                if (AsansörHareketEdebilir & Pozisyon > Pozisyonlar.depoimalat) modbus.WriteSingleCoil(GetAdresAs(PropName).GetHashCode(), value);
            }

        }
        private Boolean İmalatGitYukarı
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return CoilsAs[Index(GetAdresAs(PropName), MorD.M)];
            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                if (AsansörHareketEdebilir & Pozisyon < Pozisyonlar.imalat) modbus.WriteSingleCoil(GetAdresAs(PropName).GetHashCode(), value);
            }

        }
        private Boolean DepoGitYukarı
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return CoilsAs[Index(GetAdresAs(PropName), MorD.M)];
            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                if (AsansörHareketEdebilir & Pozisyon < Pozisyonlar.depo) modbus.WriteSingleCoil(GetAdresAs(PropName).GetHashCode(), value);
            }

        }
        public Boolean imalataGit
        {
            get
            {
                return (İmalatGitYukarı | İmalatGitAşağı);
            }
            set
            {
                İmalatGitYukarı = value;
                İmalatGitAşağı = value;
            }
        }
        public Boolean DepoyaGit
        {
            get
            {
                return (DepoGitAşaı | DepoGitYukarı);
            }
            set
            {
                DepoGitAşaı = value;
                DepoGitYukarı = value;
            }
        }
        public Boolean ZemineGönder
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return CoilsAs[Index(GetAdresAs(PropName), MorD.M)];
            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                if (AsansörHareketEdebilir) modbus.WriteSingleCoil(GetAdresAs(PropName).GetHashCode(), value);
            }

        }
        public Boolean ÇatıyaGönder
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return CoilsAs[Index(GetAdresAs(PropName), MorD.M)];
            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                if (AsansörHareketEdebilir) modbus.WriteSingleCoil(GetAdresAs(PropName).GetHashCode(), value);
            }

        }
        AdreslerAs GetAdresAs(string str)
        {
            return (AdreslerAs)Enum.Parse(typeof(AdreslerAs), str);
        }
        int Index(AdreslerAs Data, MorD type)
        {
            if (type == MorD.M)
            {
                return Data.GetHashCode() - AdreslerAs.FirstAdrM.GetHashCode();
            }
            else
            {
                return Data.GetHashCode() - AdreslerAs.FirstAdrD.GetHashCode();
            }

        }
        enum MorD
        {
            M,
            D
        }
        #endregion
        //[DataMember]
        public KapıDurumları ÇatıKapıDurumu
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return (KapıDurumları)RegistersAsKap[Index(GetAdresAsKap(PropName))];
            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleRegister(GetAdresAsKap(PropName).GetHashCode(), (int)value);
            }


        }
        //[DataMember]
        public KapıDurumları İmalatKapıDurumu
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return (KapıDurumları)RegistersAsKap[Index(GetAdresAsKap(PropName))];
            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleRegister(GetAdresAsKap(PropName).GetHashCode(), (int)value);
            }


        }

        //[DataMember]
        public KapıDurumları DepoKapıDurumu
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return (KapıDurumları)RegistersAsKap[Index(GetAdresAsKap(PropName))];
            }
            set
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                modbus.WriteSingleRegister(GetAdresAsKap(PropName).GetHashCode(), (int)value);
            }


        }
        //[DataMember]
        public KapıDurumları ZeminKapıDurumu
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return (KapıDurumları)RegistersAsKap[Index(GetAdresAsKap(PropName))];
            }
            set
            {
                //throw new NotSupportedException();
            }
            //set
            //{
            //    string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
            //    modbus.WriteSingleRegister(GetAdresAsKap(PropName).GetHashCode(), (int)value);
            //}


        }

        AdreslerKapı GetAdresAsKap(string str)
        {
            return (AdreslerKapı)Enum.Parse(typeof(AdreslerKapı), str);
        }
        int Index(AdreslerKapı Data)
        {

            return Data.GetHashCode() - AdreslerKapı.FirstAdr.GetHashCode();
        }

        #region "ISINem"
        AdreslerIsıNem GetAdresIsıNem(string str)
        {
            return (AdreslerIsıNem)Enum.Parse(typeof(AdreslerIsıNem), str);
        }

        int Index(AdreslerIsıNem Data, bool isIsı)
        {
            if (isIsı)
                return Data.GetHashCode() - AdreslerIsıNem.FirstIsı.GetHashCode();
            else
                return Data.GetHashCode() - AdreslerIsıNem.FirstNem.GetHashCode();
        }
        public float Kat1_1Isı
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return (float)RegisterIsı[Index(GetAdresIsıNem(PropName), true)] / 10;
            }

            set
            {


            }
        }

        public float Kat1_1Nem
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return (float)RegisterNem[Index(GetAdresIsıNem(PropName), false)] / 10;
            }

            set
            {

            }
        }

        public float Kat1_2Isı
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return (float)RegisterIsı[Index(GetAdresIsıNem(PropName), true)] / 10;
            }

            set
            {

            }
        }

        public float Kat1_2Nem
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return (float)RegisterNem[Index(GetAdresIsıNem(PropName), false)] / 10;
            }

            set
            {

            }
        }

        public float Kat1_3Isı
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return (float)RegisterIsı[Index(GetAdresIsıNem(PropName), true)] / 10;
            }

            set
            {

            }
        }

        public float Kat1_3Nem
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return (float)RegisterNem[Index(GetAdresIsıNem(PropName), false)] / 10;
            }

            set
            {

            }
        }

        public float Kat1_4Isı
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return (float)RegisterIsı[Index(GetAdresIsıNem(PropName), true)] / 10;
            }

            set
            {

            }
        }

        public float Kat1_4Nem
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return (float)RegisterNem[Index(GetAdresIsıNem(PropName), false)] / 10;
            }

            set
            {

            }
        }

        public float Kat1_5Isı
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return (float)RegisterIsı[Index(GetAdresIsıNem(PropName), true)] / 10;
            }

            set
            {

            }
        }

        public float Kat1_5Nem
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return (float)RegisterNem[Index(GetAdresIsıNem(PropName), false)] / 10;
            }

            set
            {

            }
        }

        public float Kat2_1Isı
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return (float)RegisterIsı[Index(GetAdresIsıNem(PropName), true)] / 10;
            }

            set
            {

            }
        }

        public float Kat2_1Nem
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return (float)RegisterNem[Index(GetAdresIsıNem(PropName), false)] / 10;
            }

            set
            {

            }
        }

        public float Kat2_2Isı
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return (float)RegisterIsı[Index(GetAdresIsıNem(PropName), true)] / 10;
            }

            set
            {

            }
        }

        public float Kat2_2Nem
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return (float)RegisterNem[Index(GetAdresIsıNem(PropName), false)] / 10;
            }

            set
            {

            }
        }

        public float Kat2_3Isı
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return (float)RegisterIsı[Index(GetAdresIsıNem(PropName), true)] / 10;
            }

            set
            {

            }
        }

        public float Kat2_3Nem
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return (float)RegisterNem[Index(GetAdresIsıNem(PropName), false)] / 10;
            }

            set
            {

            }
        }

        public float Kat2_4Isı
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return (float)RegisterIsı[Index(GetAdresIsıNem(PropName), true)] / 10;
            }

            set
            {

            }
        }

        public float Kat2_4Nem
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return (float)RegisterNem[Index(GetAdresIsıNem(PropName), false)] / 10;
            }

            set
            {

            }
        }

        public float Kat3_1Isı
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return (float)RegisterIsı[Index(GetAdresIsıNem(PropName), true)] / 10;
            }

            set
            {

            }
        }

        public float Kat3_1Nem
        {
            get
            {
                string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
                return (float)RegisterNem[Index(GetAdresIsıNem(PropName), false)] / 10;
            }

            set
            {

            }
        }
        #endregion

    }

}
