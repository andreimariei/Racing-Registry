package Repository;

import Domain.Pilot;

public interface CrudRepository<ID, E> {
    E findOne(ID id) throws IllegalArgumentException;
    Iterable<E> getAll();
    void add(E entity);
    void delete(E entity);
}
