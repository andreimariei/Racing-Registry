package Network.DTO;

import java.io.Serializable;

public class AddInscriereDTO implements Serializable {
    public String nume;
    public String numeEchipa;

    public AddInscriereDTO(String nume, String numeEchipa, int capacitate, int id) {
        this.id = id;
        this.nume = nume;
        this.numeEchipa = numeEchipa;
        this.capacitate = capacitate;

    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }


    public int capacitate;
    public int id;




    public String getNume() {
        return nume;
    }

    public int getCapacitate() {
        return capacitate;
    }

    public String getNumeEchipa() {
        return numeEchipa;
    }

    public void setNumeEchipa(String numeEchipa) {
        this.numeEchipa = numeEchipa;
    }

    public void setNume(String nume) {
        this.nume = nume;
    }

    public void setCapacitate(int capacitate) {
        this.capacitate = capacitate;
    }
}
