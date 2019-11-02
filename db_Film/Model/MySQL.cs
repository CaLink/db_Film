using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace db_Film
{
    internal class MySQL
    {
        protected static MySqlConnection connection;

        internal static void SetUpMySqlConnection()
        {
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
            sb.Server = Properties.Settings.Default.server;
            sb.Database = Properties.Settings.Default.db;
            sb.UserID = Properties.Settings.Default.user;
            sb.Password = Properties.Settings.Default.password;
            sb.CharacterSet = "utf8";
            connection = new MySqlConnection(sb.ToString());
        }

        static protected bool OpenConnection()
        {
            try
            {
                if (connection == null)
                    SetUpMySqlConnection();
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message + "\nКакая досада \nAccess Denied");
                return false;
            }
        }

        static protected void CloseConnection()
        {
            try
            {
                connection.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message + "\nКакая досада \nAccess Denied");
            }
        }

        static internal bool TestConnection()
        {
            try
            {
                connection.Open();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message + "\nКакая досада \nAccess Denied");
                return false;
            }
        }

        protected int ShowNextID(string table)
        {
            int result = -1;
            string query = $"show table status where `Name` = '{table}'";
            if (OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, connection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                    while (dr.Read())
                        result = dr.GetInt32("Auto_increment");
                CloseConnection();
            }
            return result;
        }

        protected void ExecureNonQuery(string query)
        {
            if (OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, connection))
                    mc.ExecuteNonQuery();
                CloseConnection();
            }
        }

    }
}
