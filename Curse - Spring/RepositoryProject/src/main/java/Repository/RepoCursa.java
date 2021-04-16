package Repository;

import Domain.Cursa;

public interface RepoCursa extends CrudRepository<Long,Cursa> {
    Cursa findByCapacitate(int capacitate);

}
