package Repository;

import Domain.Angajat;

public interface RepoAngajat extends CrudRepository<Long, Angajat> {

    Angajat findByUsername(String username);
}
