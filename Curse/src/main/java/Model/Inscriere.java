package Model;

public class Inscriere extends Entity<Long> {
    public long Pilot;
    public long Cursa;

    public long getPilot() {
        return Pilot;
    }

    public void setPilot(long pilot) {
        Pilot = pilot;
    }

    public long getCursa() {
        return Cursa;
    }

    public void setCursa(long cursa) {
        Cursa = cursa;
    }

    public Inscriere(long pilot, long cursa) {
        Pilot = pilot;
        Cursa = cursa;
    }
}
