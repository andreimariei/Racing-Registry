using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace curse.Networking.dto
{   
    [Serializable]
    public class CursaDTO
    {
        public int capacitate;
        public CursaDTO(int capac)
        {
            this.capacitate = capac;
        }
        public virtual int Capacitate
        {
            get
            {
                return capacitate;
            }
            set
            {
                this.capacitate = value;
            }
        }
    }
}
