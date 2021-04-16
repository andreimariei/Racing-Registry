package Controller;

import Model.Angajat;
import Service.Service;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.TextField;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.Pane;
import javafx.stage.Stage;


public class LoginController {
    @FXML
    AnchorPane anchorPane;

    @FXML
    TextField username;

    @FXML
    TextField password;

    private Service srv;
    private Stage MainPageStage;
    private Stage LogInStage;

    public void logare(MouseEvent mouseEvent) {

        String user = username.getText();
        String parola = password.getText();
        if (user.equals("") || parola.equals("")) {
            Alert alert = new Alert(Alert.AlertType.INFORMATION);
            alert.setTitle("Eroare");
            alert.setHeaderText(null);
            alert.setContentText("Trebuie sa introduceti username si parola!");
        }
        Angajat angajat = null;
        try {
            angajat = srv.logare(user, parola);
            System.out.println(angajat);
        } catch (Exception e) {
            throw e;
        }


        try {

            FXMLLoader loader = new FXMLLoader();
            loader.setLocation(getClass().getResource("/Views/MainPage.fxml"));
            Pane root;
            root = loader.load();

            MainPageControl ctr = loader.getController();

            ctr.setSrv(angajat,srv);
            Stage primaryStage =new Stage();
            primaryStage.setScene(new Scene(root));
            primaryStage.setTitle("Curse");
            primaryStage.show();



        }
        catch(Exception e)
        {
            e.printStackTrace();
        }


    }

    public void setSrv(Service srv,Stage MainPageStage,Stage LoginStage) {
        this.srv=srv;
        this.MainPageStage=MainPageStage;
        this.LogInStage=LoginStage;
    }
}
