 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using txhmontroland.Classes;
using txhmontroland.ADO;
using txhmontroland.Pages;

namespace txhmontroland.Classes
{
    public class Matiere
    {
        public int id { get; set; }
        public string nom_matiere { get; set; }
        public string sigle_matiere { get; set; }
        public double afficherTotalHeure { get; set; }
        public List<CodeConcour> codeConcours { get; set; }
        public List<Enseignant> enseignants { get; set;}

        public Matiere() { }
        public Matiere(int id_, string nom_, string sigle_) 
        {
            this.id = id_;
            this.nom_matiere = nom_;    
            this.sigle_matiere = sigle_;
            this.afficherTotalHeure = 0.0;
            this.codeConcours = new List<CodeConcour>(); 
            this.enseignants = new List<Enseignant>();
        }
        public Matiere(string nom, string sigle, List<CodeConcour> codeConcours)
        {
            this.id = 0;
            this.nom_matiere = nom;
            this.sigle_matiere = sigle;
            this.codeConcours = codeConcours;
            this.afficherTotalHeure = 0.0;
            this.enseignants = new List<Enseignant>();
        }
        public Matiere(int id_,string nom, string sigle, List<CodeConcour> codeConcours)
        {
            this.id = id_;
            this.nom_matiere = nom;
            this.sigle_matiere = sigle;
            this.afficherTotalHeure = 0.0;
            this.codeConcours = codeConcours;
            this.enseignants = new List<Enseignant>();
        }
        public Matiere(int id_, string nom, string sigle, List<Enseignant> enseignants)
        {
            this.id = id_;
            this.nom_matiere = nom;
            this.sigle_matiere = sigle;
            this.afficherTotalHeure = 0.0;
            this.codeConcours = new List<CodeConcour>();
            this.enseignants = enseignants;
        }
        public void AddCodeConcours(CodeConcour c)
        {
            this.codeConcours.Add(c);
        }
        public void AddEnseignant(Enseignant e)
        {
            this.enseignants.Add(e);
        }

        public override string ToString()
        {
            return nom_matiere;
        }
    }
}
