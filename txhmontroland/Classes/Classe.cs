using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace txhmontroland.Classes
{
    public class Classe
    {
        public int id { get; set; }
        public int nombre_eleve { get; set; }
        public string nom_classe { get; set; }
        public string type_formation { get; set; } //LEGT, ES etc...
        public string niveau_classe { get; set; } //2nd, 1ere etc...
        public int valeur_niveau { get; set; }
        public double division { get;set; }
        public double afficherTotalHeure { get; set; }
        public List<Matiere> matieres { get; set; }


        public Classe() { }
        public Classe(int id_, int nbEleve_, string nom_,string typeFormation,string niveau_,int valeurNiveau_, List<Matiere> matieres)
        {
            this.id = id_;
            this.nom_classe = nom_;
            this.nombre_eleve = nbEleve_;
            this.type_formation = typeFormation;
            this.niveau_classe = niveau_;
            this.valeur_niveau = valeurNiveau_;
            this.matieres = matieres;
            this.division = 0;
            this.afficherTotalHeure = 0;
        }
        public Classe(int id_, int nbEleve_, string nom_, string typeFormation, string niveau_, int valeurNiveau_,double division_)
        {
            this.id = id_;
            this.nom_classe = nom_;
            this.nombre_eleve = nbEleve_;
            this.type_formation = typeFormation;
            this.niveau_classe = niveau_;
            this.valeur_niveau = valeurNiveau_;
            this.matieres = new List<Matiere>();
            this.division = division_;
            this.afficherTotalHeure = 0;
        }
        public Classe(int nbEleve_, string nom_, string typeFormation, string niveau_, int valeurNiveau_)
        {
            this.id = 0;
            this.nom_classe = nom_;
            this.nombre_eleve = nbEleve_;
            this.type_formation = typeFormation;
            this.niveau_classe = niveau_;
            this.valeur_niveau = valeurNiveau_;
            this.matieres = new List<Matiere>();
            this.division = 0;
            this.afficherTotalHeure = 0;
        }

        public Classe(int nbEleve_, string nom_, double division_)
        {
            this.id = 0;
            this.nom_classe = nom_;
            this.nombre_eleve = nbEleve_;
            this.type_formation = "";
            this.niveau_classe = "";
            this.valeur_niveau = 0;
            this.matieres = new List<Matiere>();
            this.division = division_;
            this.afficherTotalHeure = 0;
        }
        public Classe(int id_,int nbEleve_, string nom_)
        {
            this.id = id_;
            this.nom_classe = nom_;
            this.nombre_eleve = nbEleve_;
            this.type_formation = "";
            this.niveau_classe = "";
            this.valeur_niveau = 0;
            this.matieres = new List<Matiere>();
            this.division = 0;
            this.afficherTotalHeure = 0;
        }

        public void addMatiere(Matiere m)
        {
            this.matieres.Add(m);
        }

        public override string ToString()
        {
            return this.nom_classe;
        }
    }
}
