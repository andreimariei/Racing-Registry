package Network.Utils;

import Network.RPCProtocol.CursaClientRPCReflectionWorker;
import Services.ICursaService;

import java.net.Socket;

public class CursaRPCConcurrentServer extends AbstractConcurrentServer {
    private ICursaService cursaService;

    public CursaRPCConcurrentServer(int port, ICursaService cursaService) {
        super(port);
        this.cursaService=cursaService;
        System.out.println("Flight - FlightRpcConcurrentServer");
    }

    @Override
    protected Thread createWorker(Socket client) {
        CursaClientRPCReflectionWorker worker = new CursaClientRPCReflectionWorker(cursaService, client);
        Thread tw = new Thread(worker);
        return tw;
    }
}