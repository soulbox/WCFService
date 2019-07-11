using Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Repository
{
    using Extensions;
    public class RepositoryBase<T, Context> : IRepository<T>, IDBContext<Context>
        where T : class
        where Context : DbContext
    {
        static Context db;
        public Context Db
        {
            get
            {
                db = db ?? Activator.CreateInstance<Context>();
                if (db.Configuration.LazyLoadingEnabled) db.Configuration.LazyLoadingEnabled = false;
                return db = db ?? Activator.CreateInstance<Context>();
            }
            set
            {
                db = value;
            }
        }



        public bool Güncelle(T entity)
        {
            try
            {

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Güncellede Sorun var\n{ex.ToString()}".AppendLog());
                return false;
            }
        }

        public List<T> Listele()
        {
            return Db.Set<T>().ToList();
        }
        public bool Ekle(T entity)
        {
            try
            {
                db.Set<T>().Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eklemede Sorun var\n{ex.ToString()}".AppendLog());
                return false;
            }

        }
        public bool Sil(T entity)
        {
            try
            {
                db.Set<T>().Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Silmede Sorun var\n{ex.ToString()}".AppendLog());
                return false;
            }
        }
    }
    public interface IRepository<T>
    {
        List<T> Listele();
        bool Ekle(T entity);
        bool Güncelle(T entity);
        bool Sil(T entity);
    }
    public interface IDBContext<Context> where Context : DbContext
    {
        Context Db { get; set; }
    }
}
