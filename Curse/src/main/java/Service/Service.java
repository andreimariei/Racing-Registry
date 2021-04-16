package Service;

import Model.Angajat;
import Model.Cursa;
import Model.Inscriere;
import Model.Pilot;
import Repository.RepoAngajat;
import Repository.RepoCursa;
import Repository.RepoInscriere;
import Repository.RepoPilot;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

import static java.lang.Long.parseLong;

public class Service {
    private RepoAngajat repoAngajat;
    private RepoCursa repoCursa;
    private RepoPilot repoPilot;
    private RepoInscriere repoInscriere;

    public Service(RepoAngajat repoAngajat, RepoCursa repoCursa, RepoPilot repoPilot,RepoInscriere repoInscriere) {
        this.repoAngajat = repoAngajat;
        this.repoCursa = repoCursa;
        this.repoPilot = repoPilot;
        this.repoInscriere=repoInscriere;
    }

    public List<Pilot> getPiloti(){
        return StreamSupport.stream(repoPilot.getAll().spliterator(),false).collect(Collectors.toList());
    }

    public List<Cursa> getCurse(){
        return StreamSupport.stream(repoCursa.getAll().spliterator(),false).collect(Collectors.toList());
    }

    public Angajat logare(String username, String parola) throws ServiceException {
        Angajat user = repoAngajat.findByUsername(username);

        if(!user.getParola().equals(parola))
            throw new ServiceException("parola gresita");

        return user;
    }
    public List<Pilot> getInscrisiEchipa(String nume, int capacitate)
    {
        Iterable<Inscriere> insc=repoInscriere.getAll();
        Iterable<Pilot> pil=repoPilot.getAll();
        Cursa cursa=repoCursa.findByCapacitate(capacitate);
        List<Inscriere> rez1 = new ArrayList<>();
        List<Pilot> rez2=new ArrayList<>();
        insc.forEach(x->{
            if(x.Cursa==cursa.getId()) {
                rez1.add(x);
            }
        });
        rez1.forEach(x->{
            pil.forEach(y->
            {
                if(x.getPilot()==y.Id && y.getNumeEchipa().equals(nume))
                    rez2.add(y);
            });
        });
    return rez2;
    }

    public void addInscriere(int id,String nume, String numeEchipa, int capacitate)
    {
        Cursa c=repoCursa.findByCapacitate(capacitate);
        Iterable<Pilot> pil=repoPilot.getAll();
        Pilot p=new Pilot(nume,numeEchipa);
        long id1=id;
        p.setId(id1);
        pil.forEach(x->
        {
            if(x.getNumeEchipa().equals(numeEchipa) && x.getNume().equals(nume))
            repoInscriere.add(new Inscriere(x.getId(),c.getId()));

        });

    }

}
