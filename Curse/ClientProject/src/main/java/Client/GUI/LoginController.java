package Client.GUI;

import Domain.Angajat;
import Services.ICursaObserver;
import Services.ICursaService;
import Services.ProjectException;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Node;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.TextField;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.Pane;
import javafx.stage.Stage;
import javafx.stage.WindowEvent;

import java.awt.event.ActionEvent;


public class LoginController {
    @FXML
    AnchorPane anchorPane;

    @FXML
    TextField username;

    @FXML
    TextField password;


    private ICursaService srv;
    private Stage MainPageStage;
    private Stage LogInStage;
    private MainPageControl mainPageControl;
    private Parent mainPageParent;

    public void setParent(Parent parent)
    {
        this.mainPageParent=parent;
    }
    public void setPageControl(MainPageControl control)
    {
        this.mainPageControl=control;
    }

    public void logare(MouseEvent mouseEvent) throws ProjectException {

        String user = username.getText();
        String parola = password.getText();

        if (user.equals("") || parola.equals("")) {
            Alert alert = new Alert(Alert.AlertType.INFORMATION);
            alert.setTitle("Eroare");
            alert.setHeaderText(null);
            alert.setContentText("Trebuie sa introduceti username si parola!");
        }
        Angajat angajat = new Angajat(user,parola);
        System.out.println(angajat.toString());
        try {

            Angajat angajat1=srv.login(angajat, (ICursaObserver) mainPageControl);
            System.out.println(angajat1.toString());
            openStage(angajat1,mouseEvent);
            System.out.println(angajat);
        } catch (ProjectException e) {
            throw e;
        }





    }
    public void openStage(Angajat angajat, MouseEvent actionEvent)
    {
        Stage userStage=new Stage();
        userStage.setScene(new Scene(mainPageParent));
        userStage.setTitle("Welcome,"+angajat.getUsername());
        userStage.setOnCloseRequest(new EventHandler<WindowEvent>() {
            @Override
            public void handle(WindowEvent event) {
                mainPageControl.logout();
                System.exit(0);
            }
        });
        mainPageControl.setAngajat(angajat);
        userStage.show();
        try {
            mainPageControl.init();
        } catch (ProjectException e) {
            e.printStackTrace();
        }
        ((Node)(actionEvent.getSource())).getScene().getWindow().hide();
    }
    public void setSrv(ICursaService srv,Stage LoginStage) {
        this.srv=srv;
        this.LogInStage=LoginStage;
    }
}
