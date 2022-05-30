using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using txhmontroland.Classes;
using MySql.Data.MySqlClient;

namespace txhmontroland.ADO
{
    public class AdoConcours
    {
        public static List<CodeConcour> getAll()
        {
            List<CodeConcour> cc = new List<CodeConcour>();
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM (enseignant e INNER JOIN cc_e_posseder p ON p.id_enseignant = e.id_enseignant) INNER JOIN codeconcour cc ON cc.id_codeconcour = p.id_codeconcour ORDER BY cc.id_codeconcour";
                reader = cmd.ExecuteReader();
                Boolean continuer = reader.Read();
                double VHCC = 0;
                double HSACC = 0;
                int idCC;
                idCC = reader.GetInt32(9);
                while (continuer)
                {
                    CodeConcour cc2 = new CodeConcour(reader.GetInt32(9), reader.GetString(10), reader.GetString(11), reader.GetString(12));
                    cc2.getMatierePossible();
                    Enseignant e = new Enseignant(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetDouble(4), reader.GetDouble(5), reader.GetDouble(6));
                    continuer = reader.Read();
                    cc2.addEnseignant(e);
                    foreach (Enseignant e2 in cc2.enseignants)
                    {

                        cc2.afficherNomEnseignants = e2.nom + " " + e2.prénom;
                        cc2.afficherContractEnseignants = e2.contract;
                        cc2.afficherEnseignantId = e2.id;

                        List<Travailler> t = AdoTravailler.getOneFromEnseignant(e.id);//On a besoin de récupérer le niveau et le taux horaire de la classe pour calculer les pondé
                        if (t.Count() != 0)
                        {
                            double volumeHoraireTotal = 0;
                            double pondeLegt = 0;
                            double pondeBts = 0;
                            double ajustementPonde = 0;
                            double totalPonde = 0;
                            double volumeHoraireLEP = 0;
                            double volumeHoraireLegt1T = 0;
                            double volumeHoraireLegt2nd = 0;
                            double volumeHoraireBts = 0;

                            foreach (Travailler t2 in t)
                            {
                                int niveau = t2.Classe.valeur_niveau; // 1 = 2nd, 2 = 1ere et Tale LEGT  3 = Bts  0 =LEP
                                if (niveau <= 0)
                                {
                                    volumeHoraireLEP += t2.volume_horaire;
                                    volumeHoraireTotal += t2.volume_horaire;
                                }
                                if (niveau == 1)
                                {
                                    volumeHoraireLegt2nd += t2.volume_horaire;
                                    volumeHoraireTotal += t2.volume_horaire;
                                }
                                if (niveau == 2)
                                {
                                    pondeLegt += t2.volume_horaire * 0.1;
                                    volumeHoraireTotal += t2.volume_horaire;
                                    volumeHoraireLegt1T += t2.volume_horaire;
                                    if (pondeLegt > 10)
                                    {
                                        pondeLegt = 1;
                                    }
                                }
                                if (niveau == 3)
                                {
                                    volumeHoraireTotal += t2.volume_horaire;
                                    volumeHoraireBts += t2.volume_horaire;
                                    pondeBts += t2.volume_horaire * 0.25;
                                }
                                totalPonde = pondeBts + pondeLegt;
                                if (volumeHoraireTotal > 18)
                                {
                                    ajustementPonde = totalPonde * 18 / volumeHoraireTotal;
                                }
                                else
                                {
                                    ajustementPonde = totalPonde;
                                }
                            }

                            e2.ajustementPonderation = ajustementPonde;
                            e2.totalPonde = totalPonde;

                            cc2.complementPoste = e2.ponderationLegt;
                            cc2.ajustementPonde = e2.ajustementPonderation;
                            cc2.AS = e2.ponderationBts;
                            cc2.afficherPondLegtEnseignants = pondeLegt;
                            cc2.afficherPondBtsEnseignants = pondeBts;
                            cc2.afficherTotalponde = e2.totalPonde;
                            cc2.afficherHeuresBts = volumeHoraireBts;
                            cc2.afficherHeuresLegt1T = volumeHoraireLegt1T;
                            cc2.afficherHeuresLegt2ND = volumeHoraireLegt2nd;
                            cc2.afficherHeuresLep = volumeHoraireLEP;
                            cc2.afficherTotalHEnseignants = e2.totalHeure();

                            if (volumeHoraireTotal > e2.heures_mini)
                            {
                                cc2.afficherHSA = volumeHoraireTotal - e2.heures_mini;
                            }
                            else
                            {
                                cc2.afficherHSA = 0;
                            }
                            cc.Add(cc2);
                        }
                        else
                        {
                            cc2.complementPoste = 0;
                            cc2.ajustementPonde = 0;
                            cc2.afficherPondLegtEnseignants = 0;
                            cc2.afficherPondBtsEnseignants = 0;
                            cc2.afficherTotalponde = 0;
                            cc2.afficherHeuresBts = 0;
                            cc2.afficherHeuresLegt1T = 0;
                            cc2.afficherHeuresLegt2ND = 0;
                            cc2.afficherHeuresLep = 0;
                            cc2.afficherTotalHEnseignants = 0;
                            cc2.complementPoste = e2.ponderationLegt;
                            cc2.AS = e2.ponderationBts;
                            cc.Add(cc2);
                        }

                    }
                }
                int idCode = cc.First().id;
                foreach (CodeConcour codeConcour in cc)
                {
                    if(cc.First(x => x.id == idCode).id == codeConcour.id)
                    { 
                        VHCC += codeConcour.afficherTotalHEnseignants;
                        HSACC += codeConcour.afficherHSA;
                    }
                    else
                    {
                        List<CodeConcour> cc20 = cc.FindAll(x => x.id == codeConcour.id - 1);
                        foreach (CodeConcour cc20_ in cc20)
                        {
                            cc20_.totalHSA = null;
                            cc20_.totalVH = null;
                        }
                        cc.Last(x => x.id == codeConcour.id-1).totalVH = VHCC;
                        cc.Last(x => x.id == codeConcour.id-1).totalHSA = HSACC;
                        VHCC = 0;
                        HSACC = 0;

                        idCode = codeConcour.id;

                        VHCC += codeConcour.afficherTotalHEnseignants;
                        HSACC += codeConcour.afficherHSA;
                    }                  
                }
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return cc;
        }

