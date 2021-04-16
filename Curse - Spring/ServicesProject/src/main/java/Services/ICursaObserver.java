package Services;

import Domain.Inscriere;
import Domain.Pilot;

import java.rmi.Remote;
import java.rmi.RemoteException;


public interface ICursaObserver extends Remote {
    void inscriereAdded(Pilot pilot) throws ProjectException, RemoteException;
}
