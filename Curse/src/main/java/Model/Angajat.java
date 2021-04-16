package Model;

public class Angajat extends Entity<Long> {
    public String username;

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public Angajat(String username, String password) {
        this.username = username;
        this.password = password;
    }

    public String password;


    public Object getParola() {
        return this.password;
    }
}
