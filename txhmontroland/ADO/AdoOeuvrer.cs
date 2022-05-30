using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using txhmontroland.Classes;
using MySql.Data.MySqlClient;
namespace txhmontroland.ADO
{
    public class AdoOeuvrer
    {
        public static List<Oeuvrer> getAll()
        {
            List<Oeuvrer> oe = new List<Oeuvrer>();
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM (c_e_oeuvrer oe INNER JOIN classe c ON oe.id_classe = c.id_classe) INNER JOIN enseignant e ON oe.id_enseignant = e.id_enseignant";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Oeuvrer oe2 = new Oeuvrer(new Enseignant(reader.GetInt16(1),reader.GetString(11),reader.GetString(12),reader.GetString(12)), new Classe(reader.GetInt16(0),reader.GetInt16(4),reader.GetString(5)), Convert.ToBoolean(reader.GetByte(2)));
                    oe.Add(oe2);
                }
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return oe;
        }
        public static List<Oeuvrer> getAllForEnseignant(Enseignant e)
        {
            List<Oeuvrer> oe = new List<Oeuvrer>();
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM (c_e_oeuvrer oe INNER JOIN classe c ON oe.id_classe = c.id_classe) INNER JOIN enseignant e ON oe.id_enseignant = e.id_enseignant WHERE oe.id_enseignant = " + e.id  ;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Oeuvrer oe2 = new Oeuvrer(new Enseignant(reader.GetInt16(1), reader.GetString(11), reader.GetString(12), reader.GetString(12)), new Classe(reader.GetInt16(0), reader.GetInt16(4), reader.GetString(5)), Convert.ToBoolean(reader.GetByte(2)));
                    oe.Add(oe2);
                }
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return oe;
        }
        public static List<Oeuvrer> getAllFromClasse(int id)
        {
            List<Oeuvrer> oe = new List<Oeuvrer>();
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM (c_e_oeuvrer oe INNER JOIN classe c ON oe.id_classe = c.id_classe) INNER JOIN enseignant e ON oe.id_enseignant = e.id_enseignant WHERE oe.id_classe = " + id;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Oeuvrer oe2 = new Oeuvrer(new Enseignant(reader.GetInt16(1), reader.GetString(11), reader.GetString(12), reader.GetString(12)), new Classe(reader.GetInt16(0), reader.GetInt16(4), reader.GetString(5)), Convert.ToBoolean(reader.GetByte(2)));
                    oe.Add(oe2);
                }
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return oe;
        }
        public static void creerOeuvrer(Oeuvrer o)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "INSERT INTO c_e_oeuvrer (id_classe,id_enseignant,is_pp) VALUES ( '" + o.classe.id + "','" + o.enseignant.id+ "','" + Convert.ToByte(o.is_pp) + "')";
                cmd.ExecuteNonQuery();
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void creerOeuvrer(Oeuvrer o,int idClasse)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "INSERT INTO c_e_oeuvrer (id_classe,id_enseignant,is_pp) VALUES ( '" + idClasse + "','" + o.enseignant.id + "','" + Convert.ToByte(o.is_pp) + "')";
                cmd.ExecuteNonQuery();
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static Boolean isPP(int idProf,int idClasse)
        {
            Boolean isPP = false;
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM c_e_oeuvrer WHERE id_enseignant =" + idProf + " AND id_classe = '" + idClasse + "'";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    isPP = Convert.ToBoolean(reader.GetByte(2));
                }
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return isPP;
        }
    }
}
