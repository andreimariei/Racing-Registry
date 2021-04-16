using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace curse.Networking.dto
{
    [Serializable]
    public class PilotDTO
    {
        public string nume;
        public string numeEchipa;
        public string username { get; set; }
        public PilotDTO() { }
        public PilotDTO(string n, string ne,string user)
        {
            this.username = user;
            this.nume = n;
            this.numeEchipa = ne;
        }
        public virtual string Nume
        {
            get
            {
                return nume;
            }
            set
            {
                this.nume = value;
            }
        }


        public virtual string NumeEchipa
        {
            get
            {
                return numeEchipa;
            }
            set
            {
                this.numeEchipa = value;
            }
        }
    }
}
