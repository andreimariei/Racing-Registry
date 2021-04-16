using curse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace curse.Persistance
{
    public interface IRepoPilot : CrudRepo<long,Pilot>
    {
        IEnumerable<Pilot> FindByNume(string nume);
        IEnumerable<Pilot> FindByNumeEchipa(string numeEchipa);
    }
}
