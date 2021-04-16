
using curse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace curse.Persistance
{
    interface IRepoCursa : CrudRepo<long,Cursa>
    {
        Cursa FindByCapacitate(int capacitate);
    }
}