        public static List<CodeConcour> getAllLight()
        {
            List<CodeConcour> ccs = new List<CodeConcour>();
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM codeconcour";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CodeConcour cc = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                    ccs.Add(cc);
                }
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ccs;
        }
        public static CodeConcour getOneLight(int id)
        {
            CodeConcour cc = new CodeConcour();
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM codeconcour WHERE id_codeconcour = " + id;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cc = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                }
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return cc;
        }
        public static List<CodeConcour> GetFromEnseignant(Enseignant e)
        {
            List<CodeConcour> ccs = new List<CodeConcour>();
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM cc_e_posseder p INNER JOIN codeconcour cc ON p.id_codeconcour = cc.id_codeconcour WHERE id_enseignant = " + e.id;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CodeConcour cc = new(reader.GetInt32(0), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                    ccs.Add(cc);
                }
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ccs;
        }
        public static List<CodeConcour> GetOneFromMatiere(Matiere m)
        {
            List<CodeConcour> ccs = new List<CodeConcour>();
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM (cc_m_pouvoir po INNER JOIN codeconcour cc ON po.id_codeconcour = cc.id_codeconcour )INNER JOIN matiere m ON po.id_matiere = m.id_matiere WHERE po.id_matiere = " + m.id;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CodeConcour cc = new(reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                    ccs.Add(cc);
                }
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ccs;
        }
        public static List<Matiere> GetMatierePossibles(CodeConcour codeConcour)
        {
            List<Matiere> matieres = new List<Matiere>();
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM (cc_m_pouvoir po INNER JOIN matiere m ON po.id_matiere = m.id_matiere) INNER JOIN codeconcour cc ON cc.id_codeconcour = po.id_codeconcour WHERE cc.id_codeconcour = " + codeConcour.id;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Matiere m = new(reader.GetInt32(2), reader.GetString(3), reader.GetString(4));
                    matieres.Add(m);
                }
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return matieres;
        }

        public static void CreerConcours(CodeConcour codeConcour)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "INSERT INTO codeconcours (codeconcour, nom, couleur) VALUES ('" + codeConcour.codeconcour + "','" + codeConcour.nom + "','" + codeConcour.couleur + "')";
                cmd.ExecuteNonQuery();
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ModifierConcours(CodeConcour codeConcour)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "INSERT INTO codeconcours (codeconcour, nom, couleur) VALUES ('" + codeConcour.codeconcour + "','" + codeConcour.nom + "','" + codeConcour.couleur + "') WHERE id_codeconcour = " + codeConcour.id;
                cmd.ExecuteNonQuery();
                Ado.CloseSqlConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void SupprimerConcours(CodeConcour codeConcour)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "DELETE FROM codeconcour WHERE id_codeconcour = " + codeConcour.id;
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