package Domain;

import java.io.Serializable;

public abstract class Entity<T> implements Serializable {
    public T Id;

    public T getId()
    {
       return Id;
    }

    public void setId(T id){
        this.Id=id;
    }
}
