
import Network.Utils.AbstractServer;
import Network.Utils.CursaRPCConcurrentServer;

import Repository.*;
import Server.CurseServerImpl;

import Services.ICursaService;


import java.io.IOException;
import java.rmi.ServerException;
import java.util.Properties;

public class StartRpcServer {
    private static int defaultPort = 55555;

    public static void main(String[] args) {
        Properties serverProps = new Properties();
        try{
            serverProps.load(StartRpcServer.class.getResourceAsStream("/flightServer.properties"));
            System.out.println("Server properties set. ");
            serverProps.list(System.out);
        } catch (IOException e) {
            System.err.println("Cannot find /flightServer.properties " + e);
            return;
        }
        RepoAngajat iRepositoryEmployee = new RepoDBAngajat(serverProps);
        RepoInscriere iRepositoryInscriere = new RepoDBInscriere(serverProps);
        RepoCursa iRepositoryCursa = new RepoDBCursa(serverProps);
        RepoPilot iRepositoryPilot = new RepoDBPilot(serverProps);

        ICursaService curseServerImpl = new CurseServerImpl(iRepositoryEmployee, iRepositoryCursa, iRepositoryInscriere, iRepositoryPilot);
        int flightServerPort = defaultPort;
        try{
            flightServerPort = Integer.parseInt(serverProps.getProperty("flight.server.port"));
        }catch (NumberFormatException nef){
            System.err.println("Wrong  Port Number" + nef.getMessage());
            System.err.println("Using default port " + defaultPort);
        }
        System.out.println("Starting server on port: " + flightServerPort);
        AbstractServer server = new CursaRPCConcurrentServer(flightServerPort, curseServerImpl);
        try{
            server.start();
        }catch (ServerException e){
            System.err.println("Error starting the server" + e.getMessage());
        }finally {
            try {
                server.stop();
            }catch(ServerException e){
                System.err.println("Error stopping server " + e.getMessage());
            }
        }
    }
}
