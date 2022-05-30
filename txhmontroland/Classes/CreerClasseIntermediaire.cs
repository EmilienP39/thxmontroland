using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace txhmontroland.Classes
{
    public class CreerClasseIntermediaire
    {
        public List<Matiere> matiereList { get; set; }
        public Enseignant enseignant { get; set; }
        public List<Enseignant> enseignantList { get;set; }
        public double horaire_referentiel { get; set; }
        public double volume_horaire { get; set; }
        public Boolean is_pp { get; set; }

        public Matiere matiere { get; set; }
        public CreerClasseIntermediaire() { }
        public CreerClasseIntermediaire(Matiere m, Enseignant e, double h_ref, double vh)
        {
            this.matiere = m;
            this.matiereList = new List<Matiere>();
            this.enseignant = e;
            this.enseignantList = new List<Enseignant>();
            this.horaire_referentiel = h_ref;
            this.volume_horaire = vh;
            this.is_pp = false;
        }
    }
}
