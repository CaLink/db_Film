using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db_Film.Model
{
    class DickSQL : MySQL
    {
        static public List<Dick> GetDick(string dick)
        {
            List<Dick> result = new List<Dick>();
            string queryMain = $"SELECT tbl_{dick}.id, tbl_{dick}.`Name` FROM tbl_{dick}";
            if (OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(queryMain, connection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                    while (dr.Read())
                        result.Add(new Dick
                        {
                            ID = dr.GetInt32("id"),
                            Name = dr.GetString("Name")
                        });
                CloseConnection();
            }
            return result;
        }
    }
}
