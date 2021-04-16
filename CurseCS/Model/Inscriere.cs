using System;
using System.Collections.Generic;
using System.Text;

namespace curse.Model
{
    [Serializable]
    public class Inscriere : Entity<long>
    {
        public long Pilot { get;  set; }
        public long Cursa { get;  set; }
        public Inscriere(long pilot, long cursa)
        {
            Id = 0;
            Pilot = pilot;
            Cursa = cursa;
        }
        public Inscriere(long id, long pilot, long cursa)
        {
            Id = id;
            Pilot = pilot;
            Cursa = cursa;
        }

        public long getPilot()
        {
            return Pilot;
        }
    }
}
