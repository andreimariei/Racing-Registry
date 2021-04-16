package Domain;

import java.io.Serializable;

public class Cursa extends Entity<Long> implements Serializable {
    public int Capacitate;


    public void setCapacitate(int capacitate) {
        Capacitate = capacitate;
    }

    public int getCapacitate() {
        return Capacitate;
    }

    public Cursa(int capacitate) {
        Capacitate = capacitate;
    }
}
