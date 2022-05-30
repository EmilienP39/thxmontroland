using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace txhmontroland.Classes
{
    public class Avoir
    {
        public Matiere matiere { get; set; }
        public Classe classe { get; set; }
        public double volume_horaire { get; set; }

        public Avoir() { }
        public Avoir(Matiere matiere_,Classe classe_,double volume_horaire_)
        {
            this.matiere = matiere_;
            this.classe = classe_;
            this.volume_horaire = volume_horaire_;
        }
    }
}
