using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace curse.Model
{
    [Serializable]
    public class Pilot : Entity<long>
    {
        public string nume { get; private set; }
        public string numeEchipa { get; private set; }

        public Pilot(string nume, string numeEchipa)
        {
            Id = 0;
            this.nume = nume;
            this.numeEchipa = numeEchipa;
        }
        public Pilot(long id, string nume, string numeEchipa)
        {
            Id = id;
            this.nume = nume;
            this.numeEchipa = numeEchipa;
        }
    }
}
