package Network.DTO;

import Domain.Angajat;

public class DTOUtils {
    public static Angajat getFromDTO(AngajatDTO aDTO)
    {
        String username=aDTO.getUsername();
        String password=aDTO.getPassword();
        return new Angajat(username,password);

    }
    public static AngajatDTO getAngajatDTO(Angajat angajat)
    {
        String username=angajat.getUsername();
        String password=angajat.getPassword();
        return new AngajatDTO(username,password);
    }

}
