using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using txhmontroland.Classes;
using MySql.Data.MySqlClient;

namespace txhmontroland.ADO
{
    public class AdoMatiere
    {
        public static List<Matiere> getAll()
        {
            List<Matiere> t = new List<Matiere>();
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM matiere ";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    t.Add(new Matiere(reader.GetInt16(0), reader.GetString(1), reader.GetString(2)));
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return t;
        }
        public static List<Matiere> getAllAvecEnseignant()
        {
            List<Matiere> ms = new List<Matiere>();
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM (((enseignant e INNER JOIN cc_e_posseder p ON e.id_enseignant = p.id_enseignant) INNER JOIN codeconcour cc ON p.id_codeconcour = cc.id_codeconcour) INNER JOIN cc_m_pouvoir pv ON cc.id_codeconcour = pv.id_codeconcour) INNER JOIN matiere m ON pv.id_matiere = m.id_matiere ORDER BY m.id_matiere";
                reader = cmd.ExecuteReader();
                Boolean continuer = reader.Read();
                int idMatiere;
                idMatiere = reader.GetInt32(15);
                while (continuer)
                {
                    Matiere ma = new Matiere(reader.GetInt32(15), reader.GetString(16), reader.GetString(17));
                    while (ma.id == idMatiere && continuer)
                    {
                        Enseignant e = new Enseignant(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetDouble(6));
                        ma.AddEnseignant(e);
                        continuer = reader.Read();
                        if (continuer)
                        {
                            idMatiere = reader.GetInt32(15);
                        }
                    }
                    ms.Add(ma);
                }
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ms;
        }

        public static Matiere getOne(int id)
        {
            Matiere m = new Matiere();
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM Matiere WHERE id_matiere = " + id;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    m = new Matiere(reader.GetInt16(0), reader.GetString(1), reader.GetString(2));
                }
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return m;
        }
        public static void CreerMatiere(Matiere m)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "INSERT INTO matiere (nom_matiere, sigle_matiere) VALUES ('" + m.nom_matiere + "','" + m.sigle_matiere + "')";
                cmd.ExecuteNonQuery();
                int id_matiere = Convert.ToInt32(cmd.LastInsertedId);
                foreach (CodeConcour cc in m.codeConcours)
                {
                    cmd.CommandText = "INSERT INTO cc_m_pouvoir (id_codeconcour, id_matiere) VALUES ('" + cc.id + "','" + id_matiere + "')";
                    cmd.ExecuteNonQuery();
                }
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void ModifierMatiere(Matiere matiere)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "UPDATE matiere SET nom_matiere = '" + matiere.nom_matiere + "',sigle_matiere = '" + matiere.sigle_matiere + "' WHERE id_matiere = " + matiere.id;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM cc_m_pouvoir WHERE id_matiere = " + matiere.id;
                cmd.ExecuteNonQuery();

                foreach (CodeConcour cc in matiere.codeConcours)
                {
                    cmd.CommandText = "INSERT INTO cc_m_pouvoir (id_codeconcour, id_matiere) VALUES ('" + cc.id + "','" + matiere.id + "')";
                    cmd.ExecuteNonQuery();
                }
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void SupprimerMatiere(Matiere matiere)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "DELETE FROM matiere WHERE id_matiere = " + matiere.id;
                cmd.ExecuteNonQuery();
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
