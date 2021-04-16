package Repository;

import Model.Cursa;

public interface RepoCursa extends CrudRepository<Long,Cursa> {
    Cursa findByCapacitate(int capacitate);

}
