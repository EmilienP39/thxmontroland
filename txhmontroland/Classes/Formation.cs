using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace txhmontroland.Classes
{
    public class Formation
    {
        public int id { get; set; }
        public string name { get; set; }
        public string sigle { get; set; }
        public int afficherNombreeleve { get; set; }
        public int afficherNombreDivision { get; set; }
        public List<Classe> Classes { get; set; }

        public Formation() { }
        public Formation(int id_,string nom_, string sigle_) 
        { 
            this.id = id_;
            this.name = nom_;
            this.sigle = sigle_;
            this.Classes = new List<Classe>();
            this.afficherNombreDivision = 0;
            this.afficherNombreeleve = 0;
        }
        public override string ToString()
        {
            return this.sigle;
        }
    }
}
