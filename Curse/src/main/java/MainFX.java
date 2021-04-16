import Controller.LoginController;
import Controller.MainPageControl;
import Repository.*;
import Service.Service;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.layout.Pane;
import javafx.stage.Stage;

import java.io.FileReader;
import java.io.IOException;
import java.util.Properties;


public class MainFX extends Application {
    private Service srv;

    public void setez()
    {
        Properties prop=new Properties();
        try {
            prop.load(new FileReader("bd.config"));
        } catch (IOException e) {
            System.out.println("Cannot find bd.config " + e);
        }
        RepoAngajat repoAngajat = new RepoDBAngajat(prop);
        RepoCursa repoCursa = new RepoDBCursa(prop);
        RepoPilot repoPilot = new RepoDBPilot(prop);
        RepoInscriere repoInscriere=new RepoDBInscriere(prop);
        srv=new Service(repoAngajat,repoCursa,repoPilot,repoInscriere);
    }

    public void start(Stage primaryStage) throws Exception {
        FXMLLoader loaderMainMenu = new FXMLLoader();
        loaderMainMenu.setLocation(getClass().getResource("/Views/MainPage.fxml"));
        Parent rootMainMenu = loaderMainMenu.load();
        MainPageControl controllerMainMenu = loaderMainMenu.getController();

        Stage stageMainMenu = new Stage();
        stageMainMenu.setTitle("Main menu");
        stageMainMenu.setScene(new Scene(rootMainMenu));

        setez();

        FXMLLoader loader=new FXMLLoader();
        loader.setLocation(getClass().getResource("/Views/Login.fxml"));
        Pane root;
        root = loader.load();

        LoginController ctr = loader.getController();

        ctr.setSrv(srv,primaryStage,stageMainMenu);

        primaryStage.setScene(new Scene(root));
        primaryStage.setTitle("Login");
        primaryStage.show();


    }
    public static void main(String[] args) {
        Application.launch();
    }
}
