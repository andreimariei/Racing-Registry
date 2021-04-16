package Domain;


import java.io.Serializable;

public class Pilot extends Entity<Long> implements Serializable {
    public String Nume;
    public String NumeEchipa;

    public String getNume() {
        return Nume;
    }

    public void setNume(String nume) {
        this.Nume = nume;
    }

    public String getNumeEchipa() {
        return NumeEchipa;
    }

    public void setNumeEchipa(String numeEchipa) {
        this.NumeEchipa = numeEchipa;
    }

    public Pilot(Long id,String nume, String numeEchipa) {
        Id=id;
        this.Nume = nume;
        this.NumeEchipa = numeEchipa;
    }

    public Pilot(String nume, String numeEchipa) {
        Id= Long.valueOf(-1);
        this.Nume = nume;
        this.NumeEchipa = numeEchipa;
    }

}
