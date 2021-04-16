using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace curse.Model

{
    [Serializable]
    public class Cursa : Entity<long>
    {
        public int capacitate { get; private set; }

        public Cursa(int capacitate)
        {
            Id = 0;
            this.capacitate = capacitate;
        }
        public Cursa(long id, int capacitate)
        {
            Id = id;
            this.capacitate = capacitate;
        }
    }
}
