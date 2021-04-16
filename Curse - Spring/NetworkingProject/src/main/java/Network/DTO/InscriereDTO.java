package Network.DTO;


import java.io.Serializable;

public class InscriereDTO implements Serializable {
    public String nume;
    public int capacitate;



    public String getNume() {
        return nume;
    }

    public int getCapacitate() {
        return capacitate;
    }

    public InscriereDTO(String nume, int capacitate) {
        this.nume = nume;
        this.capacitate = capacitate;
    }

    public void setNume(String nume) {
        this.nume = nume;
    }

    public void setCapacitate(int capacitate) {
        this.capacitate = capacitate;
    }
}
