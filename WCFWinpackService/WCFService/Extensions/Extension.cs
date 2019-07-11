using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
//using System.Reflection;

using System.Text;

namespace Extensions
{
    public static class Extension
    {
        public static T MapTo<T>(this object source)
        {
            Type hedeftip = typeof(T);
            Type kaynaktip = source.GetType();
            T sonuc = Activator.CreateInstance<T>();
            PropertyInfo[] hedefözellikler = hedeftip.GetProperties();
            PropertyInfo[] kaynaközellikler = kaynaktip.GetProperties();
            foreach (PropertyInfo ko in kaynaközellikler)
            {
                PropertyInfo ho = hedefözellikler.FirstOrDefault(x => x.Name.ToLower() == ko.Name.ToLower());
                if (ho != null)
                {
                    object veri = ko.GetValue(source);
                    ho.SetValue(sonuc, veri);
                }
                
            }
            return sonuc;
        }
        public static bool CheckDatabaseExists(this SqlConnectionStringBuilder Constr)
        {

            bool result = false;

            string databaseName = Constr.InitialCatalog;
            string baseCon = Constr.ConnectionString.Replace(databaseName, "ITS_Client");
            string sqlCreateDBQuery = string.Format("SELECT database_id FROM sys.databases WHERE Name= '{0}'", databaseName);
            try
            {
                SqlConnection tmpConn = new SqlConnection(baseCon);


                using (tmpConn)
                {
                    using (SqlCommand sqlCmd = new SqlCommand(sqlCreateDBQuery, tmpConn))
                    {
                        tmpConn.Open();

                        object resultObj = sqlCmd.ExecuteScalar();

                        int databaseID = 0;

                        if (resultObj != null)
                        {
                            int.TryParse(resultObj.ToString(), out databaseID);
                        }

                        tmpConn.Close();

                        result = (databaseID > 0);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CheckDatabaseExists Hata:\n{ex.ToString()}".AppendLog());
                result = false;
            }

            return result;
        }
    }
}
