package Repository;

import Model.Angajat;

public interface RepoAngajat extends CrudRepository<Long, Angajat> {

    Angajat findByUsername(String username);
}
