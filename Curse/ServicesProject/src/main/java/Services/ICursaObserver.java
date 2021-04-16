package Services;

import Domain.Inscriere;
import Domain.Pilot;

public interface ICursaObserver {
    void inscriereAdded(Pilot pilot) throws ProjectException;
}
