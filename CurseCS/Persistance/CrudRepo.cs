using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace curse.Persistance
{
    public interface CrudRepo<ID, E>
    {
        E FindOne(ID id);
        IEnumerable<E> GetAll();
        void Add(E entity);
        void Delete(ID id);
    }
}
