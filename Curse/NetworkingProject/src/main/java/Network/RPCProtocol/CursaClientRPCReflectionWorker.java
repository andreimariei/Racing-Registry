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
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.net.Socket;
import java.util.List;

public class CursaClientRPCReflectionWorker implements ICursaObserver, Runnable {
    private ICursaService server;
    private Socket connection;
    private ObjectInputStream input;
    private ObjectOutputStream output;
    private volatile boolean connected;

    public CursaClientRPCReflectionWorker(ICursaService server, Socket connection) {
        this.server = server;
        this.connection = connection;

        try{
            output = new ObjectOutputStream(connection.getOutputStream());
            output.flush();
            input = new ObjectInputStream(connection.getInputStream());
            connected = true;
        }catch (IOException e){
            e.printStackTrace();
        }
    }

    @Override
    public void run() {
        while(connected){
            try {
                Object request = input.readObject();
                Response response = handleRequest((Request)request);
                if (response != null){
                    sendResponse(response);
                }
            } catch (IOException | ClassNotFoundException e) {
                e.printStackTrace();
            }
            try {
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
        try {
            input.close();
            output.close();
            connection.close();
        } catch (IOException e) {
            System.out.println("Error "+e);
        }
    }

    private static Response okResponse = new Response.Builder().type(ResponseType.OK).build();

    private Response handleRequest(Request request){
        Response response = null;
        String handlerName = "handle" + (request).type();
        System.out.println("HandlerName " + handlerName);
        try {
            Method method = this.getClass().getDeclaredMethod(handlerName, Request.class);
            response = (Response)method.invoke(this, request);
            System.out.println("Method " + handlerName + " invoked");
        } catch (NoSuchMethodException | InvocationTargetException | IllegalAccessException e) {
            e.printStackTrace();
        }
        return response;
    }

    private Response handleLOGIN(Request request){
        System.out.println("Login request ..." + request.type());
        Angajat employee1 = (Angajat)request.data();
        try {
            Angajat employee = server.login(employee1, this);
            return new Response.Builder().type(ResponseType.OK).data(employee).build();
        } catch (ProjectException e) {
            connected = false;
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }

    private Response handleLOGOUT(Request request){
        Angajat employee = (Angajat) request.data();
        try{
            server.logout(employee, this);
            connected = false;
            return  okResponse;
        }catch (ProjectException e){
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }

    private Response handleGET_ALL_CURSE(Request request){
        try{
            List<Cursa> cursaList = (List<Cursa>) server.getCurse();
            return new Response.Builder().type(ResponseType.GET_CURSE).data(cursaList).build();

        }catch (ProjectException e){
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }
    private Response handleGET_ALL_PILOTI(Request request){
        try{
            List<Pilot> cursaList = (List<Pilot>) server.getPiloti();
            return new Response.Builder().type(ResponseType.GET_PILOTI).data(cursaList).build();

        }catch (ProjectException e){
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }
    private Response handleGET_PILOTI_INSCRISI(Request request){
        try{
            InscriereDTO dto= (InscriereDTO) request.data();
            List<Pilot> cursaList = (List<Pilot>) server.getInscrisiEchipa(dto.getNume(),dto.getCapacitate());
            return new Response.Builder().type(ResponseType.GET_PILOTI_INSCRISI).data(cursaList).build();
        }catch (ProjectException e){
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }

    private Response handleADD_INSCRIERE(Request request){
        try{
            AddInscriereDTO dto=(AddInscriereDTO) request.data();
            Inscriere inscriere=server.addInscriere((int) dto.getId(),dto.getNume(),dto.getNumeEchipa(),dto.getCapacitate());
            return new Response.Builder().type(ResponseType.ADD_INSCRIERE).data(inscriere).build();
        }catch (ProjectException e){
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }

    private void sendResponse(Response response) throws IOException{
        System.out.println("Sending response " + response);
        synchronized (output) {
            output.writeObject(response);
            output.flush();
        }
    }

    @Override
    public void inscriereAdded(Pilot pilot) throws ProjectException {
        Response response = new Response.Builder().type(ResponseType.INSCRIERE_ADDED).data(pilot).build();
        try{
            sendResponse(response);
        }catch (IOException e){
            e.printStackTrace();
        }
    }
}