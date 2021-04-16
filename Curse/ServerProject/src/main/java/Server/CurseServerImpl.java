package Server;

import Domain.Angajat;
import Domain.Cursa;
import Domain.Inscriere;
import Domain.Pilot;

import Repository.RepoAngajat;
import Repository.RepoCursa;
import Repository.RepoInscriere;
import Repository.RepoPilot;
import Services.ICursaObserver;

import Services.ICursaService;
import Services.ProjectException;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public class CurseServerImpl implements ICursaService {
    private RepoAngajat iRepositoryEmployee;
    private RepoCursa iRepositoryCursa;
    private RepoInscriere iRepositoryInscriere;
    private RepoPilot iRepositoryPilot;
    private Map<String, ICursaObserver> loggedEmployees;
    private final int defaultThreadsNo = 5;

    public CurseServerImpl(RepoAngajat iRepositoryEmployee, RepoCursa iRepositoryCursa, RepoInscriere iRepositoryInscriere, RepoPilot iRepositoryPilot) {
        this.iRepositoryEmployee = iRepositoryEmployee;
        this.iRepositoryCursa = iRepositoryCursa;
        this.iRepositoryInscriere = iRepositoryInscriere;
        this.iRepositoryPilot = iRepositoryPilot;
        loggedEmployees = new ConcurrentHashMap<>();
    }

    @Override
    public synchronized Angajat login(Angajat employee, ICursaObserver client) throws ProjectException {
        Angajat employee1 = iRepositoryEmployee.findByUsername(employee.getUsername());
        System.out.println(employee1);
        if(employee1 != null){
            if(loggedEmployees.get(employee.getUsername()) != null) {
                throw new ProjectException("Employee already logged in.");
            }
            if(!employee1.getPassword().equals(employee.getPassword())){
                throw new ProjectException("Wrong password.");
            }
            loggedEmployees.put(employee1.getUsername(), client);
            return employee1;
        }else{
            throw new ProjectException("Authentication failed.");
        }
    }

    @Override
    public synchronized void logout(Angajat employee, ICursaObserver client) throws ProjectException {
        if(loggedEmployees.get(employee.getUsername()) != null){
            loggedEmployees.remove(employee.getUsername());
        }
    }

    @Override
    public synchronized Iterable<Cursa> getCurse() throws ProjectException {
        return (List<Cursa>) iRepositoryCursa.getAll();
    }
    @Override
    public synchronized Iterable<Pilot> getPiloti() throws ProjectException
    {
        return (List<Pilot>) iRepositoryPilot.getAll();
    }


    public List<Pilot> getInscrisiEchipa(String nume, int capacitate)
    {
        Iterable<Inscriere> insc= iRepositoryInscriere.getAll();
        Iterable<Pilot> pil= iRepositoryPilot.getAll();
        Cursa cursa=iRepositoryCursa.findByCapacitate(capacitate);
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

    public Inscriere addInscriere(int id,String nume, String numeEchipa, int capacitate)
    {
        Cursa c=iRepositoryCursa.findByCapacitate(capacitate);
        Inscriere inscriere=new Inscriere(0,0);
        long id1=id;
        if(iRepositoryPilot.findOne(id1)==null)
        {
            iRepositoryPilot.add(new Pilot(id1,nume,numeEchipa));
        }
        Iterable<Pilot> pil=iRepositoryPilot.getAll();
        pil.forEach(x->
        {
            if(x.getNumeEchipa().equals(numeEchipa))
            iRepositoryInscriere.add(new Inscriere(x.getId(),c.getId()));

        });
        inscriere.setPilot(id1);
        inscriere.setCursa(c.getId());
        if(inscriere.getPilot()==0) {
            return null;
        }
        notifyClients(iRepositoryPilot.findOne(id1));
        return inscriere;
    }
    private void notifyClients(Pilot pilot){
        ExecutorService executor = Executors.newFixedThreadPool(defaultThreadsNo);
        for(String clientLogged : loggedEmployees.keySet()){
            ICursaObserver client = loggedEmployees.get(clientLogged);
            if(client != null) {
                executor.execute(() -> {
                    try {
                        System.out.println("Notifying [" + clientLogged + "]");
                        synchronized (client) {
                            client.inscriereAdded(pilot);
                        }
                        System.out.println("Notified");
                    } catch (ProjectException e) {
                        System.err.println("Error notifying client " + e);
                    }
                });
            }
        }
        executor.shutdown();
    }
}
