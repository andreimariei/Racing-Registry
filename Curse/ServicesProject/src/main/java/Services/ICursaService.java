package Services;

import Domain.Angajat;
import Domain.Cursa;
import Domain.Inscriere;
import Domain.Pilot;

import java.util.List;


public interface ICursaService {
    Angajat login(Angajat employee, ICursaObserver client) throws ProjectException;
    void logout(Angajat employee, ICursaObserver client) throws ProjectException;
    Iterable<Cursa> getCurse() throws ProjectException;
    List<Pilot> getInscrisiEchipa(String nume, int capacitate) throws ProjectException;
    Inscriere addInscriere(int id,String nume, String numeEchipa,int capacitate) throws ProjectException;
    Iterable<Pilot> getPiloti() throws ProjectException;
}
