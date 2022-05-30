using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using txhmontroland.ADO;

namespace txhmontroland.Classes
{
    public class CodeConcour
    {
        public int id { get; set; }
        public string codeconcour { get; set; }
        public string nom { get; set; }
        public List<Enseignant> enseignants { get; set;}
        public List<Matiere> matieres { get;set;}
        public string afficherNomEnseignants { get; set; }
        public string afficherContractEnseignants { get; set; }
        public double afficherPondLegtEnseignants { get; set; }
        public double afficherPondBtsEnseignants { get; set; }
        public double ajustementPonde { get; set; }
        public double afficherTotalHEnseignants { get; set; }
        public double afficherTotalponde { get; set; }
        public double afficherHeuresLep { get; set; }
        public double afficherHeuresLegt1T { get; set; }
        public double afficherHeuresLegt2ND { get; set; }
        public double afficherHeuresBts { get; set; }
        public double AS { get; set; }
        public int afficherEnseignantId { get; set; }
        public double afficherHSA { get; set; }
        public double? totalVH {get;set;}
        public double? totalHSA { get; set; }
        public string couleur { get; set; }
        public double complementPoste { get; set; }

        public CodeConcour() { }
        public CodeConcour(int id_,string codeconcour_,string nom_,string couleur_)
        {
            this.id = id_;
            this.codeconcour = codeconcour_;
            this.nom = nom_;
            this.enseignants = new List<Enseignant>();
            this.afficherPondBtsEnseignants = 0;
            this.afficherNomEnseignants = "";
            this.afficherPondLegtEnseignants = 0;
            this.afficherContractEnseignants = "";
            this.ajustementPonde = 0;
            this.afficherTotalHEnseignants = 0;
            this.afficherTotalponde = 0;
            this.afficherHeuresLep = 0;
            this.afficherEnseignantId = 0;
            this.afficherHSA = 0;
            this.totalHSA = 0;
            this.totalVH = 0;
            this.couleur = couleur_;
            this.matieres = new List<Matiere>();
            this.AS = 0;
        }
        public CodeConcour(string codeconcour_, string nom_)
        {
            this.id = 0;
            this.codeconcour = codeconcour_;
            this.nom = nom_;
            this.enseignants = new List<Enseignant>();
            this.afficherPondBtsEnseignants = 0;
            this.afficherNomEnseignants = "";
            this.afficherPondLegtEnseignants = 0;
            this.afficherContractEnseignants = "";
            this.ajustementPonde = 0;
            this.afficherTotalHEnseignants = 0;
            this.afficherTotalponde = 0;
            this.afficherHeuresLep = 0;
            this.afficherEnseignantId = 0;
            this.afficherHSA = 0;
            this.totalHSA = 0;
            this.totalVH = 0;
            this.couleur = "";
            this.matieres = new List<Matiere>();
            this.AS = 0;
        }
        public void addEnseignant(Enseignant e)
        {
            this.enseignants.Add(e);
            e.CodesConcours.Add(this);
        }

        public void addMatiere(Matiere m)
        {
            this.matieres.Add(m);
        }
        public void getMatierePossible()
        {
            List<Matiere> m = AdoConcours.GetMatierePossibles(this);
            foreach(Matiere matiere in m)
            {
                this.matieres.Add(matiere);
            }
        }
        public override string ToString()
        {
            return codeconcour + " " + nom;
        }
    }
}
