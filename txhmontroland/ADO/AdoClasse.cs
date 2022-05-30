using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using txhmontroland.Classes;
using MySql.Data.MySqlClient;

namespace txhmontroland.ADO
{
    public class AdoClasse
    {
        public static List<Classe> getAll()
        {
            List<Classe> c = new List<Classe>();
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM Classe";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    c.Add(new Classe(reader.GetInt32(0), reader.GetInt16(1), reader.GetString(2),reader.GetString(3),reader.GetString(4),reader.GetInt16(5),reader.GetDouble(6)));
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return c;
        }
        public static Classe getOne(int id)
        {
            Classe c = new Classe();
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM Classe WHERE id_classe = " + id;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    c = new Classe(reader.GetInt32(0), reader.GetInt16(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt16(5), reader.GetDouble(6));
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return c;
        }
        public static int creerClasse(Classe c)
        {
            int idClasse = 0;
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "INSERT INTO classe (nombre_eleve, nom_classe, type_formation,valeur_niveau,division) VALUES ('" + c.nombre_eleve + "','" + c.nom_classe + "','" + c.type_formation + "','" + c.valeur_niveau + "','" + c.division + "')";
                cmd.ExecuteNonQuery();
                idClasse = Convert.ToInt32(cmd.LastInsertedId);
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return idClasse;
        }
        public static List<Classe> getAllAvecVH()
        {
            List<Classe> c = new List<Classe>();
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM classe c INNER JOIN c_e_m_travailler t ON c.id_classe = t.id_classe";
                reader = cmd.ExecuteReader();
                Boolean continuer = reader.Read();
                int idClasse = reader.GetInt16(0);
                while (continuer)
                {
                    Classe c1 = new Classe(reader.GetInt32(0), reader.GetInt16(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt16(5), reader.GetDouble(6));
                    while(c1.id == idClasse && continuer)
                    {
                        c1.afficherTotalHeure += reader.GetDouble(10);
                        continuer = reader.Read();
                        if (continuer)
                        {
                            idClasse = reader.GetInt16(0);
                        }
                    }
                    c.Add(c1);
                }
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return c;
        }
        public static List<Classe> getMatiereClasses()
        {
            List<Classe> c = new List<Classe>();
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM (c_e_m_travailler t INNER JOIN  matiere m ON m.id_matiere = t.id_matiere) INNER JOIN classe c ON t.id_classe = c.id_classe";
                reader = cmd.ExecuteReader();
                Boolean continuer = reader.Read();
                int idClasse = reader.GetInt16(0);
                int idMatiere = reader.GetInt32(2);
                while (continuer)
                {                                    //Id                 //NbEleve          //Nom      
                    Classe classe = new Classe(reader.GetInt16(0), reader.GetInt32(9),reader.GetString(10),reader.GetString(11),reader.GetString(12),reader.GetInt32(13),reader.GetDouble(14));
                    while (classe.id == idClasse && continuer)
                    {
                        Matiere matiere = new(reader.GetInt32(2), reader.GetString(6), reader.GetString(7));
                        while (matiere.id == idMatiere && continuer)
                        {
                            matiere.afficherTotalHeure += reader.GetDouble(3);
                            continuer = reader.Read();
                            if (continuer)
                            {
                                idMatiere = reader.GetInt32(2);
                            }
                        }      
                        classe.addMatiere(matiere);
                        if (continuer)
                        {
                            idClasse = reader.GetInt16(0);
                        }
                    }
                    c.Add(classe);
                }
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return c;
        }
    }
}

