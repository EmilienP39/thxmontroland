using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace txhmontroland.ADO
{
    internal class AdoLogin
    {
        public static string[] getLogs()
        {
            string utilisateur ="";
            string password="";
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM identifiants";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    utilisateur = reader.GetString(1);
                    password = reader.GetString(2);
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            string[] logs = { utilisateur, password };
            return logs;
        }
    }
}
