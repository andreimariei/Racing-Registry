package Model;

public abstract class Entity<T> {
    public T Id;

    public T getId()
    {
       return Id;
    }

    public void setId(T id){
        this.Id=id;
    }
}
