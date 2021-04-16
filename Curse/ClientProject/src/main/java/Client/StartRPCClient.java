package Client;

import Client.GUI.LoginController;
import Client.GUI.MainPageControl;
import Network.RPCProtocol.CurseServerRPCProxy;
import Services.ICursaService;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.layout.Pane;
import javafx.stage.Stage;

import java.io.IOException;
import java.util.Properties;


public class StartRPCClient extends Application {

    private Stage primaryStage;

    private static int defaultPort=55555;
    private static String defaultServer="localhost";

    public static void main(String[] args) {
        launch(args);
    }
    @Override
    public void start(Stage primaryStage) throws Exception
    {
        System.out.println("In start");
        Properties clientProps=new Properties();
        try{
            clientProps.load(StartRPCClient.class.getResourceAsStream("/curseClient.properties"));
            System.out.println("Curse properties set. ");
            clientProps.list(System.out);

        }catch(IOException e)
        {
            System.out.println("Cannot find curseClient.properties"+e);
            return;
        }
        String serverIP=clientProps.getProperty("curse.server.host",defaultServer);
        int serverPort=defaultPort;
        try{
            serverPort=Integer.parseInt(clientProps.getProperty("curse.server.port"));
        }catch(NumberFormatException e)
        {
            System.err.println("Port invalid"+e.getMessage());
            System.out.println("Using default port:"+defaultPort);
        }
        System.out.println("Using server IP"+serverIP);
        System.out.println("Using server port"+serverPort);

        ICursaService server=new CurseServerRPCProxy(serverIP,serverPort);

        FXMLLoader loaderMainMenu = new FXMLLoader(getClass().getClassLoader().getResource("Views/MainPage.fxml"));
        Parent rootMainMenu = loaderMainMenu.load();
        MainPageControl controllerMainMenu = loaderMainMenu.<MainPageControl>getController();
        controllerMainMenu.setSrv(server);


        /*Stage stageMainMenu = new Stage();
        stageMainMenu.setTitle("Main menu");
        stageMainMenu.setScene(new Scene(rootMainMenu));*/


        FXMLLoader loader=new FXMLLoader();
        loader.setLocation(getClass().getClassLoader().getResource("Views/Login.fxml"));
        Parent root;
        root = loader.load();

        LoginController ctr = loader.<LoginController>getController();

        ctr.setSrv(server,primaryStage);
        ctr.setParent(rootMainMenu);
        ctr.setPageControl(controllerMainMenu);
        primaryStage.setScene(new Scene(root));
        primaryStage.setTitle("Login");
        primaryStage.show();

    }
}
