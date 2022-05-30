using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace txhmontroland.Classes
{
    public class Travailler
    {
        public Enseignant enseignant { get; set; }
        public Matiere matiere { get; set; }
        public Classe Classe { get; set; }
        public double volume_horaire { get; set; }
        public double horaire_referentiel { get; set; }
        public double afficherHLEP { get; set; }
        public double afficherHLegt2 { get; set; }
        public double afficherHLegt1T { get; set; }
        public double afficherHBts { get; set; }
        public string afficherNomMatiere { get; set; }
        public string afficherNomClasse { get; set; }

        public Travailler() { }
        public Travailler(Enseignant enseignant_,Matiere matiere_, Classe classe_,double volume_horaire_,double horaire_referentiel_)
        {
            this.enseignant = enseignant_;
            this.matiere = matiere_;
            this.Classe = classe_;
            this.volume_horaire = volume_horaire_;
            this.afficherHLEP = 0;
            this.afficherHLegt1T = 0;
            this.afficherHLegt2 = 0;
            this.afficherHLegt1T = 0;
            this.horaire_referentiel = horaire_referentiel_;
        }
        public Travailler(Enseignant enseignant_,double volume_horaire_, double horaire_referentiel_)
        {
            this.enseignant = enseignant_;
            this.volume_horaire = volume_horaire_;
            this.Classe = new Classe();
            this.matiere = new Matiere();
            this.afficherHLEP = 0;
            this.afficherHLegt1T = 0;
            this.afficherHLegt2 = 0;
            this.afficherHLegt1T = 0;
            this.horaire_referentiel = horaire_referentiel_;
        }

        public Travailler(Classe classe)
        {
            this.enseignant = new Enseignant();
            this.volume_horaire = 0;
            this.Classe = classe;
            this.matiere = new Matiere();
            this.afficherHLEP = 0;
            this.afficherHLegt1T = 0;
            this.afficherHLegt2 = 0;
            this.afficherHLegt1T = 0;
            this.horaire_referentiel = 0;
        }
        public void addEnseignant(Enseignant e)
        {
            this.enseignant = e;
        }
    }
}
