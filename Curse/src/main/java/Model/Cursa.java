package Model;

import java.util.ArrayList;
import java.util.List;

public class Cursa extends Entity<Long> {
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
