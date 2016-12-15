using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoneyMoonDB.Models
{
    public class Afspraak
    {
        public int AfspraakId { get; set; }
        public string Naam { get; set; }
        public DateTime TrouwDatum { get; set; }
        public int Telefoonnummer { get; set; }
        public string Email { get; set; }
        public bool Nieuwsbrief { get; set; }
        public DateTime Tijd { get; set; }


    }
}
