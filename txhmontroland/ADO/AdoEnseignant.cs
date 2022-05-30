using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using txhmontroland.Classes;
using MySql.Data.MySqlClient;

namespace txhmontroland.ADO
{
    public class AdoEnseignant
    {
        public static List<Enseignant> getAll()
        {
            List<Enseignant> e = new List<Enseignant>();
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM (((enseignant e INNER JOIN cc_e_posseder po ON po.id_enseignant = e.id_enseignant )INNER JOIN codeconcour c ON po.id_codeconcour = c.id_codeconcour) INNER JOIN cc_m_pouvoir p ON p.id_codeconcour = c.id_codeconcour) INNER JOIN matiere m ON m.id_matiere = p.id_matiere ORDER BY e.id_enseignant";
                reader = cmd.ExecuteReader();
                Boolean continuer = reader.Read();
                int idEnseignant;
                idEnseignant = reader.GetInt32(0);
                while (continuer)
                {                 
                    Enseignant en = new Enseignant(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5),reader.GetDouble(6));
                    while(en.id == idEnseignant && continuer)
                    {
                        Matiere m = new Matiere(reader.GetInt32(15), reader.GetString(16), reader.GetString(17));
                        CodeConcour cc = new CodeConcour(reader.GetInt32(9), reader.GetString(10),reader.GetString(11),reader.GetString(12));
                        en.addCodeConcour(cc);
                        en.addMatiere(m);
                        continuer = reader.Read();                        
                        if (continuer)
                        {
                            idEnseignant = reader.GetInt32(0);
                        }
                        foreach (CodeConcour cc2 in en.CodesConcours)
                        {
                            en.afficherCodeConcour += cc.codeconcour + "\n";
                        }
                    }
                    e.Add(en);
                }
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return e;
        }
        public static List<Enseignant> getAllLight()
        {
            List<Enseignant> e = new List<Enseignant>();
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM enseignant";
                reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    Enseignant en = new Enseignant(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetDouble(6));
                    e.Add(en);
                }
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return e;
        }
        public static Enseignant getOne(int id)
        {
            Enseignant e = new Enseignant();
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM (((enseignant e INNER JOIN cc_e_posseder po ON po.id_enseignant = e.id_enseignant )INNER JOIN codeconcour c ON po.id_codeconcour = c.id_codeconcour) INNER JOIN cc_m_pouvoir p ON p.id_codeconcour = c.id_codeconcour) INNER JOIN matiere m ON m.id_matiere = p.id_matiere WHERE e.id_enseignant = " + id;
                reader = cmd.ExecuteReader();
                Boolean continuer = reader.Read();
                int idEnseignant;
                idEnseignant = reader.GetInt32(0);
                    e = new Enseignant(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetDouble(6));
                    while (e.id == idEnseignant && continuer)
                    {
                        Matiere m = new Matiere(reader.GetInt32(15), reader.GetString(16), reader.GetString(17));
                        CodeConcour cc = new CodeConcour(reader.GetInt32(9), reader.GetString(10), reader.GetString(11), reader.GetString(12));
                        e.addCodeConcour(cc);
                        e.addMatiere(m);
                        continuer = reader.Read();
                        if (continuer)
                        {
                            idEnseignant = reader.GetInt32(0);
                        }
                    }
                
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return e;
        }
        public static Enseignant getOneLight(int id)
        {
            Enseignant en = new Enseignant();
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM enseignant WHERE id_enseignant = " +id;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                     en =  new Enseignant(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetDouble(6));
                }
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return en;
        }

        public static void creerEnseignant(Enseignant e)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "INSERT INTO enseignant (nom, prenom, contract, heures_mini) VALUES ('" + e.nom + "','" + e.prénom + "','" + e.contract + "','" + e.heures_mini + "')";
                cmd.ExecuteNonQuery();
                foreach (CodeConcour cc in e.CodesConcours)
                {
                    cmd.CommandText = "INSERT INTO cc_e_posseder (id_codeconcour, id_enseignant) VALUES ('" + cc.id + "','" + e.id + "')";
                    cmd.ExecuteNonQuery();
                }
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void modifierEnseignant(Enseignant e)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "UPDATE enseignant SET nom = '" + e.nom + "',prenom = '"+ e.prénom + "', contract = '" + e.contract + "', heures_mini = '" + e.heures_mini +"' WHERE id_enseignant = "+e.id;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM cc_e_posseder WHERE id_enseignant = " + e.id;
                cmd.ExecuteNonQuery();

                foreach (CodeConcour cc in e.CodesConcours)
                {
                    cmd.CommandText = "INSERT INTO cc_e_posseder (id_codeconcour, id_enseignant) VALUES ('" + cc.id +"','" + e.id +"')";
                    cmd.ExecuteNonQuery();
                }
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void modifierComplementEnseignant(Enseignant e)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "UPDATE enseignant SET ponderation_legt = '" + e.ponderationLegt + "',ponderation_bts = '"+ e.ponderationBts +"' WHERE id_enseignant = " + e.id;
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Ado.CloseSqlConnection();
        }

        public static void supprimerEnseignant(Enseignant e)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "DELETE FROM enseignant WHERE id_enseignant = " + e.id;
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Ado.CloseSqlConnection();
        }
    }
}
