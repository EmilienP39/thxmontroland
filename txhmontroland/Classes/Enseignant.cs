using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using txhmontroland.ADO;

namespace txhmontroland.Classes
{
    public class Enseignant
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string prénom { get; set; }
        public string contract { get; set; }
        public double ponderationLegt { get; set; } //Complément de poste
        public double ponderationBts { get; set; } //AS
        public double heures_mini { get; set; }
        public double totalPonde { get; set; }
        public double ajustementPonderation { get; set; }
        public List<CodeConcour> CodesConcours { get; set; }
        public List<Matiere> matieres { get; set; }
        public string afficherCodeConcour { get; set; }
        public string afficherORS { get; set; }

        public Enseignant() { }
        public Enseignant(int id_, string contract_,string nom_, string prénom_,double ponderationLegt_, double ponderationBts_,double heures_mini_)
        {
            this.id = id_;
            this.nom = nom_;
            this.prénom = prénom_;
            this.contract = contract_;
            this.heures_mini = heures_mini_;
            this.ponderationLegt = ponderationLegt_;
            this.ponderationBts = ponderationBts_;
            this.CodesConcours = new List<CodeConcour>();
            this.matieres = new List<Matiere>();
            this.afficherCodeConcour = "";
            this.ajustementPonderation = 0;
            this.totalPonde = 0;
            this.afficherORS = "";
        }
        public Enseignant(string contract_, string nom_, string prénom_, double heures_mini, List<CodeConcour> codeConcours)
        {
            this.id = 0;
            this.nom = nom_;
            this.prénom = prénom_;
            this.contract = contract_;
            this.heures_mini = heures_mini;
            this.ponderationLegt = 0;
            this.ponderationBts = 0;
            this.CodesConcours = codeConcours;
            this.matieres = new List<Matiere>();
            this.afficherCodeConcour = "";
            this.ajustementPonderation = 0;
            this.totalPonde = 0;
            this.afficherORS = "";
        }

        public Enseignant(int id_, string contract_, string nom_, string prénom_, double heures_mini_)
        {
            this.id = id_;
            this.nom = nom_;
            this.prénom = prénom_;
            this.contract = contract_;
            this.heures_mini = heures_mini_;
            this.ponderationLegt = 0;
            this.ponderationBts = 0;
            this.CodesConcours = new List<CodeConcour>();
            this.matieres = new List<Matiere>();
            this.afficherCodeConcour = "";
            this.ajustementPonderation = 0;
            this.totalPonde = 0;
            this.afficherORS = "";
        }
        public Enseignant(int id_, string contract_, string nom_, string prénom_, double heures_mini_,List<CodeConcour> cc)
        {
            this.id = id_;
            this.nom = nom_;
            this.prénom = prénom_;
            this.contract = contract_;
            this.heures_mini = heures_mini_;
            this.ponderationLegt = 0;
            this.ponderationBts = 0;
            this.CodesConcours = cc;
            this.matieres = new List<Matiere>();
            this.afficherCodeConcour = "";
            this.ajustementPonderation = 0;
            this.totalPonde = 0;
            this.afficherORS = "";
        }
        public Enseignant(int id_,string contract_, string nom_, string prénom_)
        {
            this.id = id_;
            this.nom = nom_;
            this.prénom = prénom_;
            this.contract = contract_;
            this.heures_mini = 0;
            this.ponderationLegt = 0;
            this.ponderationBts = 0;
            this.CodesConcours = new List<CodeConcour>();
            this.matieres = new List<Matiere>();
            this.afficherCodeConcour = "";
            this.ajustementPonderation = 0;
            this.totalPonde = 0;
            this.afficherORS = "";
        }
        public void addMatiere(Matiere m)
        {
            this.matieres.Add(m);
        }

        public void addCodeConcour(CodeConcour cc)
        {
            this.CodesConcours.Add(cc);
            cc.enseignants.Add(this);
        }
        public double totalHeure()
        {
            double sum = 0;
            List<Travailler> t0  = AdoTravailler.getOneFromEnseignant(this.id);
            foreach(Travailler t in t0)
            {
                sum += t.volume_horaire;
            }
            
            return sum;
        }

        public override string ToString()
        {
            return this.nom + " " + this.prénom;
        }
    }
}
