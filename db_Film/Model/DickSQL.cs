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
            string queryMain = $"SELECT tbl_{dick}.id, tbl_{dick}.`Name` FROM tbl_{dick} ORDER BY `Name`";
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

        public void InputDick(Dick dick, string tbl)
        {
            tbl = tbl.ToLower();
            dick.ID = ShowNextID($"tbl_{tbl}"); 
            string quety = $"insert into tbl_{tbl} value(0,@b)";
            if(OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(quety,connection))
                {
                    MySqlParameter p = new MySqlParameter("@b", MySqlDbType.String);
                    p.Value = dick.Name;
                    mc.Parameters.Add(p);

                    mc.ExecuteNonQuery();
                }

                CloseConnection();
            }
        }

        public void UpdateDick(Dick dick, string tbl)
        {
            tbl = tbl.ToLower();
            string query = $"update tbl_{tbl} set Name = @b where id={dick.ID}";
            if (OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, connection))
                {
                    MySqlParameter p = new MySqlParameter("@b", MySqlDbType.String);
                    p.Value = dick.Name;
                    mc.Parameters.Add(p);

                    mc.ExecuteNonQuery();
                }
                CloseConnection();
            }

        }

        public void OutputDick(Dick dick, string tbl)
        {
            tbl = tbl.ToLower();
            string query = $"delete from tbl_{tbl} where id={dick.ID}";
            ExecureNonQuery(query);
        }


    }
}
