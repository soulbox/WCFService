using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    using Entity;
    using DTO.DataCollect;
    using Extensions;
    using System.Data.SqlClient;

    public class MachineRepository : IMachineRepository, IDBContext<ITS_ServerEntities>
    {
        static ITS_ServerEntities db;

        const string Query = @"select * from işemriListele where işEmriID in (
select max(işEmriID) from işemriListele 
where MakinaIP  like '%192%'
group by MakinaIP )
order by Makina ";

        public ITS_ServerEntities Db
        {
            get
            {
                return db = db ?? new ITS_ServerEntities();
            }
            set
            {
                db = value;
            }
        }
        public List<DB_Machine> Listele()
        {

            var Idler = Db.işemriListele
                .Where(x => x.MakinaIP.Contains("192"))
                .GroupBy(i => i.Makina)
                .Select(g => new
                {
                    ID = g.Max(row => row.İşEmriID)
                });

            var dönücek = Db.işemriListele
                .Where(x => Idler.Select(a => a.ID).Contains(x.İşEmriID)).ToList()
                .Select(x => x.MapTo<DB_Machine>())
                .ToList()
                .OrderBy(x=>x.Makina)
                .ToList();

            dönücek.ForEach(x =>
            {
                if (string.IsNullOrEmpty(x.MakinaIP) & x.İşEmriID == 0)
                {
                    x.Count = 0;
                    return;
                }

                if ( !x.stringBuilder.CheckDatabaseExists())
                {
                    Console.WriteLine($"Makine:{x.Makina}\tIP:{x.MakinaIP}\tDB:{x.stringBuilder.InitialCatalog} Bulunamadı!.".AppendLog());
                    x.Count = 0;
                    return;
                }                    

                try
                {

                    using (ITS_ClientEntities Mac = new ITS_ClientEntities())
                    {
                        Mac.Database.Connection.Close();
                        Mac.Database.Connection.ConnectionString = x.stringBuilder.ConnectionString;
                        if (Mac.Database.Connection.State != System.Data.ConnectionState.Open)
                            Mac.Database.Connection.Open();
                        x.Count = Mac.FirmaÜrünClient.Count();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Makine Listelemede Hata\n{ex.ToString()}".AppendLog());
                }
            });
            return dönücek;

        }
    }
    public interface IMachineRepository
    {
        List<DB_Machine> Listele();
    }
}
