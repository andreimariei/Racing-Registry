package Domain;

import java.io.Serializable;

public class Angajat extends Entity<Long> implements Serializable {
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

    @Override
    public String toString() {
        return "Angajat{" +
                "username='" + username + '\'' +
                ", password='" + password + '\'' +
                '}';
    }
}
