using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace txhmontroland.ADO 
{
    public class Ado
    {
        static MySqlConnection connection;
        public static MySqlConnection OpenSqlConnection()
        {
            string connectionString = GetConnectionString();
            connection = new MySqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();
            Console.WriteLine("State: {0}", connection.State);
            Console.WriteLine("ConnectionString: {0}", connection.ConnectionString);
            return connection;
        }
        public static void CloseSqlConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        static private string GetConnectionString()
        {
            return "Data Source=letouzicethan.fr;Initial Catalog=letouzic_emilien_bdd;User ID=letouzic_emilien;Password=7709577Aa*";
        }

        public static void SaveDataBase()
        {
            int anneeActuelle = DateTime.Today.Year;
            int anneeDerniere = DateTime.Today.Year - 1;
            try
            {
                MySqlCommand cmd = new();
                cmd.Connection = OpenSqlConnection();
                cmd.CommandText = "CREATE thxmontroland" + anneeDerniere + "-" + anneeActuelle ;
                MySqlScript script = new(connection, File.ReadAllText("script.sql"));
                script.Delimiter = "$$";
                script.Execute();
                CloseSqlConnection(); 
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
