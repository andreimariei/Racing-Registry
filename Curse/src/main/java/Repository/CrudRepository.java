package Repository;

import Model.Cursa;

public interface CrudRepository<ID, E> {
    E findOne(ID id);
    Iterable<E> getAll();
    void add(E entity);
    void delete(E entity);
}
