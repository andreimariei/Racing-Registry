package Network.RPCProtocol;

import Domain.Angajat;
import Domain.Cursa;
import Domain.Inscriere;
import Domain.Pilot;
import Network.DTO.AddInscriereDTO;
import Network.DTO.InscriereDTO;
import Services.ICursaObserver;
import Services.ICursaService;
import Services.ProjectException;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.rmi.RemoteException;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

public class CurseServerRPCProxy implements ICursaService {
    private String host;
    private int port;

    private ICursaObserver client;
    private BlockingQueue<Network.RPCProtocol.Response> responses;

    private ObjectInputStream input;
    private ObjectOutputStream output;
    private Socket connection;

    public List<ICursaObserver> observers;

    private volatile boolean finished;

    public CurseServerRPCProxy(String host, int port) {
        this.host = host;
        this.port = port;
        responses = new LinkedBlockingQueue<Network.RPCProtocol.Response>();
        observers = new ArrayList<>();
        try {
            initializeConnection();
        } catch (ProjectException e) {
            e.printStackTrace();
        }
    }

    @Override
    public Angajat login(Angajat angajat, ICursaObserver client) throws ProjectException {

        Request request = new Request.Builder().type(Network.RPCProtocol.RequestType.LOGIN).data(angajat).build();
        sendRequest(request);
        Network.RPCProtocol.Response response = readResponse();
        if (response.type() == ResponseType.OK){
            this.client = client;
            System.out.println("OK");
            Angajat angajat1 = (Angajat)response.data();
            System.out.println(angajat1);
            return new Angajat(angajat1.getUsername(), angajat1.getPassword());
        }
        if (response.type() == ResponseType.ERROR){
            String err = response.data().toString();
            closeConnection();
            throw new ProjectException(err);
        }
        throw new ProjectException();
    }

    @Override
    public void logout(Angajat angajat, ICursaObserver client) throws ProjectException {
        Request request = new Request.Builder().type(Network.RPCProtocol.RequestType.LOGOUT).data(angajat).build();
        sendRequest(request);
        Network.RPCProtocol.Response response = readResponse();
        closeConnection();

        if (response.type() == ResponseType.ERROR){
            String err = response.data().toString();
            throw new ProjectException(err);
        }
    }

    @Override
    public Iterable<Cursa> getCurse() throws ProjectException {
        Request request=new Request.Builder().type(RequestType.GET_ALL_CURSE).data(null).build();
        sendRequest(request);
        Response response=readResponse();
        if(response.type()==ResponseType.ERROR)
        {
            String err=response.data().toString();
            throw new ProjectException(err);
        }
        return (List<Cursa>) response.data();
    }

    @Override
    public List<Pilot> getInscrisiEchipa(String nume, int capacitate) throws ProjectException {
        InscriereDTO dto=new InscriereDTO(nume, capacitate);
        Request request=new Request.Builder().type(RequestType.GET_PILOTI_INSCRISI).data(dto).build();
        sendRequest(request);
        Response response=readResponse();
        if(response.type()==ResponseType.ERROR)
        {
            String err=response.data().toString();
            throw new ProjectException(err);
        }
        return (List<Pilot>) response.data();
    }

    @Override
    public Inscriere addInscriere(int id,String nume, String numeEchipa, int capacitate) throws ProjectException {
        AddInscriereDTO dto=new AddInscriereDTO(nume,numeEchipa,capacitate,id);
        Request request=new Request.Builder().type(RequestType.ADD_INSCRIERE).data(dto).build();
        sendRequest(request);
        Response response=readResponse();
        if(response.type()==ResponseType.ERROR)
        {
            String err=response.data().toString();
            throw new ProjectException(err);
        }
        return null ;
    }

    @Override
    public Iterable<Pilot> getPiloti() throws ProjectException {
        Request request=new Request.Builder().type(RequestType.GET_ALL_PILOTI).data(null).build();
        sendRequest(request);
        Response response=readResponse();
        if(response.type()==ResponseType.ERROR)
        {
            String err=response.data().toString();
            throw new ProjectException(err);
        }
        return (List<Pilot>) response.data();
    }


    private void initializeConnection() throws ProjectException {
        try {
            connection = new Socket(host, port);
            System.out.println(host);
            System.out.println(port);
            output = new ObjectOutputStream(connection.getOutputStream());
            output.flush();
            input = new ObjectInputStream(connection.getInputStream());
            finished = false;
            startReader();
        } catch (IOException e) {
            e.printStackTrace();
        }

    }

    private void closeConnection() {
        finished = true;
        try {
            input.close();
            output.close();
            connection.close();
            client = null;
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void startReader(){
        Thread tw=new Thread(new ReaderThread());
        tw.start();
    }

    private boolean isUpdate(Network.RPCProtocol.Response response){
        return response.type()== ResponseType.INSCRIERE_ADDED;
    }

    private void handleUpdate(Network.RPCProtocol.Response response){
        Pilot pilot = (Pilot) response.data();
        try{
            client.inscriereAdded(pilot);
        }catch (ProjectException| RemoteException e){
            e.printStackTrace();
        }
    }

    private void sendRequest(Request request)throws ProjectException {
        try {
            output.writeObject(request);
            output.flush();
        } catch (IOException e) {
            throw new ProjectException("Error sending object "+e);
        }
    }

    private Response readResponse() throws ProjectException {
        Response response = null;
        try{
            response = responses.take();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        return response;
    }

    private class ReaderThread implements Runnable{
        public void run() {
            while( !finished ){
                try {
                    Object response = input.readObject();
                    System.out.println("response received " + response);
                    if (isUpdate((Network.RPCProtocol.Response)response)){
                        handleUpdate((Network.RPCProtocol.Response)response);
                    }else{
                        try {
                            responses.put((Network.RPCProtocol.Response)response);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }
                } catch (IOException | ClassNotFoundException e) {
                    System.out.println("Reading error " + e);
                }
            }
        }
    }
}
