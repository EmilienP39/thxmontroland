using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using txhmontroland.Classes;
using MySql.Data.MySqlClient;

namespace txhmontroland.ADO
{
    public class AdoTravailler
    {
        public static List<Travailler> getAll()
        {
            List<Travailler> t = new List<Travailler>();
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM c_e_m_travailler t INNER JOIN enseignant e ON t.id_enseignant = e.id_enseignant ORDER BY e.id_enseignant ";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Travailler tr = new Travailler(new Enseignant(reader.GetInt32(1), reader.GetString(6), reader.GetString(7), reader.GetString(8)), reader.GetDouble(3),reader.GetDouble(4)); ;
                    t.Add(tr);
                }
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return t;
        }
        public static List<Travailler> getOneFromEnseignant(int id)
        {
            List<Travailler> t = new List<Travailler>();
            Travailler t1 = new Travailler();
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM ((c_e_m_travailler t INNER JOIN enseignant e ON t.id_enseignant = e.id_enseignant) INNER JOIN matiere m ON t.id_matiere = m.id_matiere) INNER JOIN classe c ON t.id_classe = c.id_classe WHERE t.id_enseignant = " + id;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    double hES = 0;
                    double hLegt1T =0;
                    double hLegt2 =0;
                    double hLep =0;
                    t1 = new Travailler(new Enseignant(reader.GetInt32(5), reader.GetString(6), reader.GetString(7), reader.GetString(8),reader.GetDouble(11)),new Matiere(reader.GetInt32(2),reader.GetString(13), reader.GetString(14)),new Classe(reader.GetInt32(0),reader.GetInt32(16),reader.GetString(17),reader.GetString(18),reader.GetString(19),reader.GetInt32(20), reader.GetDouble(21)),reader.GetDouble(3),reader.GetDouble(4));
                    if(t1.Classe.type_formation =="ES")
                    {
                        hES = t1.volume_horaire;
                    }
                    if(t1.Classe.type_formation == "LEGT1°T°")
                    {
                        hLegt1T = t1.volume_horaire;
                    }
                    if(t1.Classe.type_formation == "LEGT2°")
                    {
                        hLegt2 = t1.volume_horaire;
                    }
                    if(t1.Classe.type_formation == "LEP")
                    {
                        hLep = t1.volume_horaire;
                    }
                    t1.afficherHBts = hES;
                    t1.afficherHLegt1T = hLegt1T;
                    t1.afficherHLegt2 = hLegt2;
                    t1.afficherHLEP = hLep;
                    t1.afficherNomClasse = t1.Classe.nom_classe;
                    t1.afficherNomMatiere = t1.matiere.nom_matiere;
                    t.Add(t1);
                }
                Ado.CloseSqlConnection();              
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return t;
        }
        public static List<Travailler> getFromClasse(int id)
        {
            List<Travailler> t = new List<Travailler>();
            Travailler t1 = new Travailler();
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM ((c_e_m_travailler t INNER JOIN enseignant e ON t.id_enseignant = e.id_enseignant) INNER JOIN matiere m ON t.id_matiere = m.id_matiere) INNER JOIN classe c ON t.id_classe = c.id_classe WHERE t.id_classe = " + id;
                reader = cmd.ExecuteReader();
                double hES = 0;
                double hLegt1T = 0;
                double hLegt2 = 0;
                double hLep = 0;
                while (reader.Read())
                {
                    t1 = new Travailler(new Enseignant(reader.GetInt32(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetDouble(11)), new Matiere(reader.GetInt32(2), reader.GetString(13), reader.GetString(14)), new Classe(reader.GetInt32(0), reader.GetInt32(16), reader.GetString(17), reader.GetString(18), reader.GetString(19), reader.GetInt32(20), reader.GetDouble(21)), reader.GetDouble(3), reader.GetDouble(4));
                    t.Add(t1);
                    if (t1.Classe.type_formation == "ES")
                    {
                        hES = t1.volume_horaire;
                    }
                    if (t1.Classe.type_formation == "LEGT1T")
                    {
                        hLegt1T = t1.volume_horaire;
                    }
                    if (t1.Classe.type_formation == "LEGT2")
                    {
                        hLegt2 = t1.volume_horaire;
                    }
                    if (t1.Classe.type_formation == "LEP")
                    {
                        hLep = t1.volume_horaire;
                    }
                    t1.afficherHBts = hES;
                    t1.afficherHLegt1T = hLegt1T;
                    t1.afficherHLegt2 = hLegt2;
                    t1.afficherHLEP = hLep;
                    t1.afficherNomClasse = t1.Classe.nom_classe;
                    t1.afficherNomMatiere = t1.matiere.nom_matiere;
                }
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return t;
        }
        public static void CreerTravailler(Travailler t,int idClasse)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "INSERT INTO c_e_m_travailler (id_enseignant, id_classe,id_matiere,volume_horaire,horaire_referenciel) VALUES('" + t.enseignant.id + "','" + idClasse + "','" + t.matiere.id + "', '" + t.volume_horaire + "','" + t.horaire_referentiel +"')";
                cmd.ExecuteNonQuery();
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static List<double> getDotationPricipale()
        {
            List<double> dotation = new List<double>();
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM dotation_principale ";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    double dotation1 = reader.GetDouble(1);
                    double DDFPT = reader.GetDouble(2);
                    double DOC = reader.GetDouble(3);
                    dotation.Add(dotation1);
                    dotation.Add(DDFPT);
                    dotation.Add(DOC);
                }
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dotation;
        }

        public static void modifierDotationPrincipale(double dotation)
        {
            string dotaString = Convert.ToString(dotation).Replace(",", ".");
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "UPDATE dotation_principale SET dotation =" + dotaString;
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Ado.CloseSqlConnection();
        }
        public static void modifierDDFPT(double dotation)
        {
            string dotaString = Convert.ToString(dotation).Replace(",", ".");
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "UPDATE dotation_principale SET DDFPT =" + dotaString;
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Ado.CloseSqlConnection();
        }
        public static void modifierDOC(double dotation)
        {
            string dotaString = Convert.ToString(dotation).Replace(",",".");
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "UPDATE dotation_principale SET DOC = " + dotaString;
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                 Console.WriteLine(ex.Message);
            }
            Ado.CloseSqlConnection();
        }

        public static void supprimerTravaillerFromClasse(Classe c)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "DELETE FROM c_e_m_travailler WHERE id_classe = " + c.id ;
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