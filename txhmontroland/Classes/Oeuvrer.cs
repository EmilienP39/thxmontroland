using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace txhmontroland.Classes
{
    public class Oeuvrer
    {
        public Enseignant enseignant { get; set; } 
        public Classe classe { get; set; } 
        public Boolean is_pp { get; set; }
        public Oeuvrer() { }
        public Oeuvrer(Enseignant enseignant_, Classe classe_, Boolean is_pp_)
        {
            this.enseignant = enseignant_;
            this.classe = classe_;  
            this.is_pp = is_pp_;
        }
    }
}
